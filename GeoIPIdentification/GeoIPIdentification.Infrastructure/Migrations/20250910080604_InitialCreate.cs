using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeoIPIdentification.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GeoIpBatches",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TotalCount = table.Column<int>(type: "INTEGER", nullable: false),
                    CompletedCount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeoIpBatches", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GeoIpResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BatchId = table.Column<Guid>(type: "TEXT", nullable: false),
                    IpAddress = table.Column<string>(type: "TEXT", nullable: false),
                    CountryCode = table.Column<string>(type: "TEXT", nullable: false),
                    CountryName = table.Column<string>(type: "TEXT", nullable: false),
                    TimeZone = table.Column<string>(type: "TEXT", nullable: false),
                    Latitude = table.Column<double>(type: "REAL", nullable: false),
                    Longitude = table.Column<double>(type: "REAL", nullable: false),
                    IsCompleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeoIpResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GeoIpResults_GeoIpBatches_BatchId",
                        column: x => x.BatchId,
                        principalTable: "GeoIpBatches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GeoIpResults_BatchId",
                table: "GeoIpResults",
                column: "BatchId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GeoIpResults");

            migrationBuilder.DropTable(
                name: "GeoIpBatches");
        }
    }
}
