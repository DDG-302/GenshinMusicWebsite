using Microsoft.EntityFrameworkCore.Migrations;

namespace genshinwebsite.Migrations.MusicData
{
    public partial class music : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Music",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Datetime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MusicTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Abstract_content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    User_id = table.Column<int>(type: "int", nullable: false),
                    View_num = table.Column<int>(type: "int", nullable: false),
                    Like_num = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Music", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Music");
        }
    }
}
