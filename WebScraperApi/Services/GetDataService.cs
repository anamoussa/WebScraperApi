using WebScraperApi.Models;

namespace WebScraperApi.Services
{
    public class GetDataService
    {
        public static async Task<List<CardBasicData>> GetTaskAsync()
        {
            List<CardBasicData> CardsBasicData = new List<CardBasicData>();
            string baseUrl = "https://tenders.etimad.sa/Tender/AllSupplierTendersForVisitorAsync?";
            int pageNumber = 1;
            int pageSize = 10;
            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
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
                            break;
                        }
                        else
                        {
                            Console.WriteLine($"Request failed with status code {response.StatusCode}");
                            break; // Exit the loop if the request is not successful
                        }
                    }
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine($"Request failed with exception: {ex.Message}");
                }
            }
            return CardsBasicData;
        }
    }
}
