using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Chuanhoafile.Migrations
{
    public partial class ipinit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ipconnectwebs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateUpdate = table.Column<DateTime>(nullable: false),
                    UpdateBy = table.Column<string>(nullable: true),
                    ip = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ipconnectwebs", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ipconnectwebs");
        }
    }
}
