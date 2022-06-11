using Microsoft.EntityFrameworkCore.Migrations;

namespace Designa.UDP.Reciever.Service.Migrations
{
    public partial class UpdateLaneConfigurationsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_lane_configuration",
                table: "lane_configuration");

            migrationBuilder.RenameTable(
                name: "lane_configuration",
                newName: "lane_configurations");

            migrationBuilder.AddPrimaryKey(
                name: "pk_lane_configurations",
                table: "lane_configurations",
                column: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_lane_configurations",
                table: "lane_configurations");

            migrationBuilder.RenameTable(
                name: "lane_configurations",
                newName: "lane_configuration");

            migrationBuilder.AddPrimaryKey(
                name: "pk_lane_configuration",
                table: "lane_configuration",
                column: "id");
        }
    }
}
