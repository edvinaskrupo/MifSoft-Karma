using Microsoft.EntityFrameworkCore.Migrations;

namespace Care.Migrations
{
    public partial class Posts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    OrgId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrgName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrgShortDescr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrgLongDescr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrgLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrgLogo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrgPhoto = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.OrgId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Posts");
        }
    }
}
