using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebScraperApi.Migrations
{
    /// <inheritdoc />
    public partial class int0 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CardBasicDatas",
                columns: table => new
                {
                    tenderIdString = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    tenderId = table.Column<int>(type: "int", nullable: false),
                    referenceNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tenderName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tenderNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    multipleSearch = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    agencyCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    branchId = table.Column<int>(type: "int", nullable: false),
                    branchName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    agencyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tenderStatusId = table.Column<int>(type: "int", nullable: false),
                    tenderStatusIdString = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tenderStatusName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tenderTypeId = table.Column<int>(type: "int", nullable: false),
                    tenderTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    technicalOrganizationId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    condetionalBookletPrice = table.Column<float>(type: "real", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    lastEnqueriesDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    lastOfferPresentationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    offersOpeningDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    lastEnqueriesDateHijri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    offersOpeningDateHijri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lastOfferPresentationDateHijri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    insideKSA = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tenderActivityName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tenderActivityNameList = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tenderActivityId = table.Column<int>(type: "int", nullable: false),
                    submitionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    financialFees = table.Column<float>(type: "real", nullable: false),
                    invitationCost = table.Column<float>(type: "real", nullable: false),
                    buyingCost = table.Column<float>(type: "real", nullable: false),
                    hasInvitations = table.Column<bool>(type: "bit", nullable: false),
                    remainingDays = table.Column<int>(type: "int", nullable: false),
                    remainingHours = table.Column<int>(type: "int", nullable: false),
                    remainingMins = table.Column<int>(type: "int", nullable: false),
                    currentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    currentDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    currentTime = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardBasicDatas", x => x.tenderIdString);
                });

            migrationBuilder.CreateTable(
                name: "GetRelationsDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tenderIdString = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExecutionLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompetitionActivity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SupplyItemsCompetition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConstructionWorks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaintenanceAndOperationWorks = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GetRelationsDetails", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CardBasicDatas");

            migrationBuilder.DropTable(
                name: "GetRelationsDetails");
        }
    }
}
