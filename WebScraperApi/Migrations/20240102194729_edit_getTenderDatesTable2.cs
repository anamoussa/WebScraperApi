using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebScraperApi.Migrations
{
    /// <inheritdoc />
    public partial class edit_getTenderDatesTable2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "participationConfirmationLetterDateHijri",
                table: "GetTenderDates");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "participationConfirmationLetterDateHijri",
                table: "GetTenderDates",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
