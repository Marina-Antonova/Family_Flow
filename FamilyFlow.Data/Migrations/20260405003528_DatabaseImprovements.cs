using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FamilyFlow.Data.Migrations
{
    /// <inheritdoc />
    public partial class DatabaseImprovements : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "FamilyMembers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LinkedUserId",
                table: "FamilyMembers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FamilyMembers_LinkedUserId",
                table: "FamilyMembers",
                column: "LinkedUserId",
                unique: true,
                filter: "[LinkedUserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_FamilyMembers_AspNetUsers_LinkedUserId",
                table: "FamilyMembers",
                column: "LinkedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FamilyMembers_AspNetUsers_LinkedUserId",
                table: "FamilyMembers");

            migrationBuilder.DropIndex(
                name: "IX_FamilyMembers_LinkedUserId",
                table: "FamilyMembers");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "FamilyMembers");

            migrationBuilder.DropColumn(
                name: "LinkedUserId",
                table: "FamilyMembers");
        }
    }
}
