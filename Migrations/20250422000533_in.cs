using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ipSec.Migrations
{
    /// <inheritdoc />
    public partial class @in : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TrapLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    UserAgent = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Platform = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Language = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Time = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ScreenResolution = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Timezone = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Referrer = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    DeviceMemory = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    HardwareConcurrency = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IPAddress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Region = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ISP = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Rating = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrapLogs", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrapLogs");
        }
    }
}
