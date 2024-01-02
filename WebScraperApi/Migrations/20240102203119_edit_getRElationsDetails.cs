using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebScraperApi.Migrations
{
    /// <inheritdoc />
    public partial class edit_getRElationsDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConstructionWorks",
                table: "GetRelationsDetails");

            migrationBuilder.DropColumn(
                name: "MaintenanceAndOperationWorks",
                table: "GetRelationsDetails");

            migrationBuilder.CreateTable(
                name: "ConstructionWorks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GetRelationsDetailId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConstructionWorks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConstructionWorks_GetRelationsDetails_GetRelationsDetailId",
                        column: x => x.GetRelationsDetailId,
                        principalTable: "GetRelationsDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaintenanceAndOperationWorks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GetRelationsDetailId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaintenanceAndOperationWorks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaintenanceAndOperationWorks_GetRelationsDetails_GetRelationsDetailId",
                        column: x => x.GetRelationsDetailId,
                        principalTable: "GetRelationsDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConstructionWorks_GetRelationsDetailId",
                table: "ConstructionWorks",
                column: "GetRelationsDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceAndOperationWorks_GetRelationsDetailId",
                table: "MaintenanceAndOperationWorks",
                column: "GetRelationsDetailId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConstructionWorks");

            migrationBuilder.DropTable(
                name: "MaintenanceAndOperationWorks");

            migrationBuilder.AddColumn<string>(
                name: "ConstructionWorks",
                table: "GetRelationsDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaintenanceAndOperationWorks",
                table: "GetRelationsDetails",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
