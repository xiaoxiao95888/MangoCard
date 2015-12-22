using Mango_Cards.Library.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mango_Cards.Library.Models;

namespace Mango_Cards.Service.Services
{
    public class WeChatUserService : BaseService, IWeChatUserService
    {
        public WeChatUserService(MangoCardsDataContext dbContext)
            : base(dbContext)
        {
        }

        public WeChatUser GetWeChatUser(string openId)
        {
            return DbContext.WeChatUsers.FirstOrDefault(n => n.OpenId == openId);
        }

        public WeChatUser GetWeChatUser(Guid id)
        {
            return DbContext.WeChatUsers.FirstOrDefault(n => n.Id == id);
        }

        public IQueryable<WeChatUser> GetWeChatUsers()
        {
            return DbContext.WeChatUsers.Where(n=>!n.IsDeleted);
        }

        public void Insert(WeChatUser wechatuser)
        {
            DbContext.WeChatUsers.Add(wechatuser);
            DbContext.SaveChanges();
        }

        public void Update()
        {
            DbContext.SaveChanges();
        }
    }
}
