using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace votingBackend.Migrations
{
    public partial class updateReferendum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "ReferendumSet",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "ReferendumSet");
        }
    }
}
