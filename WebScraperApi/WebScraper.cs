using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using Carter;
using OpenQA.Selenium.Support.UI;
using WebScraperApi.Services;
using WebScraperApi.Models;
using System.Globalization;
using System.Runtime.InteropServices;

namespace WebScraperApi;

public class WebScraper : ICarterModule
{
    private List<DetailsForVisitor> DetailsForVisitorsList = new List<DetailsForVisitor>();
    private List<GetTenderDates> GetTenderDatesList = new List<GetTenderDates>();
    private List<GetAwardingResult> GetAwardingResultsList = new List<GetAwardingResult>();
    private List<GetRelationsDetail> GetRelationsDetailsList = new List<GetRelationsDetail>();

    private string UrlDetailsForVisitor = $"https://tenders.etimad.sa/Tender/DetailsForVisitor?tenderIdStr=";
    private string UrlGetTenderDates = "https://tenders.etimad.sa/Tender/GetTenderDatesViewComponenet?tenderIdStr=";
    private string UrlGetAwardingResult = "https://tenders.etimad.sa/Tender/GetAwardingResultsForVisitorViewComponenet?tenderIdStr=";
    private string UrlGetRelationsDetail = "https://tenders.etimad.sa/Tender/GetRelationsDetailsViewComponenet?tenderIdStr=";



    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/webscraper");
        group.MapGet("/selenium", () =>
        {
            var data = GetDataWithSelenium("https://tenders.etimad.sa/Tender/AllTendersForVisitor");
            return data;
        }).WithOpenApi();
        group.MapGet("/getAllData", async () =>
        {
            var CardsBasicData = await GetDataService.GetTaskAsync();
            var CardsBasicDataIds = CardsBasicData.Select(t => t.tenderIdString).ToList();
            GetCardsDetails(CardsBasicDataIds);

        }).WithOpenApi();
    }

    private void GetCardsDetails(List<string> tenderIDs)
    {
        GetRelationsDetails("");
        foreach (var tenderID in tenderIDs)
        {
            //var resuylt = GetAwardingResult(tenderID);

            //DetailsForVisitor(tenderID);
            //GetTenderDates(tenderID);
            // GetAwardingResult(tenderID);
            //GetRelationsDetails(tenderID);
        }
        //add DetailsForVisitorsList to DB table and save Changes 
    }

    private void GetTenderDates(string tenderID)
    {
        var url = UrlGetTenderDates + tenderID;
        var chromeOptions = new ChromeOptions() { AcceptInsecureCertificates = true };
        chromeOptions.AddArguments("--headless=new"); // comment out for testing

        using (var driver = new ChromeDriver(chromeOptions))
        {
            driver.Navigate().GoToUrl(url);
            GetTenderDates tenderDates = null;

            var items = driver.FindElements(By.XPath("//li[@class='list-group-item']"));

            foreach (var item in items)
            {
                tenderDates = new();
                var dates = item.FindElements(By.XPath("//div[@class='row']")).Skip(1).Take(10);
                foreach (var date in dates)
                {
                    var title = date.FindElement(By.ClassName("etd-item-title"));
                    var info = date.FindElement(By.ClassName("etd-item-info"));
                    var txt = title.Text;
                    var span = info.FindElements(By.TagName("span")).Select(o => o.Text).ToArray();
                    switch (txt)
                    {
                        case "اخر موعد لإستلام الاستفسارات":
                            tenderDates.lastEnqueriesDate = DateOnly.ParseExact(span[0], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            tenderDates.lastEnqueriesDateHijri = span[1];
                            break;
                        case "آخر موعد لتقديم العروض":
                            tenderDates.lastOfferPresentationDate = DateOnly.ParseExact(span[0], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            tenderDates.lastOfferPresentationDateHijri = span[1];
                            tenderDates.lastOfferPresentationTime = TimeOnly.ParseExact(span[2], "h:mm tt", CultureInfo.InvariantCulture);
                            break;
                        case "تاريخ فتح العروض":
                            tenderDates.offersOpeningDate = span[0] == "لا يوجد" ? null : DateTime.Parse(span[0]);
                            break;
                        case "تاريخ فحص العروض":
                            tenderDates.offersExaminationDate = DateOnly.ParseExact(span[0], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            tenderDates.offersExaminationDateHijri = span[1];
                            tenderDates.offersExaminationTime = TimeOnly.ParseExact(span[2], "h:mm tt", CultureInfo.InvariantCulture);
                            break;
                        case "التاريخ المتوقع للترسية":
                            tenderDates.expectedAwardDate = DateTime.ParseExact(span[0], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            tenderDates.expectedAwardDateHijri = span[1];
                            break;
                        case "تاريخ بدء الأعمال / الخدمات":
                            tenderDates.businessStartDate = DateTime.ParseExact(span[0], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            tenderDates.businessStartDateHijri = span[1];
                            break;
                        case "تاريخ استحقاق خطاب تأكيد المشاركة":
                            tenderDates.participationConfirmationLetterDate = span[0] == "لا يوجد" ? null : DateTime.ParseExact(span[0], "dd/MM/yyyy", CultureInfo.InvariantCulture); ;
                            break;
                        case "بداية إرسال الأسئلة و الاستفسارات":
                            tenderDates.sendingInquiriesDate = DateTime.ParseExact(span[0], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            tenderDates.sendingInquiriesDateHijri = span[1];
                            break;
                        case "أقصى مدة للإجابة على الإستفسارات(أيام)":
                            tenderDates.AnswerInquiriesInDays = int.Parse(span[0]);
                            break;
                        default:
                            tenderDates.offersOpeningLocation = span[0];
                            break;
                    }
                }
                GetTenderDatesList.Add(tenderDates);
            }
            driver.Quit();
        }
    }
    private GetAwardingResult GetAwardingResult(string tenderID)
    {
        GetAwardingResult awardingResult = new GetAwardingResult();
        //  var url = UrlGetAwardingResult + "l1FcIiwZqe2uSgRYjuiCRA==";// tenderID;
        //  var url = UrlGetAwardingResult + "QLrdkFBEQtZctHDeIppNmw==";// tenderID;
        var url = UrlGetAwardingResult + tenderID;
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
                return null;
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
                    offerApplicant.supplier_name = cells[0].Text;
                    offerApplicant.financial_offer = double.Parse(cells[1].Text);
                    offerApplicant.isAccepted = cells[2].Text == "مطابق" ? true : false;
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
                    awardedSupplier.supplier_name = cells[0].Text;
                    awardedSupplier.financial_offer = double.Parse(cells[1].Text);
                    awardedSupplier.award_value = double.Parse(cells[2].Text);
                    awardedSuppliers.Add(awardedSupplier!);
                }

            }
            awardingResult.awardedSuppliers = awardedSuppliers;
            awardingResult.offerApplicants = offerApplicants;
        }
        return awardingResult;

    }
    private void GetRelationsDetails(string tenderID)
    {
        var url = UrlGetRelationsDetail + "81fd6cZ5YX5xoZT8PuFuFg==";
        IWebDriver driver = new ChromeDriver();

        // Navigate to the page with your HTML content
        driver.Navigate().GoToUrl(url);

        try
        {

            IList<IWebElement> listItems = driver.FindElements(By.CssSelector("ul.list-group.form-details-list li.list-group-item"));

            foreach (IWebElement listItem in listItems)
            {
                // Access the elements within each list item
                IWebElement titleElement = listItem.FindElement(By.CssSelector(".etd-item-title"));
                IWebElement infoElement = listItem.FindElement(By.CssSelector(".etd-item-info"));

                // Output the text of the elements
                var test = titleElement.Text;
                var test2 = infoElement.Text;
                Console.WriteLine("Title: " + titleElement.Text);
                Console.WriteLine("Info: " + infoElement.Text);
            }
            if(listItems.Count> 0)
            {
                
            }
        }
        catch (NoSuchElementException e)
        {
            Console.WriteLine("Element not found: " + e.Message);
        }
        finally
        {
            // Close the browser
            driver.Quit();
        }
    }
    private void DetailsForVisitor(string tenderID)
    {
        var url = UrlDetailsForVisitor + tenderID;


    }
    #region new
    private string GetDataWithSelenium(string url)
    {
        var chromeOptions = new ChromeOptions() { AcceptInsecureCertificates = true };
        chromeOptions.AddArguments("--headless=new"); // comment out for testing

        using (var driver = new ChromeDriver(chromeOptions))
        {
            driver.Navigate().GoToUrl(url);

            var cards = driver.FindElements(By.XPath("//div[@id='cardsresult']//div[@class='row justify-content-center']//div[@class='col-12 col-md-12 mb-4']"));

            for (int i = 0; i < cards.Count; i++)
            {
                // Find elements on each iteration to avoid staleness
                var card = driver.FindElements(By.XPath("//div[@id='cardsresult']//div[@class='row justify-content-center']//div[@class='col-12 col-md-12 mb-4']"))[i];
                var detailsBtn = card.FindElement(By.ClassName("pull-right"));
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(50));
                wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", detailsBtn);

                detailsBtn.Click();
                GetDetails(driver, card);
                driver.Navigate().Back();
                // Find elements again after the back navigation
                cards = driver.FindElements(By.XPath("//div[@id='cardsresult']//div[@class='row justify-content-center']//div[@class='col-12 col-md-12 mb-4']"));
            }

            driver.Quit();
        }

        return "";
    }

    // Modify GetDetails to accept a card element and extract information
    //private void GetDetails(IWebDriver driver, IWebElement card)
    //{
    //    // Implement your logic to extract details from the card element
    //    // For example:
    //    var title = card.FindElement(By.XPath(".//div[@class='tender-card-title']")).Text;
    //    var description = card.FindElement(By.XPath(".//div[@class='tender-card-description']")).Text;

    //    // Print or store the extracted details as needed
    //    Console.WriteLine($"Title: {title}");
    //    Console.WriteLine($"Description: {description}");
    //    Console.WriteLine("--------------");
    //}
    private void GetDetails(IWebDriver driver, IWebElement card)
    {
        List<string> carddetails = new List<string>();
        var details = driver.FindElements(By.ClassName("form-details-list"));
        foreach (var detail in details)
        {
            var items = detail.FindElements(By.ClassName("list-group-item"));
            foreach (var item in items)
            {
                var title = item.FindElement(By.ClassName("etd-item-title")).Text;
                var infoSpans = item.FindElements(By.ClassName("etd-item-info"));
                string info = string.Empty;
                if (infoSpans.Any())
                {
                    foreach (var infoSpan in infoSpans)
                    {
                        info += infoSpan.Text;
                    }
                    //todo add details to database and change the return of the function to void
                }
                carddetails.Add(info);
                // DetailsDict.Add(title, info);
            }
            //DetailsList.Add(carddetails);
        }
    }
    #endregion



}
