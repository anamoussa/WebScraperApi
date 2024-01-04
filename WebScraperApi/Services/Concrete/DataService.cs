namespace WebScraperApi.Services.Concrete;

public class DataService : IDataService
{
    private readonly ScrapDBContext _context;

    public DataService(ScrapDBContext context)
    {
        _context = context;
    }

    public async Task<List<CardBasicData>> GetDataFromApiAsync()
    {
        List<CardBasicData> CardsBasicData = new();
        string baseUrl = "https://tenders.etimad.sa/Tender/AllSupplierTendersForVisitorAsync?";
        int pageNumber = 1;
        int pageSize = 20000;

        var handler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator,
        };

        using HttpClient httpClient = new(handler);
        httpClient.Timeout = TimeSpan.FromMinutes(2);
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
                var tobeinsertedids = cardsPage!.Data!.Select(o => o.tenderIdString).ToList();
                var existitems = _context.CardBasicDatas.Where(o => tobeinsertedids.Contains(o.tenderIdString));
                _context.CardBasicDatas.RemoveRange(existitems);
                _context.CardBasicDatas.AddRange(cardsPage!.Data!);
                await _context.SaveChangesAsync();
                pageNumber++;
            }
            else
            {
                Console.WriteLine($"Request failed with status code {response.StatusCode}");
                break; // Exit the loop if the request is not successful
            }
        }

        return CardsBasicData;
    }
    public async Task<IEnumerable<string>> GetCardsIDsPagesFromDBAsync(int pageSize = 20000, int pageNumber = 1)
    {
        var cards = await _context
            .CardBasicDatas
            .Select(c => c.tenderIdString)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking()
            .ToListAsync();
        return cards;
    }
}
