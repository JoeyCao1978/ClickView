using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Persistence.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Videos",
                columns: table => new
                {
                    Name = table.Column<string>(nullable: true),
                    Duration = table.Column<int>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: true),
                    Id = table.Column<int>(nullable: true),
                    Thumbnail = table.Column<string>(nullable: true)
                }
            );
            migrationBuilder.CreateTable(
                name: "Playlists",
                columns: table => new
                {
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),                 
                    Id = table.Column<int>(nullable: true),
                    VideoIds = table.Column<int>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: true),
                }
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Videos");
            migrationBuilder.DropTable(
                name: "Playlists");
        }
    }
}
