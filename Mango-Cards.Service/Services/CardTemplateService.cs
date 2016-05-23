using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mango_Cards.Library.Models;
using Mango_Cards.Library.Services;

namespace Mango_Cards.Service.Services
{
    public class CardTemplateService : BaseService, ICardTemplateService
    {
        public CardTemplateService(MangoCardsDataContext dbContext)
            : base(dbContext)
        {
        }

        public CardTemplate GetCardTemplate(Guid id)
        {
            return DbContext.CardTemplates.FirstOrDefault(n => n.Id == id);
        }

        public IQueryable<CardTemplate> GetCardTemplates()
        {
            return DbContext.CardTemplates.Where(n => !n.IsDeleted);
        }

        public void Insert(CardTemplate cardTemplate)
        {
            DbContext.CardTemplates.Add(cardTemplate);
            Update();
        }

        public void Update()
        {
            DbContext.SaveChanges();
        }
    }
}
