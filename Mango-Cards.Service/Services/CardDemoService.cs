using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mango_Cards.Library.Models;
using Mango_Cards.Library.Services;

namespace Mango_Cards.Service.Services
{
    public class CardDemoService : BaseService, ICardDemoService
    {
        public CardDemoService(MangoCardsDataContext dbContext)
            : base(dbContext)
        {
        }
        public void Insert(CardDemo cardDemo)
        {
            DbContext.CardDemos.Add(cardDemo);
            DbContext.SaveChanges();
        }

        public void Update()
        {
            this.DbContext.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var carddemo = DbContext.CardDemos.FirstOrDefault(n => n.Id == id);
            DbContext.CardDemos.Remove(carddemo);
            Update();
        }

        public CardDemo GetCardDemo(Guid id)
        {
            return DbContext.CardDemos.FirstOrDefault(n => n.Id == id);
        }

        public IQueryable<CardDemo> GetCardDemos()
        {
            return DbContext.CardDemos.Where(n => !n.IsDeleted);
        }
       
    }
}
