using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebScraperApi.Migrations
{
    /// <inheritdoc />
    public partial class int66 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompetitionActivity",
                table: "GetRelationsDetails");

            migrationBuilder.DropColumn(
                name: "ExecutionLocation",
                table: "GetRelationsDetails");

            migrationBuilder.AlterColumn<string>(
                name: "Supplier_name",
                table: "offerApplicants",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "AwardNumber",
                table: "DetailsForVisitors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompetitionPurpose",
                table: "DetailsForVisitors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompetitionStatus",
                table: "DetailsForVisitors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "ContractDuration",
                table: "DetailsForVisitors",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "FinalGuarantee",
                table: "DetailsForVisitors",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<bool>(
                name: "IsInsuranceRequired",
                table: "DetailsForVisitors",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPreliminaryGuaranteeRequired",
                table: "DetailsForVisitors",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "OfferingMethod",
                table: "DetailsForVisitors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PreliminaryGuaranteeAddress",
                table: "DetailsForVisitors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Supplier_name",
                table: "AwardedSuppliers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "CompetitionActivities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GetRelationsDetailId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetitionActivities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompetitionActivities_GetRelationsDetails_GetRelationsDetailId",
                        column: x => x.GetRelationsDetailId,
                        principalTable: "GetRelationsDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExecutionLocations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InSideKingdom = table.Column<bool>(type: "bit", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GetRelationsDetailId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExecutionLocations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExecutionLocations_GetRelationsDetails_GetRelationsDetailId",
                        column: x => x.GetRelationsDetailId,
                        principalTable: "GetRelationsDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExecutionLocationId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Regions_ExecutionLocations_ExecutionLocationId",
                        column: x => x.ExecutionLocationId,
                        principalTable: "ExecutionLocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompetitionActivities_GetRelationsDetailId",
                table: "CompetitionActivities",
                column: "GetRelationsDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_ExecutionLocations_GetRelationsDetailId",
                table: "ExecutionLocations",
                column: "GetRelationsDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_Regions_ExecutionLocationId",
                table: "Regions",
                column: "ExecutionLocationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompetitionActivities");

            migrationBuilder.DropTable(
                name: "Regions");

            migrationBuilder.DropTable(
                name: "ExecutionLocations");

            migrationBuilder.DropColumn(
                name: "AwardNumber",
                table: "DetailsForVisitors");

            migrationBuilder.DropColumn(
                name: "CompetitionPurpose",
                table: "DetailsForVisitors");

            migrationBuilder.DropColumn(
                name: "CompetitionStatus",
                table: "DetailsForVisitors");

            migrationBuilder.DropColumn(
                name: "ContractDuration",
                table: "DetailsForVisitors");

            migrationBuilder.DropColumn(
                name: "FinalGuarantee",
                table: "DetailsForVisitors");

            migrationBuilder.DropColumn(
                name: "IsInsuranceRequired",
                table: "DetailsForVisitors");

            migrationBuilder.DropColumn(
                name: "IsPreliminaryGuaranteeRequired",
                table: "DetailsForVisitors");

            migrationBuilder.DropColumn(
                name: "OfferingMethod",
                table: "DetailsForVisitors");

            migrationBuilder.DropColumn(
                name: "PreliminaryGuaranteeAddress",
                table: "DetailsForVisitors");

            migrationBuilder.AlterColumn<string>(
                name: "Supplier_name",
                table: "offerApplicants",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompetitionActivity",
                table: "GetRelationsDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExecutionLocation",
                table: "GetRelationsDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Supplier_name",
                table: "AwardedSuppliers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
