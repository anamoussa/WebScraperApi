using Carter;
using WebScraperApi.Services.Abstract;

namespace WebScraperApi;

public class WebScraper : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/Get", async (IDataService dataService, IScraperService scraperService) =>
        {
            var CardsBasicData = await dataService.GetTaskAsync();
            var CardsBasicDataIds = CardsBasicData.Select(t => t.tenderIdString).ToList();
            scraperService.GetCardsDetails(CardsBasicDataIds);

        }).WithDisplayName("GetAllData")
            .WithOpenApi();
    }





}
