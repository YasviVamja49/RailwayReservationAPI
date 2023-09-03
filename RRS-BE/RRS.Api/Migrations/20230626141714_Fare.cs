using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RRS.Api.Migrations
{
    public partial class Fare : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BaseFare",
                table: "Trains",
                type: "int",
                nullable: false,
                defaultValue: 200);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BaseFare",
                table: "Trains");
        }
    }
}
