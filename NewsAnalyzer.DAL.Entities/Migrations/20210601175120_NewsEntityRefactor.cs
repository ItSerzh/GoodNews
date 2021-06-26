using Microsoft.EntityFrameworkCore.Migrations;

namespace NewsAnalyzer.DAL.Core.Migrations
{
    public partial class NewsEntityRefactor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Content",
                table: "News",
                newName: "Summary");

            migrationBuilder.AddColumn<string>(
                name: "Body",
                table: "News",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Body",
                table: "News");

            migrationBuilder.RenameColumn(
                name: "Summary",
                table: "News",
                newName: "Content");
        }
    }
}
