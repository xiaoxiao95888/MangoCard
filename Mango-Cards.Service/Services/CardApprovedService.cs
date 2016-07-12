using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mango_Cards.Library.Models;
using Mango_Cards.Library.Services;

namespace Mango_Cards.Service.Services
{
   public  class CardApprovedService : BaseService, ICardApprovedService
    {
        public CardApprovedService(MangoCardsDataContext dbContext)
            : base(dbContext)
        {
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public CardApproved GetCardApproved(Guid id)
        {
            return DbContext.CardApproveds.FirstOrDefault(n => n.Id == id);
        }

        public IQueryable<CardApproved> GetCardApproveds()
        {
            return DbContext.CardApproveds.Where(n => !n.IsDeleted);
        }

        public void Insert(CardApproved cardApproved)
        {
            DbContext.CardApproveds.Add(cardApproved);
            Update();
        }

        public void Update()
        {
            DbContext.SaveChanges();
        }
    }
}
