﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebScraperApi.Models.Data;

#nullable disable

namespace WebScraperApi.Migrations
{
    [DbContext(typeof(ScrapDBContext))]
    [Migration("20240105104547_AnswerInquiriesInDaysNullable")]
    partial class AnswerInquiriesInDaysNullable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WebScraperApi.Models.AwardingResult.AwardedSupplier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("Award_value")
                        .HasColumnType("float");

                    b.Property<double>("Financial_offer")
                        .HasColumnType("float");

                    b.Property<int>("GetAwardingResultId")
                        .HasColumnType("int");

                    b.Property<string>("Supplier_name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("GetAwardingResultId");

                    b.ToTable("AwardedSuppliers");
                });

            modelBuilder.Entity("WebScraperApi.Models.AwardingResult.GetAwardingResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("tenderIdString")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("tenderIdString");

                    b.ToTable("GetAwardingResults");
                });

            modelBuilder.Entity("WebScraperApi.Models.AwardingResult.OfferApplicant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("Financial_offer")
                        .HasColumnType("float");

                    b.Property<int>("GetAwardingResultId")
                        .HasColumnType("int");

                    b.Property<bool>("IsAccepted")
                        .HasColumnType("bit");

                    b.Property<string>("Supplier_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("GetAwardingResultId");

                    b.ToTable("offerApplicants");
                });

            modelBuilder.Entity("WebScraperApi.Models.CardBasicData", b =>
                {
                    b.Property<string>("tenderIdString")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("agencyCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("agencyName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("branchId")
                        .HasColumnType("int");

                    b.Property<string>("branchName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("buyingCost")
                        .HasColumnType("real");

                    b.Property<float>("condetionalBookletPrice")
                        .HasColumnType("real");

                    b.Property<DateTime?>("createdAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("currentDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("currentDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("currentTime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("financialFees")
                        .HasColumnType("real");

                    b.Property<bool>("hasInvitations")
                        .HasColumnType("bit");

                    b.Property<string>("insideKSA")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("invitationCost")
                        .HasColumnType("real");

                    b.Property<DateTime?>("lastEnqueriesDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("lastEnqueriesDateHijri")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("lastOfferPresentationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("lastOfferPresentationDateHijri")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("multipleSearch")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("offersOpeningDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("offersOpeningDateHijri")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("referenceNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("remainingDays")
                        .HasColumnType("int");

                    b.Property<int>("remainingHours")
                        .HasColumnType("int");

                    b.Property<int>("remainingMins")
                        .HasColumnType("int");

                    b.Property<DateTime?>("submitionDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("technicalOrganizationId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("tenderActivityId")
                        .HasColumnType("int");

                    b.Property<string>("tenderActivityName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("tenderActivityNameList")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("tenderId")
                        .HasColumnType("int");

                    b.Property<string>("tenderName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("tenderNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("tenderStatusId")
                        .HasColumnType("int");

                    b.Property<string>("tenderStatusIdString")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("tenderStatusName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("tenderTypeId")
                        .HasColumnType("int");

                    b.Property<string>("tenderTypeName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("tenderIdString");

                    b.ToTable("CardBasicDatas");
                });

            modelBuilder.Entity("WebScraperApi.Models.GetDetailsForVisitor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AwardNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CompetitionPurpose")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CompetitionStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("ContractDuration")
                        .HasColumnType("float");

                    b.Property<double?>("FinalGuarantee")
                        .HasColumnType("float");

                    b.Property<bool>("IsInsuranceRequired")
                        .HasColumnType("bit");

                    b.Property<bool>("IsPreliminaryGuaranteeRequired")
                        .HasColumnType("bit");

                    b.Property<string>("OfferingMethod")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PreliminaryGuaranteeAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("tenderIdString")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("tenderIdString");

                    b.ToTable("GetDetailsForVisitor");
                });

            modelBuilder.Entity("WebScraperApi.Models.GetTenderDate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("AnswerInquiriesInDays")
                        .HasColumnType("int");

                    b.Property<DateTime?>("businessStartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("businessStartDateHijri")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("expectedAwardDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("expectedAwardDateHijri")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("offersOpeningLocation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("participationConfirmationLetterDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("sendingInquiriesDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("sendingInquiriesDateHijri")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("tenderIdString")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("tenderIdString");

                    b.ToTable("GetTenderDates");
                });

            modelBuilder.Entity("WebScraperApi.Models.RelationsDetail.CompetitionActivity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("GetRelationsDetailId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("GetRelationsDetailId");

                    b.ToTable("CompetitionActivities");
                });

            modelBuilder.Entity("WebScraperApi.Models.RelationsDetail.ConstructionWork", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("GetRelationsDetailId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("GetRelationsDetailId");

                    b.ToTable("ConstructionWorks");
                });

            modelBuilder.Entity("WebScraperApi.Models.RelationsDetail.ExecutionLocation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GetRelationsDetailId")
                        .HasColumnType("int");

                    b.Property<bool>("InSideKingdom")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("GetRelationsDetailId");

                    b.ToTable("ExecutionLocations");
                });

            modelBuilder.Entity("WebScraperApi.Models.RelationsDetail.GetRelationsDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Details")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SupplyItemsCompetition")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("tenderIdString")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("tenderIdString");

                    b.ToTable("GetRelationsDetails");
                });

            modelBuilder.Entity("WebScraperApi.Models.RelationsDetail.MaintenanceAndOperationWork", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("GetRelationsDetailId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("GetRelationsDetailId");

                    b.ToTable("MaintenanceAndOperationWorks");
                });

            modelBuilder.Entity("WebScraperApi.Models.RelationsDetail.Region", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ExecutionLocationId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ExecutionLocationId");

                    b.ToTable("Regions");
                });

            modelBuilder.Entity("WebScraperApi.Models.AwardingResult.AwardedSupplier", b =>
                {
                    b.HasOne("WebScraperApi.Models.AwardingResult.GetAwardingResult", "GetAwardingResult")
                        .WithMany("awardedSuppliers")
                        .HasForeignKey("GetAwardingResultId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GetAwardingResult");
                });

            modelBuilder.Entity("WebScraperApi.Models.AwardingResult.GetAwardingResult", b =>
                {
                    b.HasOne("WebScraperApi.Models.CardBasicData", "CardBasicData")
                        .WithMany()
                        .HasForeignKey("tenderIdString")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CardBasicData");
                });

            modelBuilder.Entity("WebScraperApi.Models.AwardingResult.OfferApplicant", b =>
                {
                    b.HasOne("WebScraperApi.Models.AwardingResult.GetAwardingResult", "GetAwardingResult")
                        .WithMany("offerApplicants")
                        .HasForeignKey("GetAwardingResultId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GetAwardingResult");
                });

            modelBuilder.Entity("WebScraperApi.Models.GetDetailsForVisitor", b =>
                {
                    b.HasOne("WebScraperApi.Models.CardBasicData", "CardBasicData")
                        .WithMany()
                        .HasForeignKey("tenderIdString")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CardBasicData");
                });

            modelBuilder.Entity("WebScraperApi.Models.GetTenderDate", b =>
                {
                    b.HasOne("WebScraperApi.Models.CardBasicData", "CardBasicData")
                        .WithMany()
                        .HasForeignKey("tenderIdString")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CardBasicData");
                });

            modelBuilder.Entity("WebScraperApi.Models.RelationsDetail.CompetitionActivity", b =>
                {
                    b.HasOne("WebScraperApi.Models.RelationsDetail.GetRelationsDetail", "GetRelationsDetail")
                        .WithMany("CompetitionActivities")
                        .HasForeignKey("GetRelationsDetailId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GetRelationsDetail");
                });

            modelBuilder.Entity("WebScraperApi.Models.RelationsDetail.ConstructionWork", b =>
                {
                    b.HasOne("WebScraperApi.Models.RelationsDetail.GetRelationsDetail", "GetRelationsDetail")
                        .WithMany("ConstructionWorks")
                        .HasForeignKey("GetRelationsDetailId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GetRelationsDetail");
                });

            modelBuilder.Entity("WebScraperApi.Models.RelationsDetail.ExecutionLocation", b =>
                {
                    b.HasOne("WebScraperApi.Models.RelationsDetail.GetRelationsDetail", "GetRelationsDetail")
                        .WithMany("ExecutionLocations")
                        .HasForeignKey("GetRelationsDetailId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GetRelationsDetail");
                });

            modelBuilder.Entity("WebScraperApi.Models.RelationsDetail.GetRelationsDetail", b =>
                {
                    b.HasOne("WebScraperApi.Models.CardBasicData", "CardBasicData")
                        .WithMany()
                        .HasForeignKey("tenderIdString")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CardBasicData");
                });

            modelBuilder.Entity("WebScraperApi.Models.RelationsDetail.MaintenanceAndOperationWork", b =>
                {
                    b.HasOne("WebScraperApi.Models.RelationsDetail.GetRelationsDetail", "GetRelationsDetail")
                        .WithMany("MaintenanceAndOperationWorks")
                        .HasForeignKey("GetRelationsDetailId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GetRelationsDetail");
                });

            modelBuilder.Entity("WebScraperApi.Models.RelationsDetail.Region", b =>
                {
                    b.HasOne("WebScraperApi.Models.RelationsDetail.ExecutionLocation", "ExecutionLocation")
                        .WithMany("Regions")
                        .HasForeignKey("ExecutionLocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ExecutionLocation");
                });

            modelBuilder.Entity("WebScraperApi.Models.AwardingResult.GetAwardingResult", b =>
                {
                    b.Navigation("awardedSuppliers");

                    b.Navigation("offerApplicants");
                });

            modelBuilder.Entity("WebScraperApi.Models.RelationsDetail.ExecutionLocation", b =>
                {
                    b.Navigation("Regions");
                });

            modelBuilder.Entity("WebScraperApi.Models.RelationsDetail.GetRelationsDetail", b =>
                {
                    b.Navigation("CompetitionActivities");

                    b.Navigation("ConstructionWorks");

                    b.Navigation("ExecutionLocations");

                    b.Navigation("MaintenanceAndOperationWorks");
                });
#pragma warning restore 612, 618
        }
    }
}
