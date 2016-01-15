using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mango_Cards.Library.Models;

namespace Mango_Cards.Library.Services
{
    public interface IMangoCardService : IDisposable
    {
        void Insert(MangoCard mangoCard);
        void Update();
        void Delete(Guid id);
        MangoCard GetMangoCard(Guid id);
        IQueryable<MangoCard> GetMangoCards();
    }
}
