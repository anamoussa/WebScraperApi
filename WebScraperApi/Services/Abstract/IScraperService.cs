namespace WebScraperApi.Services.Abstract;

public interface IScraperService
{
    void GetCardsDetails(List<string> tenderIDs);
}
