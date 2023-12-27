namespace WebScraperApi.Models
{
    public class GetTenderDates
    {
        public DateOnly? lastEnqueriesDate { get; set; }
        public string lastEnqueriesDateHijri { get; set; } = string.Empty;
        public DateOnly? lastOfferPresentationDate { get; set; }
        public TimeOnly? lastOfferPresentationTime { get; set; }
        public string lastOfferPresentationDateHijri { get; set; } = string.Empty;
        public DateTime? offersOpeningDate { get; set; }
        public string offersOpeningDateHijri { get; set; } = string.Empty;
        public DateOnly? offersExaminationDate { get; set; }
        public TimeOnly? offersExaminationTime { get; set; }
        public string offersExaminationDateHijri { get; set; } = string.Empty;
        public DateTime? expectedAwardDate { get; set; }
        public string expectedAwardDateHijri { get; set; } = string.Empty;
        public DateTime? businessStartDate { get; set; }
        public string businessStartDateHijri { get; set; } = string.Empty;
        public DateTime? participationConfirmationLetterDate { get; set; }
        public string participationConfirmationLetterDateHijri { get; set; } = string.Empty;
        public DateTime? sendingInquiriesDate { get; set; }
        public string sendingInquiriesDateHijri { get; set; } = string.Empty;
        public string offersOpeningLocation { get; set; } = string.Empty;
        public int AnswerInquiriesInDays { get; set; }
    }
}
