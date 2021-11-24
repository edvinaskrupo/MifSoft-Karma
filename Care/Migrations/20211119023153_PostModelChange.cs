using Microsoft.EntityFrameworkCore.Migrations;

namespace Care.Migrations
{
    public partial class PostModelChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrgLogo",
                table: "Posts");

            migrationBuilder.RenameColumn(
                name: "OrgPhoto",
                table: "Posts",
                newName: "OrgPhotoName");

            migrationBuilder.AddColumn<string>(
                name: "OrgLogoName",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrgLogoName",
                table: "Posts");

            migrationBuilder.RenameColumn(
                name: "OrgPhotoName",
                table: "Posts",
                newName: "OrgPhoto");

            migrationBuilder.AddColumn<string>(
                name: "OrgLogo",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
