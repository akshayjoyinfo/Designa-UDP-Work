using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Designa.UDP.Reciever.Service.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "configs",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    config_key = table.Column<string>(nullable: true),
                    config_value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_configs", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "fastag_audit_report",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    parking_node = table.Column<string>(nullable: true),
                    transaction_id = table.Column<string>(nullable: true),
                    ticket_id = table.Column<string>(nullable: true),
                    card_tag_no = table.Column<string>(nullable: true),
                    date_time_of_entry = table.Column<DateTime>(nullable: true),
                    date_time_of_exit = table.Column<DateTime>(nullable: true),
                    vehicle_number = table.Column<string>(nullable: true),
                    entry_device_name = table.Column<string>(nullable: true),
                    exit_device_name = table.Column<string>(nullable: true),
                    mode_of_payment = table.Column<string>(nullable: true),
                    parking_fee_gross = table.Column<decimal>(nullable: false),
                    tax = table.Column<decimal>(nullable: false),
                    parking_fee_net = table.Column<decimal>(nullable: false),
                    stay_duration = table.Column<string>(nullable: true),
                    last_modified_utc = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_fastag_audit_report", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "fastag_entries",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ticket_id = table.Column<string>(nullable: true),
                    entry_lane_id = table.Column<string>(nullable: true),
                    entry_time = table.Column<long>(nullable: true),
                    entry_time_converted = table.Column<DateTime>(nullable: true),
                    entry_response_status_code = table.Column<string>(nullable: true),
                    entry_response_status_desc = table.Column<string>(nullable: true),
                    vehicle_number = table.Column<string>(nullable: true),
                    entry_response_time = table.Column<long>(nullable: true),
                    entry_response_time_converted = table.Column<DateTime>(nullable: true),
                    entry_udp_message = table.Column<string>(nullable: true),
                    last_modified_utc = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_fastag_entries", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "fastag_payments",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ticket_id = table.Column<string>(nullable: true),
                    entry_lane_id = table.Column<string>(nullable: true),
                    exit_lane_id = table.Column<string>(nullable: true),
                    entry_time = table.Column<long>(nullable: true),
                    entry_time_converted = table.Column<DateTime>(nullable: true),
                    exit_time = table.Column<long>(nullable: true),
                    exit_time_converted = table.Column<DateTime>(nullable: true),
                    npci_transaction_id = table.Column<string>(nullable: true),
                    epc_id = table.Column<string>(nullable: true),
                    txn_amount = table.Column<decimal>(nullable: false),
                    fastag_response_status_code = table.Column<string>(nullable: true),
                    fastag_response_status_desc = table.Column<string>(nullable: true),
                    payment_udp_message = table.Column<string>(nullable: true),
                    last_modified_utc = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_fastag_payments", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "configs");

            migrationBuilder.DropTable(
                name: "fastag_audit_report");

            migrationBuilder.DropTable(
                name: "fastag_entries");

            migrationBuilder.DropTable(
                name: "fastag_payments");
        }
    }
}
