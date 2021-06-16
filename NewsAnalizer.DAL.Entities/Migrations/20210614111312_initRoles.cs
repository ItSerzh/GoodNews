using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace NewsAnalizer.DAL.Core.Migrations
{
    public partial class initRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var adminRoleId = Guid.NewGuid();
            var userRoleId = Guid.NewGuid();
            migrationBuilder.Sql($"INSERT INTO [Role] ([Id],[Name]) VALUES ('{adminRoleId}','Admin')");
            migrationBuilder.Sql($"INSERT INTO [Role] ([Id],[Name]) VALUES ('{userRoleId}','User')");

            migrationBuilder.DropIndex(
                name: "IX_Users_RoleId",
                table: "Users");

            migrationBuilder.AlterColumn<Guid>(
                name: "RoleId",
                table: "Users",
                defaultValue: userRoleId);

            migrationBuilder.Sql($"UPDATE [Users] SET RoleId = '{userRoleId}'");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($"DELETE FROM [Role] WHERE Name in ('Admin', 'User')");
            migrationBuilder.AlterColumn<Guid>(
                name: "RoleId",
                table: "Users",
                nullable: true,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

        }
    }
}
