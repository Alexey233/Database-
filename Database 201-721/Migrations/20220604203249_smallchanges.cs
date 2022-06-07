using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database_201_721.Migrations
{
    public partial class smallchanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ChangeRequestYear",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChangeRequestYear",
                table: "AspNetUsers");
        }
    }
}
