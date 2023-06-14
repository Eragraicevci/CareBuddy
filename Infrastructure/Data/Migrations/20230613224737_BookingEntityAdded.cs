using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class BookingEntityAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppointmentTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ShortName = table.Column<string>(type: "TEXT", nullable: true),
                    Time = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Price = table.Column<double>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PatientUsername = table.Column<string>(type: "TEXT", nullable: true),
                    BookedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    InfoFirstName = table.Column<string>(name: "Info_FirstName", type: "TEXT", nullable: true),
                    InfoLastName = table.Column<string>(name: "Info_LastName", type: "TEXT", nullable: true),
                    InfoContactInfo = table.Column<string>(name: "Info_ContactInfo", type: "TEXT", nullable: true),
                    InfoAddress = table.Column<string>(name: "Info_Address", type: "TEXT", nullable: true),
                    InfoMedicalHistory = table.Column<string>(name: "Info_MedicalHistory", type: "TEXT", nullable: true),
                    InfoCurrentMedication = table.Column<string>(name: "Info_CurrentMedication", type: "TEXT", nullable: true),
                    InfoSymptoms = table.Column<string>(name: "Info_Symptoms", type: "TEXT", nullable: true),
                    AppointmentTypeId = table.Column<int>(type: "INTEGER", nullable: true),
                    Subtotal = table.Column<double>(type: "REAL", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false),
                    PaymentIntentId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookings_AppointmentTypes_AppointmentTypeId",
                        column: x => x.AppointmentTypeId,
                        principalTable: "AppointmentTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BookingItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    ItemBookedServiceItemId = table.Column<int>(name: "ItemBooked_ServiceItemId", type: "INTEGER", nullable: true),
                    ItemBookedServiceName = table.Column<string>(name: "ItemBooked_ServiceName", type: "TEXT", nullable: true),
                    ItemBookedPictureUrl = table.Column<string>(name: "ItemBooked_PictureUrl", type: "TEXT", nullable: true),
                    Price = table.Column<double>(type: "decimal(18,2)", nullable: false),
                    Capacity = table.Column<int>(type: "INTEGER", nullable: false),
                    BookingId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookingItems_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookingItems_BookingId",
                table: "BookingItems",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_AppointmentTypeId",
                table: "Bookings",
                column: "AppointmentTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingItems");

            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "AppointmentTypes");
        }
    }
}
