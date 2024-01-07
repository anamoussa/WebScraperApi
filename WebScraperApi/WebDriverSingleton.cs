public class WebDriverSingleton 
{
    private static WebDriverSingleton instance;
    private static object lockObject = new object();
    private static IWebDriver driver;

    private WebDriverSingleton()
    {
        // Initialize the WebDriver instance here

        var chromeOptions = new ChromeOptions() { AcceptInsecureCertificates = true };
        chromeOptions.AddArguments("--headless=new"); // comment out for testing
        driver = new ChromeDriver(chromeOptions);
    }

    public static WebDriverSingleton Instance
    {
        get
        {
            if (instance == null)
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        instance = new WebDriverSingleton();
                    }
                }
            }
            return instance;
        }
    }

    public IWebDriver Driver
    {
        get { return driver; }
    }

    public static void Dispose()
    {
        driver.Quit();
        var chromeOptions = new ChromeOptions() { AcceptInsecureCertificates = true };
        chromeOptions.AddArguments("--headless=new"); // comment out for testing
        driver = new ChromeDriver(chromeOptions);
    }
}
