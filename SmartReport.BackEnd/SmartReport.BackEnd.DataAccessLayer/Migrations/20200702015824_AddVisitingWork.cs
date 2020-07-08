using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartReport.BackEnd.DataAccessLayer.Migrations
{
    public partial class AddVisitingWork : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsWork",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "VisitDates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTimeOffset>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisitDates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VisitDates_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VisitDates_UserId",
                table: "VisitDates",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VisitDates");

            migrationBuilder.DropColumn(
                name: "IsWork",
                table: "AspNetUsers");
        }
    }
}
