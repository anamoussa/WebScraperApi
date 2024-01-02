﻿namespace WebScraperApi.Services.Concrete;

public class ScraperService : IScraperService
{
    private List<GetDetailsForVisitor> DetailsForVisitorsList = new();
    private List<GetTenderDate> GetTenderDatesList = new();
    private List<GetAwardingResult> GetAwardingResultsList = new();
    private List<GetRelationsDetail> GetRelationsDetailsList = new();

    private readonly ScrapDBContext _context;
    private readonly IDataService _service;
    public ScraperService(ScrapDBContext context, IDataService service)
    {

        _context = context;
        _service = service;
    }
    public async Task GetDataAsync()
    {
        for (int i = 1; i < 2562; i++)
        {
            await Console.Out.WriteLineAsync("*********page Number  " + i + "  **********************");
            var CardsBasicDataFromDB = await _service.GetCardsIDsPagesFromDBAsync(100, i);
            GetAllRelatedData(CardsBasicDataFromDB.ToList());

        }

        //loop over pagination 
        //select ids
        //pass list of ids to getall relateddata
        return;
    }
    private void GetAllRelatedData(List<string> CardsIDs)
    {
        int counter = 0;
        foreach (var tenderID in CardsIDs)
        {

            GetDetailsForVisitor(tenderID);
            GetTenderDates(tenderID);
            GetAwardingResult(tenderID);
            GetRelationsDetails(tenderID);

            if (counter == 10)
            {
                //var recordsToDelete = _context.CardBasicDatas
                //.Where(c => tenderIDs.Contains(c.tenderIdString))
                //.ToList();
                //_context.CardBasicDatas.RemoveRange(recordsToDelete);
                //_context.SaveChanges();
                // _context.CardBasicDatas.AddRange(CardsBasicData);

                _context.GetTenderDates.AddRange(GetTenderDatesList);
                _context.GetRelationsDetails.AddRange(GetRelationsDetailsList);
                _context.GetAwardingResults.AddRange(GetAwardingResultsList);
                _context.GetDetailsForVisitor.AddRange(DetailsForVisitorsList);
                _context.SaveChanges();
                DetailsForVisitorsList = new();
                GetTenderDatesList = new();
                GetAwardingResultsList = new();
                GetRelationsDetailsList = new();
                counter = 0;
            }
            counter++;
            Console.WriteLine("_______________________******_____" + CardsIDs.IndexOf(tenderID));
        }
    }
    private void GetTenderDates(string tenderID)
    {
        var url = Constants.UrlGetTenderDates + tenderID;
        var chromeOptions = new ChromeOptions() { AcceptInsecureCertificates = true };
        chromeOptions.AddArguments("--headless=new"); // comment out for testing
        using var driver = new ChromeDriver(chromeOptions);
        driver.Manage().Timeouts().PageLoad.Add(TimeSpan.FromMinutes(5));
        driver.Manage().Timeouts().ImplicitWait.Add(TimeSpan.FromMinutes(5));
        driver.Manage().Timeouts().AsynchronousJavaScript.Add(TimeSpan.FromMinutes(5));
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
        ExecutionLocation executionLocation = new ExecutionLocation();
        List<ExecutionLocation> executionLocations = new List<ExecutionLocation>();
        getRelationsDetail.tenderIdString = tenderID;
        //  var url = Constants.UrlGetRelationsDetail + "bD%206v98V*@@**lZ9hHHRfEYDXg==";//tenderID;
        // var url = Constants.UrlGetRelationsDetail + "*@@**0*@@**mn0y%20iTWVZYHq1Dm7KQ==";//tenderID;
        // var url = Constants.UrlGetRelationsDetail + "1SvP6s1e2e%20NA7QGJFs6bw==";//tenderID;
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
            //   GetInDepthInfo(infoElement, getRelationsDetail);
            switch (titleElement)
            {
                case "مكان التنفيذ":
                    var spa = infoElement.FindElement(By.TagName("span")).Text;
                    var CitiesList = infoElement.FindElements(By.XPath("(//ol)[1]/li"));
                    foreach (var item in CitiesList)
                    {
                        executionLocation = new ExecutionLocation();
                        executionLocation.InSideKingdom = spa == "داخل المملكة" ? true : false;
                        if (item.Text.Contains("\n"))
                        {
                            executionLocation.City = item.Text.Split("\n")[0].Trim();
                        }
                        else
                        {
                            executionLocation.City = item.Text.Trim();
                        }
                        var Regions = item.FindElements(By.TagName("li"));
                        List<Region> regions = new List<Region>();
                        foreach (var Regionitem in Regions)
                        {
                            Region region = new Region();
                            region.Name = Regionitem.Text;
                            regions.Add(region);
                        }
                        executionLocation.Regions = regions;
                        executionLocations.Add(executionLocation);
                    }
                    break;
                case "التفاصيل":
                    getRelationsDetail.Details = infoElement.Text;
                    break;
                case "نشاط المنافسة":
                    var CompetitionActivitiesList = infoElement.FindElements(By.XPath("(//ol)[2]/li"));
                    foreach (var item in CompetitionActivitiesList)
                    {
                        CompetitionActivity competitionActivity = new CompetitionActivity();
                        competitionActivity.Name = item.Text;
                        getRelationsDetail.CompetitionActivities?.Add(competitionActivity);
                    }
                    break;
                case "تشمل المنافسة على بنود توريد":
                    getRelationsDetail.SupplyItemsCompetition = infoElement.Text;
                    break;
                case "أعمال الإنشاء":
                    var ConstructionWorksList = infoElement.FindElements(By.XPath("(//ol)[3]/li"));
                    foreach (var item in ConstructionWorksList)
                    {
                        ConstructionWork constructionWork = new ConstructionWork();
                        constructionWork.Name = item.Text;
                        getRelationsDetail.ConstructionWorks?.Add(constructionWork);
                    }
                    break;
                case "أعمال الصيانة والتشغيل":
                    var MaintenanceAndOperationWorkList = infoElement.FindElements(By.XPath("(//ol)[4]/li"));
                    foreach (var item in MaintenanceAndOperationWorkList)
                    {
                        MaintenanceAndOperationWork maintenanceAndOperationWork = new MaintenanceAndOperationWork();
                        maintenanceAndOperationWork.Name = item.Text;
                        getRelationsDetail.MaintenanceAndOperationWorks?.Add(maintenanceAndOperationWork);
                    }
                    break;
            }
        }
        // Close the browser
        driver.Quit();
        getRelationsDetail.ExecutionLocations = executionLocations;
        GetRelationsDetailsList.Add(getRelationsDetail);
        //_context.GetRelationsDetails.Add(getRelationsDetail);
        //_context.SaveChanges();
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
                        var toBeReplaced = "<i class=\"readLess\"> ...عرض الأقل...</i>";
                        IWebElement purposeSpan = driver.FindElement(By.Id("purposeSpan"));
                        var SpanText = purposeSpan.GetAttribute("innerHTML");
                        if (SpanText.Contains(toBeReplaced))
                        {
                            detailsForVisitor.CompetitionPurpose = SpanText.Replace(toBeReplaced, "");
                        }
                        else
                        {
                            detailsForVisitor.CompetitionPurpose = SpanText;
                        }
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
                        detailsForVisitor.IsPreliminaryGuaranteeRequired = (spans[0] == "لا يوجد ضمان") ? false : true;
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
