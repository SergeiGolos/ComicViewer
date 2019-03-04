using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ComicViewer.Web.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {           
            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    OriginalName = table.Column<string>(nullable: true),
                    Path = table.Column<string>(nullable: true),
                    Extension = table.Column<string>(nullable: true),
                    Thumbnail = table.Column<byte[]>(nullable: true),
                    Length = table.Column<int>(nullable: false),
                    Height = table.Column<int>(nullable: false),
                    Width = table.Column<int>(nullable: false),
                    Publisher = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Year = table.Column<string>(nullable: true),
                    Month = table.Column<string>(nullable: true),
                    Volume = table.Column<string>(nullable: true),
                    Issue = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true)
                },                                
                constraints: table =>
                {
                    
                    table.PrimaryKey("PK_Files", x => x.Id);
                });

            migrationBuilder.CreateIndex("idx_original_name", "Files", new string[] { "OriginalName" });
            migrationBuilder.CreateIndex("idx_publisher", "Files", new string[] { "Publisher" });
            migrationBuilder.CreateIndex("idx_path", "Files", new string[] { "Path" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Files");
        }
    }
}
