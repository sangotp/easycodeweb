using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyCodeAcademy.Web.Migrations
{
    public partial class coursedetailstable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "courseDetails",
                columns: table => new
                {
                    CourseDetailsId = table.Column<int>(type: "int", nullable: false),
                    CourseLanguage = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CourseOverview = table.Column<string>(type: "ntext", nullable: false),
                    CourseInclude = table.Column<string>(type: "ntext", nullable: false),
                    CourseRequirement = table.Column<string>(type: "ntext", nullable: false),
                    CourseGain = table.Column<string>(type: "ntext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_courseDetails", x => x.CourseDetailsId);
                    table.ForeignKey(
                        name: "FK_courseDetails_courses_CourseDetailsId",
                        column: x => x.CourseDetailsId,
                        principalTable: "courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "courseDetails");
        }
    }
}
