using System.ComponentModel.DataAnnotations;
using CollectIt.Database.Abstractions.Resources;
using CollectIt.Database.Infrastructure.Account.Data;
using CollectIt.Database.Infrastructure.Resources.FileManagers;
using CollectIt.MVC.View.ViewModels;
using CollectIt.MVC.View.Views.Shared.Components.MusicCards;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CollectIt.MVC.View.Controllers;

[Route("musics")]
public class MusicsController : Controller
{
    private readonly IMusicManager _musicManager;
    private readonly UserManager _userManager;
    private const int DefaultPageSize = 15;
    public MusicsController(IMusicManager musicManager, UserManager userManager)
    {
        _musicManager = musicManager;
        _userManager = userManager;
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Music(int id)
    {
        var source = await _musicManager.FindByIdAsync(id);
        if (source == null)
        {
            return View("Error");
        }

        var model = new MusicViewModel()
        {
            MusicId = id,
            Name = source.Name,
            OwnerName = source.Owner.UserName,
            UploadDate = source.UploadDate,
            Address = $"/imagesFromDb/{source.FileName}",
            Tags = source.Tags,
            IsAcquired = await _musicManager.IsAcquiredBy(source.OwnerId, id)
        };
        return View(model);
    }

    [HttpGet("")]
    public async Task<IActionResult> GetQueriedMusics([FromQuery(Name = "q")] 
                                                      [Required]
                                                      string query, 
                                                      [Range(1, int.MaxValue)]
                                                      [FromQuery(Name = "page_number")]
                                                      int pageNumber = 1,
                                                      string? redirectUrl = null)
    {
        var musics = await _musicManager.QueryAsync(query, pageNumber, DefaultPageSize);
        return View("Musics", new MusicCardsViewModel()
                              {
                                  Musics = musics.Result.Select(m => new MusicViewModel()
                                                                     {
                                                                         Address = Url.Action("GetMusicBlob", new {id = m.Id})!,
                                                                         Name = m.Name,
                                                                     }).ToList(),
                                  Query = query,
                                  PageNumber = pageNumber,
                                  MaxMusicsCount = musics.TotalCount
                              });
    }

    [HttpGet("upload")]
    [Authorize]
    public async Task<IActionResult> UploadMusic()
    {
        return View();
    }

    [HttpPost("upload")]
    [Authorize]
    public async Task<IActionResult> UploadMusic(
        [FromForm]
        [Required] 
        UploadMusicViewModel model)
    {
        var userId = int.Parse(_userManager.GetUserId(User));
        if (!TryGetExtension(model.Content.FileName, out var extension))
        {
            // ModelState.AddModelError("FormFile", $"Поддерживаемые расширения музыки: {SupportedVideoFormats.Aggregate((s, n) => $"{s}, {n}")}");
            return View("Error",
                        new ErrorViewModel()
                        {
                            Message =
                                $"Поддерживаемые расширения видео: {SupportedMusicExtensions.Aggregate((s, n) => $"{s}, {n}")}"
                        });
        }
        try
        {
            await using var stream = model.Content.OpenReadStream();
            var music = await _musicManager.CreateAsync(model.Name, userId, model.Tags.Split(' ', StringSplitOptions.RemoveEmptyEntries), stream, extension,
                                                        model.Duration);

            return RedirectToAction("GetQueriedMusics", new {q = ""});
        }
        catch (Exception ex)
        {
            return BadRequest();
        }
    }

    private bool TryGetExtension(string filename, out string extension)
    {
        extension = null!;
        if (filename is null)
        {
            throw new ArgumentNullException(nameof(filename));
        }

        var array = filename.Split('.');
        if (array.Length < 2)
        {
            return false;
        }

        return SupportedMusicExtensions.Contains(extension = array[^1].ToLower());
    }

    private static readonly HashSet<string> SupportedMusicExtensions = new() {"mp3", "ogg", "wav"};

    [HttpGet("{id:int}/blob")]
    public async Task<IActionResult> GetMusicBlob(int id)
    {
        var music = await _musicManager.FindByIdAsync(id);
        if (music is null)
        {
            return View("Error", new ErrorViewModel() {Message = "Music not found"});
        }
        var stream = await _musicManager.GetContentAsync(id);
        return File(stream, $"audio/{music!.Extension}", $"{music.Name}.{music.Extension}");
    }

    [HttpGet("{id:int}/download")]
    [Authorize]
    public async Task<IActionResult> DownloadMusicContent(int id)
    {
        var userId = int.Parse(_userManager.GetUserId(User));
        if (await _musicManager.IsAcquiredBy(id, userId))
        {
            var image = await _musicManager.FindByIdAsync(id);
            var stream = await _musicManager.GetContentAsync(id);
            return File(stream, $"audio/{image!.Extension}", $"{image.Name}.{image.Extension}");
        }

        return BadRequest();
    }
}