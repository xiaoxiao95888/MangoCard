using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mango_Cards.Library.Models;

namespace Mango_Cards.Library.Services
{
    public interface ICardTemplateService : IDisposable
    {
        void Insert(CardTemplate cardTemplate);
        void Update();
        CardTemplate GetCardTemplate(Guid id);
        IQueryable<CardTemplate> GetCardTemplates();
    }
}
