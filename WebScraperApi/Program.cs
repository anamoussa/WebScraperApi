using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("/api/webscrapper_html", () =>
{
    var data = GetPageData("https://tenders.etimad.sa/Tender/AllTendersForVisitor");
    return data;
})
.WithOpenApi();
app.MapGet("/api/webscrapper_selen", () =>
{
    var data = GetDataWithSelenium("https://tenders.etimad.sa/Tender/AllTendersForVisitor");
    return data;
})
.WithOpenApi();

app.Run();

string GetPageData(string url)
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
string GetDataWithSelenium(string url)
{
    Console.OutputEncoding = System.Text.Encoding.Unicode;
    var chromeOptions = new ChromeOptions() { AcceptInsecureCertificates = true };
    chromeOptions.AddArguments("--headless=new"); // comment out for testing
    var driver = new ChromeDriver(chromeOptions);

    // scraping logic...
    driver.Navigate().GoToUrl(url);
    //var html = driver.PageSource;
    //Console.WriteLine(html);
    var productElements = driver.FindElements(By.XPath("//div[@class='etd-cards']//div[@class='row justify-content-center']//div[@id='cardsresult']//div[@class='row justify-content-center']//div[@class='col-12 col-md-3 p-0']"));
    foreach (var product in productElements)
    {
        var elements = product.FindElements(By.XPath("//div[@class='tender-metadata border-left border-bottom']//div[@class='row']"));
        foreach (var e in elements)
        {
            var sub = e.FindElements(By.XPath("//div[@class='col-6']"));
            foreach (var s in sub)
            {
                Console.WriteLine(s.Text);
            }
            Console.WriteLine(e.Text);
        }
        Console.WriteLine(product.TagName);
    }
    // close the browser and release its resources
    driver.Quit();
    return "";
}
