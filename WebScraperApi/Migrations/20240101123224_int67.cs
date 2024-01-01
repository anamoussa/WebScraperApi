using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebScraperApi.Migrations
{
    /// <inheritdoc />
    public partial class int67 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetailsForVisitors_CardBasicDatas_tenderIdString",
                table: "DetailsForVisitors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DetailsForVisitors",
                table: "DetailsForVisitors");

            migrationBuilder.RenameTable(
                name: "DetailsForVisitors",
                newName: "GetDetailsForVisitor");

            migrationBuilder.RenameIndex(
                name: "IX_DetailsForVisitors_tenderIdString",
                table: "GetDetailsForVisitor",
                newName: "IX_GetDetailsForVisitor_tenderIdString");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GetDetailsForVisitor",
                table: "GetDetailsForVisitor",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GetDetailsForVisitor_CardBasicDatas_tenderIdString",
                table: "GetDetailsForVisitor",
                column: "tenderIdString",
                principalTable: "CardBasicDatas",
                principalColumn: "tenderIdString",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GetDetailsForVisitor_CardBasicDatas_tenderIdString",
                table: "GetDetailsForVisitor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GetDetailsForVisitor",
                table: "GetDetailsForVisitor");

            migrationBuilder.RenameTable(
                name: "GetDetailsForVisitor",
                newName: "DetailsForVisitors");

            migrationBuilder.RenameIndex(
                name: "IX_GetDetailsForVisitor_tenderIdString",
                table: "DetailsForVisitors",
                newName: "IX_DetailsForVisitors_tenderIdString");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DetailsForVisitors",
                table: "DetailsForVisitors",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DetailsForVisitors_CardBasicDatas_tenderIdString",
                table: "DetailsForVisitors",
                column: "tenderIdString",
                principalTable: "CardBasicDatas",
                principalColumn: "tenderIdString",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
