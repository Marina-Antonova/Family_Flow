using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FamilyFlow.Data.Migrations
{
    /// <inheritdoc />
    public partial class UserSeeder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), 0, "d3e9ad1c-fb02-4243-91d0-03c5368ab041", "mother@familyflow.com", true, false, null, "MOTHER@FAMILYFLOW.COM", "MOTHER@FAMILYFLOW.COM", "AQAAAAIAAYagAAAAEHnhByjI59ZTArg8AGcE05N/x8nf6vU5wnMmT5Em/RvzkSqj/geZaxotLR4WoT8X4A==", null, false, "7483fe14-54e5-4727-bcf9-9a04a570e87b", false, "mother@familyflow.com" },
                    { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), 0, "197d9a55-d8ed-423a-94ac-5c76067b7d26", "father@familyflow.com", true, false, null, "FATHER@FAMILYFLOW.COM", "FATHER@FAMILYFLOW.COM", "AQAAAAIAAYagAAAAENn1wO37w52A5GHser1XkaOpTU2Er15xbd7ntzawK3c+akUTWW7kpuXsxeZvCr8a0Q==", null, false, "a7983a11-c341-484e-bcc6-998c0e2762b6", false, "father@familyflow.com" },
                    { new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), 0, "5e8e2bc0-9858-4a04-b927-2d7816f43bab", "son@familyflow.com", true, false, null, "SON@FAMILYFLOW.COM", "SON@FAMILYFLOW.COM", "AQAAAAIAAYagAAAAECIeF2eyD8Q/Vr42OzkZLQriJmeTesLNiMJsPGhP47gzwTgIaz6WHLHp7YhrT09Q0Q==", null, false, "0adbf915-8298-44bb-9ed4-001dcca1049b", false, "son@familyflow.com" },
                    { new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), 0, "4beab116-157c-44ba-8edb-25278c51c94b", "daughter@familyflow.com", true, false, null, "DAUGHTER@FAMILYFLOW.COM", "DAUGHTER@FAMILYFLOW.COM", "AQAAAAIAAYagAAAAEJCoXu7+yC4ll0hnFwDmGmD/+V+7oX/Wqqsi3gUqqusFCFTGo9z7O8VGUVMNQMBi6A==", null, false, "6b7debd6-d685-4410-a584-07ecfe19a5b4", false, "daughter@familyflow.com" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"));
        }
    }
}
