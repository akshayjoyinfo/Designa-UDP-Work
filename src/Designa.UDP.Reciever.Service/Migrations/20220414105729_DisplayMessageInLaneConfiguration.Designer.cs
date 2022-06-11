﻿// <auto-generated />
using System;
using Designa.UDP.Reciever.Service.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Designa.UDP.Reciever.Service.Migrations
{
    [DbContext(typeof(UDPDbContext))]
    [Migration("20220414105729_DisplayMessageInLaneConfiguration")]
    partial class DisplayMessageInLaneConfiguration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Designa.UDP.Reciever.Service.Persistence.Entities.Config", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ConfigKey")
                        .HasColumnName("config_key")
                        .HasColumnType("text");

                    b.Property<string>("ConfigValue")
                        .HasColumnName("config_value")
                        .HasColumnType("text");

                    b.HasKey("Id")
                        .HasName("pk_configs");

                    b.ToTable("configs");
                });

            modelBuilder.Entity("Designa.UDP.Reciever.Service.Persistence.Entities.FastagAuditReport", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("CardTagNo")
                        .HasColumnName("card_tag_no")
                        .HasColumnType("text");

                    b.Property<DateTime?>("DateTimeOfEntry")
                        .HasColumnName("date_time_of_entry")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DateTimeOfExit")
                        .HasColumnName("date_time_of_exit")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("EntryDeviceName")
                        .HasColumnName("entry_device_name")
                        .HasColumnType("text");

                    b.Property<string>("ExitDeviceName")
                        .HasColumnName("exit_device_name")
                        .HasColumnType("text");

                    b.Property<DateTime?>("LastModifiedUtc")
                        .HasColumnName("last_modified_utc")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("ModeOfPayment")
                        .HasColumnName("mode_of_payment")
                        .HasColumnType("text");

                    b.Property<decimal>("ParkingFeeGross")
                        .HasColumnName("parking_fee_gross")
                        .HasColumnType("numeric");

                    b.Property<decimal>("ParkingFeeNet")
                        .HasColumnName("parking_fee_net")
                        .HasColumnType("numeric");

                    b.Property<string>("ParkingNode")
                        .HasColumnName("parking_node")
                        .HasColumnType("text");

                    b.Property<string>("StayDuration")
                        .HasColumnName("stay_duration")
                        .HasColumnType("text");

                    b.Property<decimal>("Tax")
                        .HasColumnName("tax")
                        .HasColumnType("numeric");

                    b.Property<string>("TicketId")
                        .HasColumnName("ticket_id")
                        .HasColumnType("text");

                    b.Property<string>("TransactionId")
                        .HasColumnName("transaction_id")
                        .HasColumnType("text");

                    b.Property<string>("VehicleNumber")
                        .HasColumnName("vehicle_number")
                        .HasColumnType("text");

                    b.HasKey("Id")
                        .HasName("pk_fastag_audit_report");

                    b.ToTable("fastag_audit_report");
                });

            modelBuilder.Entity("Designa.UDP.Reciever.Service.Persistence.Entities.FastagEntryEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("EntryLaneId")
                        .HasColumnName("entry_lane_id")
                        .HasColumnType("text");

                    b.Property<string>("EntryResponseStatusCode")
                        .HasColumnName("entry_response_status_code")
                        .HasColumnType("text");

                    b.Property<string>("EntryResponseStatusDesc")
                        .HasColumnName("entry_response_status_desc")
                        .HasColumnType("text");

                    b.Property<long?>("EntryResponseTime")
                        .HasColumnName("entry_response_time")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("EntryResponseTimeConverted")
                        .HasColumnName("entry_response_time_converted")
                        .HasColumnType("timestamp without time zone");

                    b.Property<long?>("EntryTime")
                        .HasColumnName("entry_time")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("EntryTimeConverted")
                        .HasColumnName("entry_time_converted")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("EntryUdpMessage")
                        .HasColumnName("entry_udp_message")
                        .HasColumnType("text");

                    b.Property<DateTime>("LastModifiedUtc")
                        .HasColumnName("last_modified_utc")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("TicketId")
                        .HasColumnName("ticket_id")
                        .HasColumnType("text");

                    b.Property<string>("VehicleNumber")
                        .HasColumnName("vehicle_number")
                        .HasColumnType("text");

                    b.HasKey("Id")
                        .HasName("pk_fastag_entries");

                    b.ToTable("fastag_entries");
                });

            modelBuilder.Entity("Designa.UDP.Reciever.Service.Persistence.Entities.FastagPaymentEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("EntryLaneId")
                        .HasColumnName("entry_lane_id")
                        .HasColumnType("text");

                    b.Property<long?>("EntryTime")
                        .HasColumnName("entry_time")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("EntryTimeConverted")
                        .HasColumnName("entry_time_converted")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("EpcId")
                        .HasColumnName("epc_id")
                        .HasColumnType("text");

                    b.Property<string>("ExitLaneId")
                        .HasColumnName("exit_lane_id")
                        .HasColumnType("text");

                    b.Property<long?>("ExitTime")
                        .HasColumnName("exit_time")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("ExitTimeConverted")
                        .HasColumnName("exit_time_converted")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("FastagResponseStatusCode")
                        .HasColumnName("fastag_response_status_code")
                        .HasColumnType("text");

                    b.Property<string>("FastagResponseStatusDesc")
                        .HasColumnName("fastag_response_status_desc")
                        .HasColumnType("text");

                    b.Property<DateTime?>("LastModifiedUtc")
                        .HasColumnName("last_modified_utc")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("NpciTransactionId")
                        .HasColumnName("npci_transaction_id")
                        .HasColumnType("text");

                    b.Property<string>("PaymentUdpMessage")
                        .HasColumnName("payment_udp_message")
                        .HasColumnType("text");

                    b.Property<string>("TicketId")
                        .HasColumnName("ticket_id")
                        .HasColumnType("text");

                    b.Property<decimal>("TxnAmount")
                        .HasColumnName("txn_amount")
                        .HasColumnType("numeric");

                    b.HasKey("Id")
                        .HasName("pk_fastag_payments");

                    b.ToTable("fastag_payments");
                });

            modelBuilder.Entity("Designa.UDP.Reciever.Service.Persistence.Entities.LaneConfiguration", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("DisplayResetMessage")
                        .HasColumnName("display_reset_message")
                        .HasColumnType("text");

                    b.Property<string>("Ip")
                        .HasColumnName("ip")
                        .HasColumnType("text");

                    b.Property<string>("LaneId")
                        .HasColumnName("lane_id")
                        .HasColumnType("text");

                    b.Property<DateTime?>("LastModifiedUtc")
                        .HasColumnName("last_modified_utc")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id")
                        .HasName("pk_lane_configurations");

                    b.ToTable("lane_configurations");
                });
#pragma warning restore 612, 618
        }
    }
}
