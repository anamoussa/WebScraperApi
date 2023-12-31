namespace WebScraperApi.Services.Abstract;

public interface IDataService
{
    Task<List<CardBasicData>> GetTaskAsync();
}
