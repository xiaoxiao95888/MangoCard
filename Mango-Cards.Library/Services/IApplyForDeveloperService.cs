using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mango_Cards.Library.Models;

namespace Mango_Cards.Library.Services
{
    public interface IApplyForDeveloperService : IDisposable
    {
        void Insert(ApplyForDeveloper applyForDeveloper);
        void Update();
        void Delete(Guid id);
        ApplyForDeveloper GetApplyForDeveloper(Guid id);
        IQueryable<ApplyForDeveloper> GetApplyForDevelopers();
    }
}
