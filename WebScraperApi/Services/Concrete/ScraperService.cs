using OpenQA.Selenium.Support.UI;
using static OpenQA.Selenium.VirtualAuth.VirtualAuthenticatorOptions;
using System.Collections.Generic;
using System.Net.Sockets;
using System;
using System.Net.NetworkInformation;

namespace WebScraperApi.Services.Concrete;

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
    int resetDriver = 0;
    public async Task GetDataAsync()
    {

        //max page 2562
        for (int i = 77; i < 101; i++)
        {
            await Console.Out.WriteLineAsync("*********page Number  " + i + "  **********************");
            var CardsBasicDataFromDB = await _service.GetCardsIDsPagesFromDBAsync(100, i);
            await GetAllRelatedData(CardsBasicDataFromDB.ToList());
            resetDriver++;
            if(resetDriver==3)
            {
                WebDriverSingleton.Dispose();
                resetDriver = 0;
            }

        }
        //var nulldates =_context.GetTenderDates.Where(o=>o.expectedAwardDate==null).ToList();
        //var nulldatesIds = nulldates.Select(d => d.tenderIdString).ToList();
        //foreach (var id in nulldatesIds)
        //{
        //    await GetTenderDates(id);
        //    Console.WriteLine("_______________________******_____" + nulldatesIds.IndexOf(id)+ "of"+ nulldatesIds.Count);

        //}
        //_context.GetTenderDates.RemoveRange(nulldates);
        //_context.GetTenderDates.AddRange(GetTenderDatesList);
        //await _context.SaveChangesAsync();


        //loop over pagination 
        //select ids
        //pass list of ids to getall relateddata
        return;
    }
    private async Task GetAllRelatedData(List<string> CardsIDs)
    {
        foreach (var tenderID in CardsIDs)
        {
            await GetTenderDates(tenderID); //page 48-101
            await GetAwardingResult(tenderID); //page 48-101
         //  await GetRelationsDetails(tenderID); //28//100
            await GetDetailsForVisitor(tenderID);//4-27 48-101
            Console.WriteLine("_______________________******_____" + CardsIDs.IndexOf(tenderID));
        }
         _context.GetTenderDates.AddRange(GetTenderDatesList);
        _context.GetAwardingResults.AddRange(GetAwardingResultsList);
     // _context.GetRelationsDetails.AddRange(GetRelationsDetailsList);
        _context.GetDetailsForVisitor.AddRange(DetailsForVisitorsList);
     await   _context.SaveChangesAsync();
        GetTenderDatesList = new();
        GetAwardingResultsList = new();
       // GetRelationsDetailsList = new();
        DetailsForVisitorsList = new();
    }
    private async Task GetTenderDates(string tenderID)
    {
        await Console.Out.WriteLineAsync("GetTenderDates");
        try
        {
            await Task.Run(() =>
                    {
                        GetTenderDate tenderDates = new GetTenderDate();
                        var url = Constants.UrlGetTenderDates + tenderID;
                        IWebDriver driver2 = WebDriverSingleton.Instance.Driver;
                        driver2.Navigate().GoToUrl(url);
                        tenderDates.tenderIdString = tenderID;
                        var dates = driver2.FindElements(By.CssSelector("li.list-group-item"));
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

                                        if (int.TryParse(span[0], out int parsedValue))
                                        {
                                            tenderDates.AnswerInquiriesInDays = parsedValue;
                                        }
                                        else
                                        {
                                            tenderDates.AnswerInquiriesInDays = null;
                                        }
                                        break;
                                    case "مكان فتح العرض":
                                        tenderDates.offersOpeningLocation = span[0];
                                        break;
                                        default : break;
                                }

                            }
                            catch (Exception ex)
                            {

                                Console.WriteLine("GetTenderDates Error_________" + ex.Message);
                            }
                        }
                        GetTenderDatesList.Add(tenderDates);
                    }
                        );
        }
        catch (Exception ex)
        {
            LogToFile($"\"{tenderID}\"\n", "GetTenderDates.txt");
            if (IsInternetAvailable())
            {
                WebDriverSingleton.Dispose();
            }
            else
            {
                Console.WriteLine("No internet connection.");
            }
        }
    }
    private async Task GetAwardingResult(string tenderID)
    {
        await Console.Out.WriteLineAsync("GetAwardingResult");
        try
        {
            await Task.Run(() =>
            {
                GetAwardingResult awardingResult = new GetAwardingResult();
                OfferApplicant offerApplicant = null;
                List<OfferApplicant> offerApplicants = new List<OfferApplicant>();
                AwardedSupplier awardedSupplier = null;
                List<AwardedSupplier> awardedSuppliers = new List<AwardedSupplier>();
                awardingResult.tenderIdString = tenderID;
                var url = Constants.UrlGetAwardingResult + tenderID;
                IWebDriver driver2 = WebDriverSingleton.Instance.Driver;
                driver2.Navigate().GoToUrl(url);
                try
                {
                    IWebElement element2 = driver2.FindElement(By.ClassName("card-body"));
                    var test1 = element2.Text;
                    return;
                }
                catch (Exception ex) {
                    Console.WriteLine("GetAwardingResult" +ex.Message +"data not found ");
                }

                IWebElement OfferApplicantTable = driver2.FindElement(By.XPath("//table[@summary='desc']"));
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
                var awardedSupplierTable = driver2.FindElements(By.XPath("//table[@summary='desc']")).Skip(1).Single();
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
                GetAwardingResultsList.Add(awardingResult);
            });
        }
        catch (Exception ex)
        {
            LogToFile($"\"{tenderID}\"\n", "GetAwardingResult.txt");
            if (IsInternetAvailable())
            {
                WebDriverSingleton.Dispose();
            }
            else
            {
                Console.WriteLine("No internet connection.");
            }
        }

    }
    private async Task GetRelationsDetails(string tenderID)
    {
        try
        {
            await Task.Run(() =>
        {
            GetRelationsDetail getRelationsDetail = new GetRelationsDetail();
            ExecutionLocation executionLocation = new ExecutionLocation();
            List<ExecutionLocation> executionLocations = new List<ExecutionLocation>();
            getRelationsDetail.tenderIdString = tenderID;
            //  var url = Constants.UrlGetRelationsDetail + "bD%206v98V*@@**lZ9hHHRfEYDXg==";//tenderID;
            // var url = Constants.UrlGetRelationsDetail + "*@@**0*@@**mn0y%20iTWVZYHq1Dm7KQ==";//tenderID;
            // var url = Constants.UrlGetRelationsDetail + "1SvP6s1e2e%20NA7QGJFs6bw==";//tenderID;

            //driver1.ResetInputState();
            var url = Constants.UrlGetRelationsDetail + tenderID;
            //driver1.Navigate().GoToUrl(url);
            IWebDriver driver2 = WebDriverSingleton.Instance.Driver;
            driver2.Navigate().GoToUrl(url);
            IList<IWebElement> listItems = driver2.FindElements(By.CssSelector("ul.list-group.form-details-list li.list-group-item"));
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
            //  driver2.Quit();
            getRelationsDetail.ExecutionLocations = executionLocations;
            GetRelationsDetailsList.Add(getRelationsDetail);
            //_context.GetRelationsDetails.Add(getRelationsDetail);
            //_context.SaveChanges();
        });
        }
        catch (Exception ex)
        {
            LogToFile($"\"{tenderID}\"\n", "GetRelationsDetails.txt");
            if (IsInternetAvailable())
            {
                WebDriverSingleton.Dispose();
            }
            else
            {
                Console.WriteLine("No internet connection.");
            }
        }
    }
    private async Task GetDetailsForVisitor2(string tenderID)
    {
        try
        {
            await Task.Run(() =>
            {
                GetDetailsForVisitor detailsForVisitor = new GetDetailsForVisitor();
                detailsForVisitor.tenderIdString = tenderID;

                var url = Constants.UrlDetailsForVisitor + tenderID;
                IWebDriver driver = WebDriverSingleton.Instance.Driver;

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
                            if (double.TryParse(spans[0], out double parsedValue))
                            {
                                detailsForVisitor.FinalGuarantee = parsedValue;
                            }
                            else
                            {
                                detailsForVisitor.FinalGuarantee = null;
                            }
                            break;
                        case "رقم الترسية":
                            detailsForVisitor.AwardNumber = spans[0];
                            break;
                        case "":
                            break;
                    }
                }
                DetailsForVisitorsList.Add(detailsForVisitor);
            });
        }
        catch (Exception ex)
        {
            LogToFile($"\"{tenderID}\"\n", "GetDetailsForVisitor.txt");


        }
    }
    private async Task GetDetailsForVisitor(string tenderID)
    {
        await Console.Out.WriteLineAsync("GetDetailsForVisitor");
        try
        {
            GetDetailsForVisitor detailsForVisitor = new GetDetailsForVisitor();
            detailsForVisitor.tenderIdString = tenderID;
            var url = Constants.UrlDetailsForVisitor + tenderID;
            IWebDriver driver = WebDriverSingleton.Instance.Driver;
            // Navigate to the page
            driver.Navigate().GoToUrl(url);

            // Extract information before processing list items
            IWebElement parentElement = driver.FindElement(By.Id("basicDetials"));
            IReadOnlyCollection<IWebElement> listItems = parentElement.FindElements(By.CssSelector("li.list-group-item"));

            // Process list items concurrently
            await Task.WhenAll(listItems.Select(async listItem =>
            {
                var title = listItem.FindElement(By.ClassName("etd-item-title"));
                var info = listItem.FindElement(By.ClassName("etd-item-info"));
                var txt = title.Text;
                var spans = info.FindElements(By.TagName("span")).Select(o => o.Text).ToArray();

                // Use a dictionary to map titles to actions
                var titleActions = new Dictionary<string, Action>
            {
               { "الغرض من المنافسة", () => ProcessCompetitionPurpose(detailsForVisitor, driver) },
    { "حالة المنافسة", () => detailsForVisitor.CompetitionStatus = spans[0] },
    { "مدة العقد", () => ProcessContractDuration(detailsForVisitor, spans[0]) },
    { "هل التأمين من متطلبات المنافسة", () => detailsForVisitor.IsInsuranceRequired = spans[0] == "لا" ? false : true },
    { "طريقة تقديم العروض", () => detailsForVisitor.OfferingMethod = spans[0] },
    { "مطلوب ضمان الإبتدائي", () => detailsForVisitor.IsPreliminaryGuaranteeRequired = (spans[0] == "لا يوجد ضمان") ? false : true },
    { "عنوان الضمان الإبتدائى", () => detailsForVisitor.PreliminaryGuaranteeAddress = spans[0] },
    { "الضمان النهائي", () =>
{
    if (double.TryParse(spans[0], out double parsedValue))
    {
        detailsForVisitor.FinalGuarantee = parsedValue;
    }
    else
    {
        detailsForVisitor.FinalGuarantee = 0.0;
    }
} },
    { "رقم الترسية", () => detailsForVisitor.AwardNumber = spans[0] },
    { "", () => { } } // Empty case

            };

                if (titleActions.ContainsKey(txt))
                {
                    titleActions[txt]();
                }
            }));

            DetailsForVisitorsList.Add(detailsForVisitor);
        }
        catch (Exception ex)
        {
            LogToFile($"\"{tenderID}\"\n", "GetDetailsForVisitor.txt");
            if (IsInternetAvailable())
            {
                WebDriverSingleton.Dispose();
            }
            else
            {
                Console.WriteLine("No internet connection.");
            }
        }
    }
    private void ProcessCompetitionPurpose(GetDetailsForVisitor detailsForVisitor, IWebDriver driver)
    {
        var toBeReplaced = "<i class=\"readLess\"> ...عرض الأقل...</i>";
        IWebElement purposeSpan = driver.FindElement(By.Id("purposeSpan"));
        var SpanText = purposeSpan.GetAttribute("innerHTML");
        detailsForVisitor.CompetitionPurpose = SpanText.Contains(toBeReplaced)
            ? SpanText.Replace(toBeReplaced, "")
            : SpanText;
    }

    private void ProcessContractDuration(GetDetailsForVisitor detailsForVisitor, string durationText)
    {
        if (durationText.Contains("يوم"))
        {
            string trimmedString = durationText.TrimEnd("يوم".ToCharArray());
            detailsForVisitor.ContractDuration = double.Parse(trimmedString);
        }
        else if (durationText.Contains("شهر"))
        {
            string trimmedString = durationText.TrimEnd("شهر".ToCharArray());
            detailsForVisitor.ContractDuration = double.Parse(trimmedString) * 30;
        }
        else if (durationText.Contains("سنة"))
        {
            string trimmedString = durationText.TrimEnd("سنة".ToCharArray());
            detailsForVisitor.ContractDuration = double.Parse(trimmedString) * 365;
        }
    }
    static bool IsInternetAvailable()
    {
        try
        {
            using (Ping ping = new Ping())
            {
                PingReply reply = ping.Send("www.google.com", 3000); // Adjust the timeout as needed

                return (reply != null && reply.Status == IPStatus.Success);
            }
        }
        catch (PingException)
        {
            return false;
        }
    }
    private void LogToFile(string data, string logFileName)
    {
        try
        {

            string logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, logFileName);

            // Use StreamWriter to append data to the file
            using (StreamWriter sw = File.AppendText(logFilePath))
            {
                // Write the data to the file
                sw.WriteLine($"{DateTime.Now}: {data}");
            }
        }
        catch (Exception ex)
        {
            // Handle exceptions if needed
            Console.WriteLine($"Error logging to file: {ex.Message}");
        }
    }

}


