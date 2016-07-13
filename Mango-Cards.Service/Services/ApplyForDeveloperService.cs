using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mango_Cards.Library.Models;
using Mango_Cards.Library.Services;

namespace Mango_Cards.Service.Services
{
    public class ApplyForDeveloperService : BaseService, IApplyForDeveloperService
    {
        public ApplyForDeveloperService(MangoCardsDataContext dbContext)
            : base(dbContext)
        {
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public ApplyForDeveloper GetApplyForDeveloper(Guid id)
        {
            return DbContext.ApplyForDevelopers.FirstOrDefault(n => n.Id == id);
        }

        public IQueryable<ApplyForDeveloper> GetApplyForDevelopers()
        {
            return DbContext.ApplyForDevelopers.Where(n => !n.IsDeleted);
        }

        public void Insert(ApplyForDeveloper applyForDeveloper)
        {
            DbContext.ApplyForDevelopers.Add(applyForDeveloper);
            Update();
        }

        public void Update()
        {
            DbContext.SaveChanges();
        }
    }
}
