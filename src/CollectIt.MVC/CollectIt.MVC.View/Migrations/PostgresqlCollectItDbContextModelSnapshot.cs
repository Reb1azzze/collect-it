﻿// <auto-generated />
using System;
using CollectIt.Database.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NodaTime;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using NpgsqlTypes;

#nullable disable

namespace CollectIt.MVC.View.Migrations
{
    [DbContext(typeof(PostgresqlCollectItDbContext))]
    partial class PostgresqlCollectItDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CollectIt.Database.Entities.Account.ActiveUserSubscription", b =>
                {
                    b.Property<DateInterval>("During")
                        .IsRequired()
                        .HasColumnType("daterange");

                    b.Property<int>("LeftResourcesCount")
                        .HasColumnType("integer");

                    b.Property<int>("MaxResourcesCount")
                        .HasColumnType("integer");

                    b.Property<int>("SubscriptionId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasIndex("SubscriptionId");

                    b.HasIndex("UserId");

                    b.ToView("ActiveUsersSubscriptions");
                });

            modelBuilder.Entity("CollectIt.Database.Entities.Account.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ConcurrencyStamp = "DEFAULT_STAMP",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = 2,
                            ConcurrencyStamp = "DEFAULT_STAMP",
                            Name = "User",
                            NormalizedName = "USER"
                        },
                        new
                        {
                            Id = 3,
                            ConcurrencyStamp = "DEFAULT_STAMP",
                            Name = "Technical Support",
                            NormalizedName = "TECHNICAL SUPPORT"
                        });
                });

            modelBuilder.Entity("CollectIt.Database.Entities.Account.Subscription", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("AppliedResourceType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("MaxResourcesCount")
                        .HasColumnType("integer");

                    b.Property<int>("MonthDuration")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Price")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Subscriptions");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AppliedResourceType = "Image",
                            Description = "Обычная подписка",
                            MaxResourcesCount = 50,
                            MonthDuration = 1,
                            Name = "Бронзовая",
                            Price = 200
                        },
                        new
                        {
                            Id = 2,
                            AppliedResourceType = "Image",
                            Description = "Подписка для любителей качать",
                            MaxResourcesCount = 100,
                            MonthDuration = 1,
                            Name = "Серебрянная",
                            Price = 350
                        },
                        new
                        {
                            Id = 3,
                            AppliedResourceType = "Image",
                            Description = "Не для пиратов",
                            MaxResourcesCount = 200,
                            MonthDuration = 1,
                            Name = "Золотая",
                            Price = 500
                        });
                });

            modelBuilder.Entity("CollectIt.Database.Entities.Account.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<int?>("RoleId")
                        .HasColumnType("integer");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "3e0213e9-8d80-48df-b9df-18fc7debd84e",
                            Email = "asdf@mail.ru",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedEmail = "ASDF@MAIL.RU",
                            PasswordHash = "AQAAAAEAACcQAAAAEAO/K1C4Jn77AXrULgaNn6rkHlrkXbk9jOqHqe+HK+CvDgmBEEFahFadKE8H7x4Olw==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "MSCN3JBQERUJBPLR4XIXZH3TQGICF6O3",
                            TwoFactorEnabled = false,
                            UserName = "asdf@mail.ru"
                        });
                });

            modelBuilder.Entity("CollectIt.Database.Entities.Account.UserSubscription", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateInterval>("During")
                        .IsRequired()
                        .HasColumnType("daterange");

                    b.Property<int>("LeftResourcesCount")
                        .HasColumnType("integer");

                    b.Property<int>("SubscriptionId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("SubscriptionId");

                    b.HasIndex("UserId");

                    b.ToTable("UsersSubscriptions");
                });

            modelBuilder.Entity("CollectIt.Database.Entities.Resources.Comment", b =>
                {
                    b.Property<int>("CommentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("CommentId"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("OwnerId")
                        .HasColumnType("integer");

                    b.Property<int>("TargetId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UploadDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("CommentId");

                    b.HasIndex("OwnerId");

                    b.HasIndex("TargetId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("CollectIt.Database.Entities.Resources.Resource", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<NpgsqlTsVector>("NameSearchVector")
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("tsvector")
                        .HasAnnotation("Npgsql:TsVectorConfig", "russian")
                        .HasAnnotation("Npgsql:TsVectorProperties", new[] { "Name" });

                    b.Property<int>("OwnerId")
                        .HasColumnType("integer");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UploadDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("NameSearchVector");

                    NpgsqlIndexBuilderExtensions.HasMethod(b.HasIndex("NameSearchVector"), "GIN");

                    b.HasIndex("OwnerId");

                    b.ToTable("Resources");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("CollectIt.Database.Entities.Resources.Image", b =>
                {
                    b.HasBaseType("CollectIt.Database.Entities.Resources.Resource");

                    b.ToTable("Images");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Первое изображение",
                            OwnerId = 1,
                            Path = "/imagesFromDb/avaSig.jpg",
                            UploadDate = new DateTime(2022, 3, 27, 10, 56, 59, 207, DateTimeKind.Utc)
                        });
                });

            modelBuilder.Entity("CollectIt.Database.Entities.Resources.Music", b =>
                {
                    b.HasBaseType("CollectIt.Database.Entities.Resources.Resource");

                    b.Property<TimeSpan>("Duration")
                        .HasColumnType("interval");

                    b.ToTable("Musics");
                });

            modelBuilder.Entity("CollectIt.Database.Entities.Resources.Video", b =>
                {
                    b.HasBaseType("CollectIt.Database.Entities.Resources.Resource");

                    b.Property<TimeSpan>("Duration")
                        .HasColumnType("interval");

                    b.ToTable("Videos");
                });

            modelBuilder.Entity("CollectIt.Database.Entities.Account.ActiveUserSubscription", b =>
                {
                    b.HasOne("CollectIt.Database.Entities.Account.Subscription", "Subscription")
                        .WithMany()
                        .HasForeignKey("SubscriptionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CollectIt.Database.Entities.Account.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Subscription");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CollectIt.Database.Entities.Account.User", b =>
                {
                    b.HasOne("CollectIt.Database.Entities.Account.Role", null)
                        .WithMany("Users")
                        .HasForeignKey("RoleId");
                });

            modelBuilder.Entity("CollectIt.Database.Entities.Account.UserSubscription", b =>
                {
                    b.HasOne("CollectIt.Database.Entities.Account.Subscription", "Subscription")
                        .WithMany()
                        .HasForeignKey("SubscriptionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CollectIt.Database.Entities.Account.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Subscription");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CollectIt.Database.Entities.Resources.Comment", b =>
                {
                    b.HasOne("CollectIt.Database.Entities.Account.User", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CollectIt.Database.Entities.Resources.Resource", "Target")
                        .WithMany()
                        .HasForeignKey("TargetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");

                    b.Navigation("Target");
                });

            modelBuilder.Entity("CollectIt.Database.Entities.Resources.Resource", b =>
                {
                    b.HasOne("CollectIt.Database.Entities.Account.User", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("CollectIt.Database.Entities.Account.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("CollectIt.Database.Entities.Account.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("CollectIt.Database.Entities.Account.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.HasOne("CollectIt.Database.Entities.Account.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CollectIt.Database.Entities.Account.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("CollectIt.Database.Entities.Account.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CollectIt.Database.Entities.Resources.Image", b =>
                {
                    b.HasOne("CollectIt.Database.Entities.Resources.Resource", null)
                        .WithOne()
                        .HasForeignKey("CollectIt.Database.Entities.Resources.Image", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CollectIt.Database.Entities.Resources.Music", b =>
                {
                    b.HasOne("CollectIt.Database.Entities.Resources.Resource", null)
                        .WithOne()
                        .HasForeignKey("CollectIt.Database.Entities.Resources.Music", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CollectIt.Database.Entities.Resources.Video", b =>
                {
                    b.HasOne("CollectIt.Database.Entities.Resources.Resource", null)
                        .WithOne()
                        .HasForeignKey("CollectIt.Database.Entities.Resources.Video", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CollectIt.Database.Entities.Account.Role", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
