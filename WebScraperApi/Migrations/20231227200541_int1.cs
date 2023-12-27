using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebScraperApi.Migrations
{
    /// <inheritdoc />
    public partial class int1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "tenderIdString",
                table: "GetRelationsDetails",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_GetRelationsDetails_tenderIdString",
                table: "GetRelationsDetails",
                column: "tenderIdString");

            migrationBuilder.AddForeignKey(
                name: "FK_GetRelationsDetails_CardBasicDatas_tenderIdString",
                table: "GetRelationsDetails",
                column: "tenderIdString",
                principalTable: "CardBasicDatas",
                principalColumn: "tenderIdString",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GetRelationsDetails_CardBasicDatas_tenderIdString",
                table: "GetRelationsDetails");

            migrationBuilder.DropIndex(
                name: "IX_GetRelationsDetails_tenderIdString",
                table: "GetRelationsDetails");

            migrationBuilder.AlterColumn<string>(
                name: "tenderIdString",
                table: "GetRelationsDetails",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
