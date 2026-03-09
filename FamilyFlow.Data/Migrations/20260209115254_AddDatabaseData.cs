using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FamilyFlow.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDatabaseData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "FamilyMembers",
                columns: new[] { "Id", "Age", "Name", "Role" },
                values: new object[,]
                {
                    { 1, 40, "Alice", 0 },
                    { 2, 42, "Bob", 1 },
                    { 3, 12, "Charlie", 2 },
                    { 4, 10, "Daisy", 3 }
                });

            migrationBuilder.InsertData(
                table: "HouseTasks",
                columns: new[] { "Id", "Description", "DueDate", "FamilyMemberId", "IsCompleted", "Title" },
                values: new object[,]
                {
                    { 1, "Wipe down counters and mop the floor", new DateTime(2026, 2, 11, 13, 52, 51, 606, DateTimeKind.Local).AddTicks(6227), 1, false, "Clean the kitchen" },
                    { 2, "Mow the front and back yard", new DateTime(2026, 2, 12, 13, 52, 51, 606, DateTimeKind.Local).AddTicks(6301), 2, false, "Mow the lawn" },
                    { 3, "Collect all trash and recycling and take to curb", new DateTime(2026, 2, 10, 13, 52, 51, 606, DateTimeKind.Local).AddTicks(6309), 3, false, "Take out the trash" },
                    { 4, "Clean the exterior and interior of the car", new DateTime(2026, 2, 14, 13, 52, 51, 606, DateTimeKind.Local).AddTicks(6315), 1, false, "Wash the car" },
                    { 5, "Sort and declutter items in the garage", new DateTime(2026, 2, 16, 13, 52, 51, 606, DateTimeKind.Local).AddTicks(6320), 2, false, "Organize the garage" },
                    { 6, "Buy groceries for the week", new DateTime(2026, 2, 10, 13, 52, 51, 606, DateTimeKind.Local).AddTicks(6339), 3, false, "Grocery shopping" }
                });

            migrationBuilder.InsertData(
                table: "ScheduleEvents",
                columns: new[] { "Id", "EndTime", "FamilyMemberId", "StartTime", "Title" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 2, 15, 19, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2026, 2, 15, 18, 0, 0, 0, DateTimeKind.Unspecified), "Family Meeting" },
                    { 2, new DateTime(2026, 2, 14, 11, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2026, 2, 14, 10, 0, 0, 0, DateTimeKind.Unspecified), "Grocery Shopping" },
                    { 3, new DateTime(2026, 7, 3, 15, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2026, 7, 3, 14, 0, 0, 0, DateTimeKind.Unspecified), "Doctor Appointment" },
                    { 4, new DateTime(2026, 7, 4, 18, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2026, 7, 4, 16, 0, 0, 0, DateTimeKind.Unspecified), "Birthday Party" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "HouseTasks",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "HouseTasks",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "HouseTasks",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "HouseTasks",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "HouseTasks",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "HouseTasks",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ScheduleEvents",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ScheduleEvents",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ScheduleEvents",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ScheduleEvents",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "FamilyMembers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "FamilyMembers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "FamilyMembers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "FamilyMembers",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
