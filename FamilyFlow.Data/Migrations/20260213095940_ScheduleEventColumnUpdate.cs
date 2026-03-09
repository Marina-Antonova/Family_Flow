using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FamilyFlow.Data.Migrations
{
    /// <inheritdoc />
    public partial class ScheduleEventColumnUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccompanyingAdultId",
                table: "ScheduleEvents",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "HouseTasks",
                keyColumn: "Id",
                keyValue: 1,
                column: "DueDate",
                value: new DateTime(2026, 2, 15, 11, 59, 37, 208, DateTimeKind.Local).AddTicks(6204));

            migrationBuilder.UpdateData(
                table: "HouseTasks",
                keyColumn: "Id",
                keyValue: 2,
                column: "DueDate",
                value: new DateTime(2026, 2, 16, 11, 59, 37, 208, DateTimeKind.Local).AddTicks(6278));

            migrationBuilder.UpdateData(
                table: "HouseTasks",
                keyColumn: "Id",
                keyValue: 3,
                column: "DueDate",
                value: new DateTime(2026, 2, 14, 11, 59, 37, 208, DateTimeKind.Local).AddTicks(6297));

            migrationBuilder.UpdateData(
                table: "HouseTasks",
                keyColumn: "Id",
                keyValue: 4,
                column: "DueDate",
                value: new DateTime(2026, 2, 18, 11, 59, 37, 208, DateTimeKind.Local).AddTicks(6314));

            migrationBuilder.UpdateData(
                table: "HouseTasks",
                keyColumn: "Id",
                keyValue: 5,
                column: "DueDate",
                value: new DateTime(2026, 2, 20, 11, 59, 37, 208, DateTimeKind.Local).AddTicks(6331));

            migrationBuilder.UpdateData(
                table: "HouseTasks",
                keyColumn: "Id",
                keyValue: 6,
                column: "DueDate",
                value: new DateTime(2026, 2, 14, 11, 59, 37, 208, DateTimeKind.Local).AddTicks(6357));

            migrationBuilder.UpdateData(
                table: "ScheduleEvents",
                keyColumn: "Id",
                keyValue: 1,
                column: "AccompanyingAdultId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ScheduleEvents",
                keyColumn: "Id",
                keyValue: 2,
                column: "AccompanyingAdultId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ScheduleEvents",
                keyColumn: "Id",
                keyValue: 3,
                column: "AccompanyingAdultId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "ScheduleEvents",
                keyColumn: "Id",
                keyValue: 4,
                column: "AccompanyingAdultId",
                value: 2);

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleEvents_AccompanyingAdultId",
                table: "ScheduleEvents",
                column: "AccompanyingAdultId");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleEvents_FamilyMembers_AccompanyingAdultId",
                table: "ScheduleEvents",
                column: "AccompanyingAdultId",
                principalTable: "FamilyMembers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleEvents_FamilyMembers_AccompanyingAdultId",
                table: "ScheduleEvents");

            migrationBuilder.DropIndex(
                name: "IX_ScheduleEvents_AccompanyingAdultId",
                table: "ScheduleEvents");

            migrationBuilder.DropColumn(
                name: "AccompanyingAdultId",
                table: "ScheduleEvents");

            migrationBuilder.UpdateData(
                table: "HouseTasks",
                keyColumn: "Id",
                keyValue: 1,
                column: "DueDate",
                value: new DateTime(2026, 2, 11, 13, 52, 51, 606, DateTimeKind.Local).AddTicks(6227));

            migrationBuilder.UpdateData(
                table: "HouseTasks",
                keyColumn: "Id",
                keyValue: 2,
                column: "DueDate",
                value: new DateTime(2026, 2, 12, 13, 52, 51, 606, DateTimeKind.Local).AddTicks(6301));

            migrationBuilder.UpdateData(
                table: "HouseTasks",
                keyColumn: "Id",
                keyValue: 3,
                column: "DueDate",
                value: new DateTime(2026, 2, 10, 13, 52, 51, 606, DateTimeKind.Local).AddTicks(6309));

            migrationBuilder.UpdateData(
                table: "HouseTasks",
                keyColumn: "Id",
                keyValue: 4,
                column: "DueDate",
                value: new DateTime(2026, 2, 14, 13, 52, 51, 606, DateTimeKind.Local).AddTicks(6315));

            migrationBuilder.UpdateData(
                table: "HouseTasks",
                keyColumn: "Id",
                keyValue: 5,
                column: "DueDate",
                value: new DateTime(2026, 2, 16, 13, 52, 51, 606, DateTimeKind.Local).AddTicks(6320));

            migrationBuilder.UpdateData(
                table: "HouseTasks",
                keyColumn: "Id",
                keyValue: 6,
                column: "DueDate",
                value: new DateTime(2026, 2, 10, 13, 52, 51, 606, DateTimeKind.Local).AddTicks(6339));
        }
    }
}
