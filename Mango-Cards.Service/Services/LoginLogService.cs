using Mango_Cards.Library.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mango_Cards.Library.Models;

namespace Mango_Cards.Service.Services
{
    public class LoginLogService : BaseService, ILoginLogService
    {
        public LoginLogService(MangoCardsDataContext dbContext)
            : base(dbContext)
        {
        }
        public LoginLog GetLoginLog(string state)
        {
            return DbContext.LoginLogs.FirstOrDefault(n => n.State == state);
        }

        public LoginLog GetLoginLog(Guid id)
        {
            return DbContext.LoginLogs.FirstOrDefault(n => n.Id == id);
        }

        public IQueryable<LoginLog> GetLoginLogs()
        {
            return DbContext.LoginLogs;
        }

        public void Insert(LoginLog loginLog)
        {
            DbContext.LoginLogs.Add(loginLog);
            DbContext.SaveChanges();
        }

        public void Update()
        {
            DbContext.SaveChanges();
        }
    }
}
