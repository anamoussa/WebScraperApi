namespace WebScraperApi.Services.Concrete;

public class DataService : IDataService
{
    public async Task<List<CardBasicData>> GetTaskAsync()
    {
        List<CardBasicData> CardsBasicData = new();
        string baseUrl = "https://tenders.etimad.sa/Tender/AllSupplierTendersForVisitorAsync?";
        int pageNumber = 500;
        int pageSize = 400;

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
                CardsBasicData.AddRange(cardsPage!.Data!);
                Console.WriteLine($"Page {pageNumber}:");
                pageNumber++;
                if (CardsBasicData.Count > 5)
                    break;
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
