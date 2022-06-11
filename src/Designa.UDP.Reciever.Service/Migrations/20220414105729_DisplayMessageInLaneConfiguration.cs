using Microsoft.EntityFrameworkCore.Migrations;

namespace Designa.UDP.Reciever.Service.Migrations
{
    public partial class DisplayMessageInLaneConfiguration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "display_reset_message",
                table: "lane_configurations",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "display_reset_message",
                table: "lane_configurations");
        }
    }
}
