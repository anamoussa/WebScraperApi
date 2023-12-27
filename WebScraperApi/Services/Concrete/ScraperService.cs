using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System.Globalization;
using WebScraperApi.Models;
using WebScraperApi.Helpers;
using WebScraperApi.Services.Abstract;

namespace WebScraperApi.Services.Concrete;

public class ScraperService : IScraperService
{
    private List<DetailsForVisitor> DetailsForVisitorsList = new();
    private List<GetTenderDate> GetTenderDatesList = new();
    private List<GetAwardingResult> GetAwardingResultsList = new();
    private List<GetRelationsDetail> GetRelationsDetailsList = new();

    public void GetCardsDetails(List<string> tenderIDs)
    {
        foreach (var tenderID in tenderIDs)
        {
            //var resuylt = GetAwardingResult(tenderID);

            //DetailsForVisitor(tenderID);
            GetTenderDates(tenderID);
            GetAwardingResult(tenderID);
            GetRelationsDetails(tenderID);
            Console.WriteLine(tenderIDs.IndexOf(tenderID));
        }
        //add DetailsForVisitorsList to DB table and save Changes 
    }

    private void GetTenderDates(string tenderID)
    {
        var url = Constants.UrlGetTenderDates + tenderID;
        var chromeOptions = new ChromeOptions() { AcceptInsecureCertificates = true };
        chromeOptions.AddArguments("--headless=new"); // comment out for testing

        using (var driver = new ChromeDriver(chromeOptions))
        {
            driver.Navigate().GoToUrl(url);
            GetTenderDate tenderDates = null;
            //IList<IWebElement> elements = driver.FindElements(By.XPath("//div[@class='col-4 etd-item-title']"));

            //// Extract the text from each element and store them in a list
            //List<string> elementTexts = new List<string>();
            //foreach (IWebElement element in elements)
            //{
            //    elementTexts.Add(element.Text.Trim());
            //}

            //// Output the scraped elements
            //foreach (string text in elementTexts)
            //{
            //    Console.WriteLine(text);
            //}


            tenderDates = new();
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
                        case "اخر موعد لإستلام الاستفسارات":
                            tenderDates.lastEnqueriesDate = span[0] == "لا يوجد" ? null : DateOnly.ParseExact(span[0], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            tenderDates.lastEnqueriesDateHijri = span[1];
                            break;
                        case "آخر موعد لتقديم العروض":
                            tenderDates.lastOfferPresentationDate = span[0] == "لا يوجد" ? null : DateOnly.ParseExact(span[0], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            tenderDates.lastOfferPresentationDateHijri = span[1];
                            tenderDates.lastOfferPresentationTime = span[2] == "لا يوجد" ? null : TimeOnly.ParseExact(span[2], "h:mm tt", CultureInfo.InvariantCulture);
                            break;
                        case "تاريخ فتح العروض":
                            tenderDates.offersOpeningDate = span[0] == "لا يوجد" ? null : DateTime.Parse(span[0]);
                            break;
                        case "تاريخ فحص العروض":
                            if (span[0] == "لا يوجد")
                                break;
                            tenderDates.offersExaminationDate = span[0] == "لا يوجد" ? null : DateOnly.ParseExact(span[0], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            tenderDates.offersExaminationDateHijri = span[1];
                            tenderDates.offersExaminationTime = span[2] == "لا يوجد" ? null : TimeOnly.ParseExact(span[2], "h:mm tt", CultureInfo.InvariantCulture);
                            break;
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
    private void GetTenderDatesOld(string tenderID)
    {
        var url = Constants.UrlGetTenderDates + tenderID;
        var chromeOptions = new ChromeOptions() { AcceptInsecureCertificates = true };
        chromeOptions.AddArguments("--headless=new"); // comment out for testing

        using (var driver = new ChromeDriver(chromeOptions))
        {
            driver.Navigate().GoToUrl(url);
            GetTenderDate tenderDates = null;

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
                    try
                    {
                        switch (txt)
                        {
                            case "اخر موعد لإستلام الاستفسارات":
                                tenderDates.lastEnqueriesDate = span[0] == "لا يوجد" ? null : DateOnly.ParseExact(span[0], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                tenderDates.lastEnqueriesDateHijri = span[1];
                                break;
                            case "آخر موعد لتقديم العروض":
                                tenderDates.lastOfferPresentationDate = span[0] == "لا يوجد" ? null : DateOnly.ParseExact(span[0], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                tenderDates.lastOfferPresentationDateHijri = span[1];
                                tenderDates.lastOfferPresentationTime = span[2] == "لا يوجد" ? null : TimeOnly.ParseExact(span[2], "h:mm tt", CultureInfo.InvariantCulture);
                                break;
                            case "تاريخ فتح العروض":
                                tenderDates.offersOpeningDate = span[0] == "لا يوجد" ? null : DateTime.Parse(span[0]);
                                break;
                            case "تاريخ فحص العروض":
                                if (span[0] == "لا يوجد")
                                    break;
                                tenderDates.offersExaminationDate = span[0] == "لا يوجد" ? null : DateOnly.ParseExact(span[0], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                tenderDates.offersExaminationDateHijri = span[1];
                                tenderDates.offersExaminationTime = span[2] == "لا يوجد" ? null : TimeOnly.ParseExact(span[2], "h:mm tt", CultureInfo.InvariantCulture);
                                break;
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
            }
            GetTenderDatesList.Add(tenderDates);
            driver.Quit();
        }
    }
    private void GetAwardingResult(string tenderID)
    {
        GetAwardingResult awardingResult = new GetAwardingResult();
        //  var url = UrlGetAwardingResult + "l1FcIiwZqe2uSgRYjuiCRA==";// tenderID;
        //  var url = UrlGetAwardingResult + "QLrdkFBEQtZctHDeIppNmw==";// tenderID;
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
        // var url = Constants.UrlGetRelationsDetail + "81fd6cZ5YX5xoZT8PuFuFg==";
        // var url = Constants.UrlGetRelationsDetail + "l1FcIiwZqe2uSgRYjuiCRA==";
        var url = Constants.UrlGetRelationsDetail + tenderID;
        var chromeOptions = new ChromeOptions() { AcceptInsecureCertificates = true };
        chromeOptions.AddArguments("--headless=new"); // comment out for testing
        var driver = new ChromeDriver(chromeOptions);
        driver.Navigate().GoToUrl(url);
        try
        {
            IList<IWebElement> listItems = driver.FindElements(By.CssSelector("ul.list-group.form-details-list li.list-group-item"));
            foreach (IWebElement listItem in listItems)
            {
                var titleElement = listItem.FindElement(By.CssSelector(".etd-item-title")).Text;
                var infoElement = listItem.FindElement(By.CssSelector(".etd-item-info")).Text;
                switch (titleElement)
                {
                    case "مكان التنفيذ":
                        getRelationsDetail.ExecutionLocation = infoElement;
                        break;
                    case "التفاصيل":
                        getRelationsDetail.Details = infoElement;
                        break;
                    case "نشاط المنافسة":
                        getRelationsDetail.CompetitionActivity = infoElement;
                        break;
                    case "تشمل المنافسة على بنود توريد":
                        getRelationsDetail.SupplyItemsCompetition = infoElement;
                        break;
                    case "أعمال الإنشاء":
                        getRelationsDetail.ConstructionWorks = infoElement;
                        break;
                    case "أعمال الصيانة والتشغيل":
                        getRelationsDetail.MaintenanceAndOperationWorks = infoElement;
                        break;
                }
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
        GetRelationsDetailsList.Add(getRelationsDetail);
    }
    private void DetailsForVisitor(string tenderID)
    {
        var url = Constants.UrlDetailsForVisitor + tenderID;


    }

}
