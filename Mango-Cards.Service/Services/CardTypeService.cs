using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mango_Cards.Library.Models;
using Mango_Cards.Library.Services;

namespace Mango_Cards.Service.Services
{
    public class CardTypeService : BaseService, ICardTypeService
    {
        public CardTypeService(MangoCardsDataContext dbContext)
            : base(dbContext)
        {
        }
        public void Insert(CardType cardType)
        {
            DbContext.CardTypes.Add(cardType);
            DbContext.SaveChanges();
        }

        public void Update()
        {
            DbContext.SaveChanges();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public CardType GetCardType(Guid id)
        {
            return DbContext.CardTypes.FirstOrDefault(n => n.Id == id);
        }

        public IQueryable<CardType> GetCardTypes()
        {
            return DbContext.CardTypes;
        }
    }
}
