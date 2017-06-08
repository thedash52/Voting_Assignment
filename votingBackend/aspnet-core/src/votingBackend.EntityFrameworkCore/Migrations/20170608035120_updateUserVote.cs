using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace votingBackend.Migrations
{
    public partial class updateUserVote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "UserVoteSet");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "UserVoteSet");

            migrationBuilder.DropColumn(
                name: "DeleterUserId",
                table: "UserVoteSet");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "UserVoteSet");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "UserVoteSet");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "UserVoteSet");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                table: "UserVoteSet");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserVoteSet");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "UserVoteSet",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "UserVoteSet",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "UserVoteSet");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "UserVoteSet");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "UserVoteSet",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "CreatorUserId",
                table: "UserVoteSet",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeleterUserId",
                table: "UserVoteSet",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "UserVoteSet",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "UserVoteSet",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "UserVoteSet",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LastModifierUserId",
                table: "UserVoteSet",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "UserVoteSet",
                nullable: false,
                defaultValue: 0);
        }
    }
}
