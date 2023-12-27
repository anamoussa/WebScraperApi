using System.ComponentModel.DataAnnotations;

namespace WebScraperApi.Models
{
    public class CardBasicData
    {
        [Key]
        public string tenderIdString { get; set; }

        public int tenderId { get; set; }
        public string referenceNumber { get; set; }
        public string tenderName { get; set; }
        public string tenderNumber { get; set; }
        public string multipleSearch { get; set; }
        public string agencyCode { get; set; }
        public int branchId { get; set; }
        public string branchName { get; set; }
        public string agencyName { get; set; }
        public int tenderStatusId { get; set; }
        public string tenderStatusIdString { get; set; }
        public string tenderStatusName { get; set; }
        public int tenderTypeId { get; set; }
        public string tenderTypeName { get; set; }
        public string technicalOrganizationId { get; set; }
        public float condetionalBookletPrice { get; set; }
        public DateTime? createdAt { get; set; }
        public DateTime? lastEnqueriesDate { get; set; }
        public DateTime? lastOfferPresentationDate { get; set; }
        public DateTime? offersOpeningDate { get; set; }
        public string lastEnqueriesDateHijri { get; set; }
        public string offersOpeningDateHijri { get; set; }
        public string lastOfferPresentationDateHijri { get; set; }
        public string insideKSA { get; set; }
        public string tenderActivityName { get; set; }
        public string tenderActivityNameList { get; set; }
        public int tenderActivityId { get; set; }
        public DateTime? submitionDate { get; set; }
        public float financialFees { get; set; }
        public float invitationCost { get; set; }
        public float buyingCost { get; set; }
        public bool hasInvitations { get; set; }
        public int remainingDays { get; set; }
        public int remainingHours { get; set; }
        public int remainingMins { get; set; }
        public DateTime? currentDate { get; set; }
        public DateTime? currentDateTime { get; set; }
        public string currentTime { get; set; }



    }
}
//public string tenderIdString { get; set; }

//public int tenderId { get; set; }
//public string referenceNumber { get; set; }
//public string tenderName { get; set; }
//public string tenderNumber { get; set; }
//public string multipleSearch { get; set; }
//public string agencyCode { get; set; }
//public int branchId { get; set; }
//public string branchName { get; set; }
//public string agencyName { get; set; }
//public int tenderStatusId { get; set; }
//public string tenderStatusIdString { get; set; }
//public string tenderStatusName { get; set; }
//public int tenderTypeId { get; set; }
//public string tenderTypeName { get; set; }
//public string technicalOrganizationId { get; set; }
//public float condetionalBookletPrice { get; set; }
//public DateTime? createdAt { get; set; }
//public DateTime? lastEnqueriesDate { get; set; }
//public DateTime? lastOfferPresentationDate { get; set; }
//public DateTime? offersOpeningDate { get; set; }
//public string lastEnqueriesDateHijri { get; set; }
//public string offersOpeningDateHijri { get; set; }
//public string lastOfferPresentationDateHijri { get; set; }
//public string insideKSA { get; set; }
//public string tenderActivityName { get; set; }
//public string tenderActivityNameList { get; set; }
//public int tenderActivityId { get; set; }
//public DateTime? submitionDate { get; set; }
//public float financialFees { get; set; }
//public float invitationCost { get; set; }
//public float buyingCost { get; set; }
//public bool hasInvitations { get; set; }
//public int remainingDays { get; set; }
//public int remainingHours { get; set; }
//public int remainingMins { get; set; }
//public DateTime? currentDate { get; set; }
//public DateTime? currentDateTime { get; set; }
//public string currentTime { get; set; }