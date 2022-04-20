import {Body, Controller, Delete, Get, Post, Query, Res, UseGuards} from '@nestjs/common';
import {UsersService} from "./users.service";
import {AdminJwtAuthGuard, AuthorizeAdmin} from "../auth/admin-jwt-auth.guard";
import {AssignRoleDto} from "./dto/assign-role.dto";
import {Authorize} from "../auth/jwt-auth.guard";
import {RemoveRoleDto} from "./dto/remove-role.dto";
import {ReadUserDto} from "./dto/read-user.dto";
import {Response} from "express";

@Authorize()
@Controller('api/v1/users')
export class UsersController {
    constructor(private usersService: UsersService) { }

    @Get('')
    async getAll(@Query('page_number') pageNumber: number,
                 @Query('page_size') pageSize: number,
                 @Res() response: Response) {
        const result = await this.usersService.getAllUsersAsync(pageNumber, pageSize);
        const count = result.count;
        const users = result.rows;
        response.setHeader('X-Total-Count', count);
        const dtos = users.map(u => ({
            id: u.id,
            username: u.username,
            email: u.email,
            roles: u.roles?.map(r => r.name) ?? []})
        );
        response.send(dtos);
        response.end();
    }



    @Post(':userId/roles')
    @AuthorizeAdmin()
    async assignRole(@Body() {role, userId}: AssignRoleDto) {
        await this.usersService.addRoleToUser(userId, role);
    }

    @Delete(':userId/roles')
    @AuthorizeAdmin()
    async removeRole(@Body() {userId, role}: RemoveRoleDto) {
        await this.usersService.removeRoleFromUser(userId, role);

    }
}
