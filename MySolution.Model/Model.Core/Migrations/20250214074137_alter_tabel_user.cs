using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Model.Core.Migrations
{
    public partial class alter_tabel_user : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "email",
                table: "users");

            migrationBuilder.DropColumn(
                name: "name",
                table: "users");

            migrationBuilder.AddColumn<string>(
                name: "code",
                table: "users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "hash_pass",
                table: "users",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "code",
                table: "users");

            migrationBuilder.DropColumn(
                name: "hash_pass",
                table: "users");

            migrationBuilder.AddColumn<string>(
                name: "email",
                table: "users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "users",
                type: "text",
                nullable: true);
        }
    }
}
