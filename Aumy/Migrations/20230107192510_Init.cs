using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aumy.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sockets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    State = table.Column<bool>(type: "bit", nullable: true),
                    Current = table.Column<float>(type: "real", nullable: true),
                    Power = table.Column<float>(type: "real", nullable: true),
                    Voltage = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sockets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Switchs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    State = table.Column<bool>(type: "bit", nullable: true),
                    Percentage = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Switchs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TuyaDevices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeviceId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TuyaDevices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsOnline = table.Column<bool>(type: "bit", nullable: true),
                    IsTuyaDevice = table.Column<bool>(type: "bit", nullable: false),
                    TuyaDeviceId = table.Column<int>(type: "int", nullable: true),
                    SwitchId = table.Column<int>(type: "int", nullable: true),
                    SocketId = table.Column<int>(type: "int", nullable: true),
                    DeviceTypeId = table.Column<int>(type: "int", nullable: false),
                    DeviceType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Devices_Sockets_SocketId",
                        column: x => x.SocketId,
                        principalTable: "Sockets",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Devices_Switchs_SwitchId",
                        column: x => x.SwitchId,
                        principalTable: "Switchs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Devices_TuyaDevices_TuyaDeviceId",
                        column: x => x.TuyaDeviceId,
                        principalTable: "TuyaDevices",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Devices_SocketId",
                table: "Devices",
                column: "SocketId");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_SwitchId",
                table: "Devices",
                column: "SwitchId");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_TuyaDeviceId",
                table: "Devices",
                column: "TuyaDeviceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Devices");

            migrationBuilder.DropTable(
                name: "Sockets");

            migrationBuilder.DropTable(
                name: "Switchs");

            migrationBuilder.DropTable(
                name: "TuyaDevices");
        }
    }
}
