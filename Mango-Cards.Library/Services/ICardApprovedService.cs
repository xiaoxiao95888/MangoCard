using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mango_Cards.Library.Models;

namespace Mango_Cards.Library.Services
{
    public interface ICardApprovedService : IDisposable
    {
        void Insert(CardApproved cardApproved);
        void Update();
        void Delete(Guid id);
        CardApproved GetCardApproved(Guid id);
        IQueryable<CardApproved> GetCardApproveds();
    }
}
