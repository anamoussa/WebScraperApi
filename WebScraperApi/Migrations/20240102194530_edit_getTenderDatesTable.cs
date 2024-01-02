using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebScraperApi.Migrations
{
    /// <inheritdoc />
    public partial class edit_getTenderDatesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "lastEnqueriesDate",
                table: "GetTenderDates");

            migrationBuilder.DropColumn(
                name: "lastEnqueriesDateHijri",
                table: "GetTenderDates");

            migrationBuilder.DropColumn(
                name: "lastOfferPresentationDate",
                table: "GetTenderDates");

            migrationBuilder.DropColumn(
                name: "lastOfferPresentationDateHijri",
                table: "GetTenderDates");

            migrationBuilder.DropColumn(
                name: "lastOfferPresentationTime",
                table: "GetTenderDates");

            migrationBuilder.DropColumn(
                name: "offersExaminationDate",
                table: "GetTenderDates");

            migrationBuilder.DropColumn(
                name: "offersExaminationDateHijri",
                table: "GetTenderDates");

            migrationBuilder.DropColumn(
                name: "offersExaminationTime",
                table: "GetTenderDates");

            migrationBuilder.DropColumn(
                name: "offersOpeningDate",
                table: "GetTenderDates");

            migrationBuilder.DropColumn(
                name: "offersOpeningDateHijri",
                table: "GetTenderDates");

            migrationBuilder.AlterColumn<string>(
                name: "Supplier_name",
                table: "offerApplicants",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Supplier_name",
                table: "offerApplicants",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateOnly>(
                name: "lastEnqueriesDate",
                table: "GetTenderDates",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "lastEnqueriesDateHijri",
                table: "GetTenderDates",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateOnly>(
                name: "lastOfferPresentationDate",
                table: "GetTenderDates",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "lastOfferPresentationDateHijri",
                table: "GetTenderDates",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<TimeOnly>(
                name: "lastOfferPresentationTime",
                table: "GetTenderDates",
                type: "time",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "offersExaminationDate",
                table: "GetTenderDates",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "offersExaminationDateHijri",
                table: "GetTenderDates",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<TimeOnly>(
                name: "offersExaminationTime",
                table: "GetTenderDates",
                type: "time",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "offersOpeningDate",
                table: "GetTenderDates",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "offersOpeningDateHijri",
                table: "GetTenderDates",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
