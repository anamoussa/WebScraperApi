namespace WebScraperApi.Services.Concrete;

public class DataService : IDataService
{
    private readonly ScrapDBContext _context;

    public DataService(ScrapDBContext context)
    {
        _context = context;
    }

    public async Task<List<CardBasicData>> GetTaskAsync()
    {
        List<CardBasicData> CardsBasicData = new();
        string baseUrl = "https://tenders.etimad.sa/Tender/AllSupplierTendersForVisitorAsync?";
        int pageNumber = 1;
        int pageSize = 4000;

        var handler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator,

        };
        using HttpClient httpClient = new(handler);
        while (true)
        {
            string apiUrl = $"{baseUrl}pageSize={pageSize}&pageNumber={pageNumber}";
            HttpResponseMessage response = await httpClient.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                CardsBasicDataResponse? cardsPage = Newtonsoft.Json.JsonConvert.DeserializeObject<CardsBasicDataResponse>(responseBody);
                if (cardsPage!.Data!.Count == 0)
                    break;
               // CardsBasicData.AddRange(cardsPage!.Data!);
                Console.WriteLine($"Page number {pageNumber}:");
                _context.CardBasicDatas.AddRange(cardsPage!.Data!);
                _context.SaveChanges();
                pageNumber++;
                //if (CardsBasicData.Count > 499)
                //    break;
            }
            else
            {
                Console.WriteLine($"Request failed with status code {response.StatusCode}");
                break; // Exit the loop if the request is not successful
            }
        }

        return CardsBasicData;
    }
}
