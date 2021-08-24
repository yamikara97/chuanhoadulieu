using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Chuanhoafile.Migrations
{
    public partial class addtext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Recomments",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateUpdate = table.Column<DateTime>(nullable: false),
                    UpdateBy = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    Author = table.Column<string>(nullable: true),
                    Phonenumber = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recomments", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Recomments");
        }
    }
}
