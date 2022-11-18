using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyCodeAcademy.Web.Migrations
{
    public partial class courseExercisesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "courseExerises",
                columns: table => new
                {
                    ExerciseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "ntext", nullable: true),
                    Content = table.Column<string>(type: "ntext", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LessonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_courseExerises", x => x.ExerciseId);
                    table.ForeignKey(
                        name: "FK_courseExerises_courseLessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "courseLessons",
                        principalColumn: "LessonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_courseExerises_LessonId",
                table: "courseExerises",
                column: "LessonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "courseExerises");
        }
    }
}
