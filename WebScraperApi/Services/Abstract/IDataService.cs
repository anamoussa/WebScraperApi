namespace WebScraperApi.Services.Abstract;

public interface IDataService
{
    Task<List<CardBasicData>> GetDataFromApiAsync();
    Task<IEnumerable<string>> GetCardsIDsPagesFromDBAsync(int pageSize = 20000, int pageNumber = 1);
}
