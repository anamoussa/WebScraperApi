using System.Collections.ObjectModel;
using System.Drawing;

namespace WebScraperApi.Services.Concrete;

public class ScraperService : IScraperService
{
    private List<GetDetailsForVisitor> DetailsForVisitorsList = new();
    private List<GetTenderDate> GetTenderDatesList = new();
    private List<GetAwardingResult> GetAwardingResultsList = new();
    private List<GetRelationsDetail> GetRelationsDetailsList = new();

    private readonly ScrapDBContext _context;
    public ScraperService(ScrapDBContext context)
    {

        _context = context;
    }
    public void GetAllRelatedData(List<CardBasicData> CardsBasicData)
    {
        var tenderIDs = CardsBasicData.Select(t => t.tenderIdString).ToList();
        foreach (var tenderID in tenderIDs)
        {

            //GetDetailsForVisitor(tenderID);
            //GetTenderDates(tenderID);
            //GetAwardingResult(tenderID);
            GetRelationsDetails(tenderID);
            //if (!GetAwardingResultsList.Any())
            //{
            //    return;
            //}
            //_context.CardBasicDatas.AddRange(CardsBasicData);
            //_context.GetTenderDates.AddRange(GetTenderDatesList);
            //_context.GetRelationsDetails.AddRange(GetRelationsDetailsList);
            //_context.GetAwardingResults.AddRange(GetAwardingResultsList);
            //_context.SaveChanges();
        }
    }

    private void GetTenderDates(string tenderID)
    {
        var url = Constants.UrlGetTenderDates + tenderID;
        var chromeOptions = new ChromeOptions() { AcceptInsecureCertificates = true };
        chromeOptions.AddArguments("--headless=new"); // comment out for testing
        using (var driver = new ChromeDriver(chromeOptions))
        {
            driver.Navigate().GoToUrl(url);
            GetTenderDate tenderDates = new GetTenderDate();
            tenderDates.tenderIdString = tenderID;
            var dates = driver.FindElements(By.CssSelector("li.list-group-item"));
            foreach (var date in dates)
            {
                var title = date.FindElement(By.ClassName("etd-item-title"));
                var info = date.FindElement(By.ClassName("etd-item-info"));
                var txt = title.Text;
                var span = info.FindElements(By.TagName("span")).Select(o => o.Text).ToArray();
                try
                {
                    switch (txt)
                    {
                        #region un used Data
                        //// dublicated
                        //case "اخر موعد لإستلام الاستفسارات":
                        //    tenderDates.lastEnqueriesDate = span[0] == "لا يوجد" ? null : DateOnly.ParseExact(span[0], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        //    tenderDates.lastEnqueriesDateHijri = span[1];
                        //    break;
                        //// dublicated
                        //case "آخر موعد لتقديم العروض":
                        //    tenderDates.lastOfferPresentationDate = span[0] == "لا يوجد" ? null : DateOnly.ParseExact(span[0], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        //    tenderDates.lastOfferPresentationDateHijri = span[1];
                        //    tenderDates.lastOfferPresentationTime = span[2] == "لا يوجد" ? null : TimeOnly.ParseExact(span[2], "h:mm tt", CultureInfo.InvariantCulture);
                        //    break;
                        //// dublicated
                        //case "تاريخ فتح العروض":
                        //    tenderDates.offersOpeningDate = span[0] == "لا يوجد" ? null : DateTime.Parse(span[0]);
                        //    break;
                        //case "تاريخ فحص العروض":
                        //    if (span[0] == "لا يوجد")
                        //        break;
                        //    tenderDates.offersExaminationDate = span[0] == "لا يوجد" ? null : DateOnly.ParseExact(span[0], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        //    tenderDates.offersExaminationDateHijri = span[1];
                        //    tenderDates.offersExaminationTime = span[2] == "لا يوجد" ? null : TimeOnly.ParseExact(span[2], "h:mm tt", CultureInfo.InvariantCulture);
                        //    break; 
                        #endregion
                        case "التاريخ المتوقع للترسية":
                            if (span[0] == "لا يوجد")
                                break;
                            tenderDates.expectedAwardDate = span[0] == "لا يوجد" ? null : DateTime.ParseExact(span[0], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            tenderDates.expectedAwardDateHijri = span[1];
                            break;
                        case "تاريخ بدء الأعمال / الخدمات":
                            if (span[0] == "لا يوجد")
                                break;
                            tenderDates.businessStartDate = span[0] == "لا يوجد" ? null : DateTime.ParseExact(span[0], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            tenderDates.businessStartDateHijri = span[1];
                            break;
                        case "تاريخ استحقاق خطاب تأكيد المشاركة":
                            tenderDates.participationConfirmationLetterDate = span[0] == "لا يوجد" ? null : DateTime.ParseExact(span[0], "dd/MM/yyyy", CultureInfo.InvariantCulture); ;
                            break;
                        case "بداية إرسال الأسئلة و الاستفسارات":
                            if (span[0] == "لا يوجد")
                                break;
                            tenderDates.sendingInquiriesDate = span[0] == "لا يوجد" ? null : DateTime.ParseExact(span[0], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            tenderDates.sendingInquiriesDateHijri = span[1];
                            break;
                        case "أقصى مدة للإجابة على الإستفسارات(أيام)":
                            tenderDates.AnswerInquiriesInDays = int.Parse(span[0]);
                            break;
                        case "مكان فتح العرض":
                            tenderDates.offersOpeningLocation = span[0];
                            break;
                    }

                }
                catch (Exception ex)
                {

                    Console.WriteLine("Error_________" + ex.Message);
                }
            }
            GetTenderDatesList.Add(tenderDates);
            driver.Quit();
        }
    }
    private void GetAwardingResult(string tenderID)
    {
        GetAwardingResult awardingResult = new GetAwardingResult();
        awardingResult.tenderIdString = tenderID;
        var url = Constants.UrlGetAwardingResult + tenderID;
        var chromeOptions = new ChromeOptions() { AcceptInsecureCertificates = true };
        chromeOptions.AddArguments("--headless=new"); // comment out for testing
        using (var driver = new ChromeDriver(chromeOptions))
        {

            OfferApplicant offerApplicant = null;
            List<OfferApplicant> offerApplicants = new List<OfferApplicant>();

            AwardedSupplier awardedSupplier = null;
            List<AwardedSupplier> awardedSuppliers = new List<AwardedSupplier>();
            driver.Navigate().GoToUrl(url);

            try
            {
                IWebElement element2 = driver.FindElement(By.ClassName("card-body"));
                var test1 = element2.Text;
                return;
            }
            catch (Exception) { }

            IWebElement OfferApplicantTable = driver.FindElement(By.XPath("//table[@summary='desc']"));
            IList<IWebElement> rows = OfferApplicantTable.FindElements(By.TagName("tr"));
            foreach (IWebElement row in rows)
            {
                IList<IWebElement> cells = row.FindElements(By.TagName("td"));
                if (cells.Count > 0)
                {
                    offerApplicant = new OfferApplicant();
                    offerApplicant.Supplier_name = cells[0].Text;
                    offerApplicant.Financial_offer = double.Parse(cells[1].Text);
                    offerApplicant.IsAccepted = cells[2].Text == "مطابق" ? true : false;
                    offerApplicants.Add(offerApplicant!);

                }
            }

            var awardedSupplierTable = driver.FindElements(By.XPath("//table[@summary='desc']")).Skip(1).Single();
            IList<IWebElement> awardedSupplierTableRows = awardedSupplierTable.FindElements(By.TagName("tr"));
            foreach (IWebElement row in awardedSupplierTableRows)
            {
                IList<IWebElement> cells = row.FindElements(By.TagName("td"));
                if (cells.Count > 0)
                {
                    awardedSupplier = new AwardedSupplier();
                    awardedSupplier.Supplier_name = cells[0].Text;
                    awardedSupplier.Financial_offer = double.Parse(cells[1].Text);
                    awardedSupplier.Award_value = double.Parse(cells[2].Text);
                    awardedSuppliers.Add(awardedSupplier!);
                }

            }
            awardingResult.awardedSuppliers = awardedSuppliers;
            awardingResult.offerApplicants = offerApplicants;
        }
        GetAwardingResultsList.Add(awardingResult);
    }
    private void GetRelationsDetails(string tenderID)
    {
        GetRelationsDetail getRelationsDetail = new GetRelationsDetail();
        getRelationsDetail.tenderIdString = tenderID;
        var url = Constants.UrlGetRelationsDetail + tenderID;
        var chromeOptions = new ChromeOptions() { AcceptInsecureCertificates = true };
        chromeOptions.AddArguments("--headless=new"); // comment out for testing
        using var driver = new ChromeDriver(chromeOptions);
        driver.Navigate().GoToUrl(url);

        IList<IWebElement> listItems = driver.FindElements(By.CssSelector("ul.list-group.form-details-list li.list-group-item"));
        foreach (IWebElement listItem in listItems)
        {
            var titleElement = listItem.FindElement(By.CssSelector(".etd-item-title")).Text;
            var infoElement = listItem.FindElement(By.CssSelector(".etd-item-info"));
            GetInDepthInfo(infoElement, getRelationsDetail);
            switch (titleElement)
            {
                case "مكان التنفيذ":
                    getRelationsDetail.ExecutionLocation = infoElement.Text;
                    break;
                case "التفاصيل":
                    getRelationsDetail.Details = infoElement.Text;
                    break;
                case "نشاط المنافسة":
                    getRelationsDetail.CompetitionActivity = infoElement.Text;
                    break;
                case "تشمل المنافسة على بنود توريد":
                    getRelationsDetail.SupplyItemsCompetition = infoElement.Text;
                    break;
                case "أعمال الإنشاء":
                    getRelationsDetail.ConstructionWorks = infoElement.Text;
                    break;
                case "أعمال الصيانة والتشغيل":
                    getRelationsDetail.MaintenanceAndOperationWorks = infoElement.Text;
                    break;
            }
        }
        // Close the browser
        driver.Quit();
        GetRelationsDetailsList.Add(getRelationsDetail);
    }
    private void GetInDepthInfo(IWebElement webElement, GetRelationsDetail getRelationsDetail)
    {
        try
        {
            var isListFounded = AreChildElementsPresent(webElement, By.CssSelector(".etd-item-info"));
            var isSpanFounded = AreChildElementsPresent(webElement, By.TagName("span"));
            if (!isListFounded || !isListFounded)
            {
                return;
            }
            if (isSpanFounded)
            {
                var span = webElement.FindElement(By.TagName("span")).Text;
            }
            IWebElement[]? infoItems = null;
            if (isListFounded)
            {
                infoItems = webElement?.FindElements(By.TagName("li")).ToArray();

                if (!infoItems.Any() || infoItems is null)
                {
                    return;
                }
                foreach (var item in infoItems)
                {
                    var itemTxt = item.Text;
                    GetInDepthInfo(item, getRelationsDetail);
                }
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return;
        }
    }
    private bool AreChildElementsPresent(IWebElement parentElement, By by)
    {
        try
        {
            var childElements = parentElement.FindElements(by);

            return childElements.Count > 0;
        }
        catch (NoSuchElementException)
        {
            return false;
        }
    }
    private void GetDetailsForVisitor(string tenderID)
    {
        GetDetailsForVisitor detailsForVisitor = new GetDetailsForVisitor();
        detailsForVisitor.tenderIdString = tenderID;

        var url = Constants.UrlDetailsForVisitor + tenderID;
        // Set up ChromeDriver
        ChromeOptions options = new ChromeOptions();
        options.AddArgument("--headless"); // Run Chrome in headless mode (without opening a browser window)
        IWebDriver driver = new ChromeDriver(options);

        // Navigate to the page
        driver.Navigate().GoToUrl(url);

        // Find the parent element that contains all the list items
        IWebElement parentElement = driver.FindElement(By.Id("basicDetials"));

        // Find all the list items within the parent element
        IReadOnlyCollection<IWebElement> listItems = parentElement.FindElements(By.CssSelector("li.list-group-item"));

        string result = "";

        // Loop through the list items and print their text
        foreach (IWebElement listItem in listItems)
        {
            var title = listItem.FindElement(By.ClassName("etd-item-title"));
            var info = listItem.FindElement(By.ClassName("etd-item-info"));
            var txt = title.Text;
            var spans = info.FindElements(By.TagName("span")).Select(o => o.Text).ToArray();
            try
            {
                switch (txt)
                {
                    case "الغرض من المنافسة":
                        IWebElement purposeSpan = driver.FindElement(By.Id("purposeSpan"));
                        string purposeText = purposeSpan.GetAttribute("innerHTML");
                        detailsForVisitor.CompetitionPurpose = purposeText;
                        break;
                    case "حالة المنافسة":
                        detailsForVisitor.CompetitionStatus = spans[0];
                        break;
                    case "مدة العقد":
                        if (spans[0].Contains("يوم"))
                        {
                            string trimmedString = spans[0].TrimEnd("يوم".ToCharArray());
                            detailsForVisitor.ContractDuration = double.Parse(trimmedString);
                        }
                        if (spans[0].Contains("شهر"))
                        {
                            string trimmedString = spans[0].TrimEnd("شهر".ToCharArray());
                            detailsForVisitor.ContractDuration = double.Parse(trimmedString) * 30;
                        }
                        if (spans[0].Contains("سنة"))
                        {
                            string trimmedString = spans[0].TrimEnd("سنة".ToCharArray());
                            detailsForVisitor.ContractDuration = double.Parse(trimmedString) * 365;
                        }
                        break;
                    case "هل التأمين من متطلبات المنافسة":
                        detailsForVisitor.IsInsuranceRequired = spans[0] == "لا" ? false : true;
                        break;
                    case "طريقة تقديم العروض":
                        detailsForVisitor.OfferingMethod = spans[0];
                        break;
                    case "مطلوب ضمان الإبتدائي":
                        detailsForVisitor.IsInsuranceRequired = (spans[0] == "لا يوجد ضمان") ? false : true;
                        break;
                    case "عنوان الضمان الإبتدائى":
                        detailsForVisitor.PreliminaryGuaranteeAddress = spans[0];
                        break;
                    case "الضمان النهائي":
                        detailsForVisitor.FinalGuarantee = double.Parse(spans[0]);
                        break;
                    case "رقم الترسية":
                        detailsForVisitor.AwardNumber = spans[0];
                        break;
                    case "":
                        break;
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }
        driver.Quit();
        DetailsForVisitorsList.Add(detailsForVisitor);
    }

}
