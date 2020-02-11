using Microsoft.EntityFrameworkCore.Migrations;

namespace CRUDEF.Migrations
{
    public partial class UserKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PassportNumber",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PassportSeria",
                table: "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PassportNumber",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PassportSeria",
                table: "Users");
        }
    }
}
