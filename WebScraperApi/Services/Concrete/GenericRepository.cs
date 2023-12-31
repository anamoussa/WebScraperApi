using Microsoft.EntityFrameworkCore;
using WebScraperApi.Models;
using WebScraperApi.Models.Data;

namespace WebScraperApi.Services.Concrete
{
    public class GenericRepository
    {

        private readonly ScrapDBContext _context;

        public GenericRepository(ScrapDBContext context)
        {

            _context = context;
        }

        // CRUD operations for CardBasicDatas
        public void CreateCardBasicData(CardBasicData cardBasicData)
        {
            _context.CardBasicDatas.Add(cardBasicData);
            _context.SaveChanges();
        }

        public CardBasicData ReadCardBasicData(int id)
        {
            return _context.CardBasicDatas.Find(id);
        }

        public void UpdateCardBasicData(CardBasicData cardBasicData)
        {
            _context.CardBasicDatas.Update(cardBasicData);
            _context.SaveChanges();
        }

        public void DeleteCardBasicData(CardBasicData cardBasicData)
        {
            _context.CardBasicDatas.Remove(cardBasicData);
            _context.SaveChanges();
        }

        // Repeat the above pattern for other DbSet properties...

        // CRUD operations for DetailsForVisitors
        public void CreateDetailsForVisitor(GetDetailsForVisitor detailsForVisitor)
        {
            _context.DetailsForVisitors.Add(detailsForVisitor);
            _context.SaveChanges();
        }

        public GetDetailsForVisitor ReadDetailsForVisitor(int id)
        {
            return _context.DetailsForVisitors.Find(id);
        }

        public void UpdateDetailsForVisitor(GetDetailsForVisitor detailsForVisitor)
        {
            _context.DetailsForVisitors.Update(detailsForVisitor);
            _context.SaveChanges();
        }

        public void DeleteDetailsForVisitor(GetDetailsForVisitor detailsForVisitor)
        {
            _context.DetailsForVisitors.Remove(detailsForVisitor);
            _context.SaveChanges();
        }

        // Repeat the above pattern for other DbSet properties...

        // Add methods for other DbSet properties...

    }
}

