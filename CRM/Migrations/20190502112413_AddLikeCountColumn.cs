﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Migrations
{
    public partial class AddLikeCountColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LikeCount",
                table: "Feedback",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LikeCount",
                table: "Feedback");
        }
    }
}
