using System;
using System.Linq;
using Mango_Cards.Library.Models;
using Mango_Cards.Library.Services;

namespace Mango_Cards.Service.Services
{
    public class MangoCardService : BaseService, IMangoCardService
    {
        public MangoCardService(MangoCardsDataContext dbContext)
            : base(dbContext)
        {
        }

        public void Insert(MangoCard mangoCard)
        {
            DbContext.MangoCards.Add(mangoCard);
            DbContext.SaveChanges();
        }

        public void Update()
        {
            DbContext.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var card = GetMangoCard(id);
            if (card == null) return;
            card.IsDeleted = true;
            Update();
        }
        

        public IQueryable<MangoCard> GetMangoCards()
        {
            return DbContext.MangoCards.Where(n => !n.IsDeleted);
        }
        public IQueryable<MangoCard> GetAllMangoCards()
        {
            return DbContext.MangoCards;
        }
        public MangoCard GetMangoCard(Guid id)
        {
            return DbContext.MangoCards.FirstOrDefault(n => n.Id == id);
        }
    }
}
