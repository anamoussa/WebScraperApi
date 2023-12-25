using HtmlAgilityPack;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using Carter;
using OpenQA.Selenium.Support.UI;

namespace WebScraperApi;

public class WebScraper : ICarterModule
{
    private IDictionary<string, object> DetailsDict = new Dictionary<string, object>();
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/webscraper");
        group.MapGet("/htmlagilitypack", () =>
        {
            var data = GetPageData("https://tenders.etimad.sa/Tender/AllTendersForVisitor");
            return data;
        }).WithOpenApi();
        group.MapGet("/selenium", () =>
        {
            var data = GetDataWithSelenium("https://tenders.etimad.sa/Tender/AllTendersForVisitor");
            return data;
        }).WithOpenApi();
    }
    private string GetPageData(string url)
    {
        HtmlWeb htmlWeb = new HtmlWeb()
        {
            PreRequest = (d) =>
            {
                d.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) =>
                {
                    return true;
                };
                return true;
            }
        };
        var document = htmlWeb.Load(url);
        var divElements = document.DocumentNode.SelectNodes("//div[@class='etd-cards']//div[@class='row justify-content-center']//div[@id='cardsresult']//div[@class='row justify-content-center']//div[@class='col-12 col-md-3 p-0']").ToArray();

        if (divElements != null)
        {
            foreach (var divElement in divElements)
            {
                Console.WriteLine(divElement.InnerHtml);
            }
        }
        return "";
    }
    private string GetDataWithSelenium(string url)
    {
        var chromeOptions = new ChromeOptions() { AcceptInsecureCertificates = true };
        chromeOptions.AddArguments("--headless=new"); // comment out for testing
        var driver = new ChromeDriver(chromeOptions);

        // scraping logic...
        driver.Navigate().GoToUrl(url);
        //var html = driver.PageSource;
        //Console.WriteLine(html);
        var Cards = driver.FindElements(By.XPath("//div[@id='cardsresult']//div[@class='row justify-content-center']//div[@class='col-12 col-md-12 mb-4']"));
        foreach (var card in Cards)
        {
            //var details_btns = card.FindElements(By.XPath("//div[@class='tender-card rounded card mt-0 mb-0']//div[@class='row']//div[@class='col-12 col-md-9 p-0']//div[@class='tender-metadata border-left border-bottom']//div[@class='row']//div[@class='col-12 border-bottom']//div[@class='row']//div[@class='col-12']//div[@class='pb-2']//div[@class='pull-right']"));
            var details_btn = card.FindElement(By.ClassName("pull-right"));

            details_btn.Click();

            GetDetails(driver);
            driver.Navigate().Back();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
            Cards = driver.FindElements(By.XPath("//div[@id='cardsresult']//div[@class='row justify-content-center']//div[@class='col-12 col-md-12 mb-4']"));
        }
        GetPagination(driver);
        // close the browser and release its resources
        driver.Quit();
        return "";
    }
    private void GetDetails(ChromeDriver driver)
    {
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
                DetailsDict.Add(title, info);
            }
        }

    }
    private void GetPagination(ChromeDriver driver)
    {
        var page = driver.FindElements(By.ClassName("pagination-primary"))[0];
        var items = page.FindElements(By.ClassName("page-item"));
        foreach (var item in items)
        {
            var btn = item.FindElement(By.ClassName("page-link"));
            var next = btn.GetAttribute("aria-label");
            var isDisabled = btn.GetAttribute("disabled");
            if (next == "Next" && string.IsNullOrEmpty(isDisabled))
            {
                btn.Click();
            }
        }

    }
}
