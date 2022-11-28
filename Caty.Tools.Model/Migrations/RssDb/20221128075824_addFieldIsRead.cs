using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Caty.Tools.Model.Migrations.RssDb
{
    public partial class addFieldIsRead : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRead",
                table: "RssItems",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRead",
                table: "RssItems");
        }
    }
}
