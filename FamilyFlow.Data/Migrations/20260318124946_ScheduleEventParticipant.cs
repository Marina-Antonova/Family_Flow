using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FamilyFlow.Data.Migrations
{
    /// <inheritdoc />
    public partial class ScheduleEventParticipant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleEvents_FamilyMembers_FamilyMemberId",
                table: "ScheduleEvents");

            migrationBuilder.DropIndex(
                name: "IX_ScheduleEvents_FamilyMemberId",
                table: "ScheduleEvents");

            migrationBuilder.RenameColumn(
                name: "FamilyMemberId",
                table: "ScheduleEvents",
                newName: "CreatorId");

            migrationBuilder.CreateTable(
                name: "ScheduleEventParticipants",
                columns: table => new
                {
                    ScheduleEventId = table.Column<int>(type: "int", nullable: false),
                    FamilyMemberId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleEventParticipants", x => new { x.ScheduleEventId, x.FamilyMemberId });
                    table.ForeignKey(
                        name: "FK_ScheduleEventParticipants_FamilyMembers_FamilyMemberId",
                        column: x => x.FamilyMemberId,
                        principalTable: "FamilyMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScheduleEventParticipants_ScheduleEvents_ScheduleEventId",
                        column: x => x.ScheduleEventId,
                        principalTable: "ScheduleEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleEventParticipants_FamilyMemberId",
                table: "ScheduleEventParticipants",
                column: "FamilyMemberId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScheduleEventParticipants");

            migrationBuilder.RenameColumn(
                name: "CreatorId",
                table: "ScheduleEvents",
                newName: "FamilyMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleEvents_FamilyMemberId",
                table: "ScheduleEvents",
                column: "FamilyMemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleEvents_FamilyMembers_FamilyMemberId",
                table: "ScheduleEvents",
                column: "FamilyMemberId",
                principalTable: "FamilyMembers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
