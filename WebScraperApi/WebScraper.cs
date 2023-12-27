﻿using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using Carter;
using OpenQA.Selenium.Support.UI;
using WebScraperApi.Services;
using WebScraperApi.Models;
using System.Globalization;

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
        GetAwardingResult("");

        foreach (var tenderID in tenderIDs)
        {
            //DetailsForVisitor(tenderID);
            //GetTenderDates(tenderID);
           // GetAwardingResult(tenderID);
            //GetRelationsDetails(tenderID);
        }
        //add DetailsForVisitorsList to DB table and save Changes 
    }
    private void DetailsForVisitor(string tenderID)
    {
        var url = UrlDetailsForVisitor + tenderID;


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
    private void GetAwardingResult(string tenderID)
    {
        var url = UrlGetAwardingResult + "l1FcIiwZqe2uSgRYjuiCRA==";// tenderID;
        var chromeOptions = new ChromeOptions() { AcceptInsecureCertificates = true };
        chromeOptions.AddArguments("--headless=new"); // comment out for testing
        using (var driver = new ChromeDriver(chromeOptions))
        {
            driver.Navigate().GoToUrl(url);
            GetAwardingResult awardingResult = null;
            IWebElement table = driver.FindElement(By.XPath("//table[@summary='desc']"));
            IList<IWebElement> rows = table.FindElements(By.TagName("tr"));
            foreach (IWebElement row in rows)
            {
                IList<IWebElement> cells = row.FindElements(By.TagName("td"));
                foreach (IWebElement cell in cells)
                {
                    var test = cell.Text;
                    Console.WriteLine(cell.Text);
                }
            }
            IWebElement  table2 = driver.FindElements(By.XPath("//table[@summary='desc']")).Skip(0).SingleOrDefault();
            IList<IWebElement> table2_rows = table.FindElements(By.TagName("tr"));
            foreach (IWebElement row in rows)
            {
                IList<IWebElement> cells = row.FindElements(By.TagName("td"));
                foreach (IWebElement cell in cells)
                {
                    var test = cell.Text;
                    Console.WriteLine(cell.Text);
                }
            }

        }


    }
    private void GetRelationsDetails(string tenderID)
    {

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
