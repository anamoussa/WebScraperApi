using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebScraperApi.Migrations
{
    /// <inheritdoc />
    public partial class int2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DetailsForVisitors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tenderIdString = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetailsForVisitors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetailsForVisitors_CardBasicDatas_tenderIdString",
                        column: x => x.tenderIdString,
                        principalTable: "CardBasicDatas",
                        principalColumn: "tenderIdString",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GetAwardingResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tenderIdString = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GetAwardingResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GetAwardingResults_CardBasicDatas_tenderIdString",
                        column: x => x.tenderIdString,
                        principalTable: "CardBasicDatas",
                        principalColumn: "tenderIdString",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GetTenderDates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tenderIdString = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    lastEnqueriesDate = table.Column<DateOnly>(type: "date", nullable: true),
                    lastEnqueriesDateHijri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lastOfferPresentationDate = table.Column<DateOnly>(type: "date", nullable: true),
                    lastOfferPresentationTime = table.Column<TimeOnly>(type: "time", nullable: true),
                    lastOfferPresentationDateHijri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    offersOpeningDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    offersOpeningDateHijri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    offersExaminationDate = table.Column<DateOnly>(type: "date", nullable: true),
                    offersExaminationTime = table.Column<TimeOnly>(type: "time", nullable: true),
                    offersExaminationDateHijri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    expectedAwardDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    expectedAwardDateHijri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    businessStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    businessStartDateHijri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    participationConfirmationLetterDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    participationConfirmationLetterDateHijri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sendingInquiriesDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    sendingInquiriesDateHijri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    offersOpeningLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnswerInquiriesInDays = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GetTenderDates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GetTenderDates_CardBasicDatas_tenderIdString",
                        column: x => x.tenderIdString,
                        principalTable: "CardBasicDatas",
                        principalColumn: "tenderIdString",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AwardedSuppliers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GetAwardingResultId = table.Column<int>(type: "int", nullable: false),
                    Supplier_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Financial_offer = table.Column<double>(type: "float", nullable: false),
                    Award_value = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AwardedSuppliers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AwardedSuppliers_GetAwardingResults_GetAwardingResultId",
                        column: x => x.GetAwardingResultId,
                        principalTable: "GetAwardingResults",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "offerApplicants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GetAwardingResultId = table.Column<int>(type: "int", nullable: false),
                    Supplier_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Financial_offer = table.Column<double>(type: "float", nullable: false),
                    IsAccepted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_offerApplicants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_offerApplicants_GetAwardingResults_GetAwardingResultId",
                        column: x => x.GetAwardingResultId,
                        principalTable: "GetAwardingResults",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AwardedSuppliers_GetAwardingResultId",
                table: "AwardedSuppliers",
                column: "GetAwardingResultId");

            migrationBuilder.CreateIndex(
                name: "IX_DetailsForVisitors_tenderIdString",
                table: "DetailsForVisitors",
                column: "tenderIdString");

            migrationBuilder.CreateIndex(
                name: "IX_GetAwardingResults_tenderIdString",
                table: "GetAwardingResults",
                column: "tenderIdString");

            migrationBuilder.CreateIndex(
                name: "IX_GetTenderDates_tenderIdString",
                table: "GetTenderDates",
                column: "tenderIdString");

            migrationBuilder.CreateIndex(
                name: "IX_offerApplicants_GetAwardingResultId",
                table: "offerApplicants",
                column: "GetAwardingResultId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AwardedSuppliers");

            migrationBuilder.DropTable(
                name: "DetailsForVisitors");

            migrationBuilder.DropTable(
                name: "GetTenderDates");

            migrationBuilder.DropTable(
                name: "offerApplicants");

            migrationBuilder.DropTable(
                name: "GetAwardingResults");
        }
    }
}
