using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyCodeAcademy.Web.Migrations
{
    public partial class courseLessonsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "courseLessons",
                columns: table => new
                {
                    LessonId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "ntext", nullable: true),
                    Content = table.Column<string>(type: "ntext", nullable: true),
                    Video = table.Column<string>(type: "ntext", nullable: true),
                    Achievement = table.Column<string>(type: "ntext", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChapterId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_courseLessons", x => x.LessonId);
                    table.ForeignKey(
                        name: "FK_courseLessons_courseChapters_ChapterId",
                        column: x => x.ChapterId,
                        principalTable: "courseChapters",
                        principalColumn: "ChapterId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_courseLessons_ChapterId",
                table: "courseLessons",
                column: "ChapterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "courseLessons");
        }
    }
}
