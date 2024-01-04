namespace WebScraperApi.Endpoints;

public class WebScraper : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/Get", async (IDataService dataService, IScraperService scraperService) =>
        {
            //var CardsBasicData = await dataService.GetDataFromApiAsync();
           //  var CardsBasicDataFromDB = await dataService.GetCardsIDsPagesFromDBAsync();
           await scraperService.GetDataAsync();
           // scraperService.GetRelationsDetails("%20%20aI98vrLa4XXlr4Dicx2Q==");
           // scraperService.GetRelationsDetails("%20*@@**f7tUt3O1dpaFRkbPB4yQ==");

        }).WithOpenApi();
    }
}
