using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace votingBackend.Migrations
{
    public partial class updateUserVote2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Referendum",
                table: "UserVoteSet",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<int>(
                name: "PartyId",
                table: "UserVoteSet",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "ElectorateId",
                table: "UserVoteSet",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Referendum",
                table: "UserVoteSet",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PartyId",
                table: "UserVoteSet",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ElectorateId",
                table: "UserVoteSet",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
