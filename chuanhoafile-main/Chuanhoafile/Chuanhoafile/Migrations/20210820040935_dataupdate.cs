using Microsoft.EntityFrameworkCore.Migrations;

namespace Chuanhoafile.Migrations
{
    public partial class dataupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "placeFatherCode",
                table: "PlaceCases",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "placeFatherCode",
                table: "PlaceCases");
        }
    }
}
