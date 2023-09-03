using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RRS.Api.Migrations
{
    public partial class Modeladded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Passengers",
                columns: table => new
                {
                    PassengerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PassengerName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passengers", x => x.PassengerId);
                    table.ForeignKey(
                        name: "FK_Passengers_Users_Id",
                        column: x => x.Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trains",
                columns: table => new
                {
                    TrainNo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Startloc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Endloc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Arrivaltime = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Departuretime = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Arrivaldate = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Ac1tier = table.Column<int>(type: "int", nullable: false),
                    Ac2tier = table.Column<int>(type: "int", nullable: false),
                    Ac3tier = table.Column<int>(type: "int", nullable: false),
                    Sleeper = table.Column<int>(type: "int", nullable: false),
                    Tatkal = table.Column<int>(type: "int", nullable: false),
                    Ladies = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trains", x => x.TrainNo);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Ticketno = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ticketclass = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Berthno = table.Column<int>(type: "int", nullable: false),
                    Coachno = table.Column<int>(type: "int", nullable: false),
                    Arrivaldate = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Bookingdate = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Bookingstatus = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PassengerId = table.Column<int>(type: "int", nullable: false),
                    TrainNo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Ticketno);
                    table.ForeignKey(
                        name: "FK_Tickets_Passengers_PassengerId",
                        column: x => x.PassengerId,
                        principalTable: "Passengers",
                        principalColumn: "PassengerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tickets_Trains_TrainNo",
                        column: x => x.TrainNo,
                        principalTable: "Trains",
                        principalColumn: "TrainNo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Passengers_Id",
                table: "Passengers",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_PassengerId",
                table: "Tickets",
                column: "PassengerId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_TrainNo",
                table: "Tickets",
                column: "TrainNo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "Passengers");

            migrationBuilder.DropTable(
                name: "Trains");
        }
    }
}
