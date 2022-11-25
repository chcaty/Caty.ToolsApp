using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Caty.Tools.Model.Migrations.RssDb
{
    public partial class addfiledsourceId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SourceId",
                table: "RssFeeds",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SourceId",
                table: "RssFeeds");
        }
    }
}
