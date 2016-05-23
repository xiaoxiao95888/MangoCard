using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mango_Cards.Library.Models;

namespace Mango_Cards.Library.Services
{
    public interface IDataContext : IObjectContextAdapter, IDisposable
    {
        IDbSet<CardTemplate> CardTemplates { get; set; }
        IDbSet<Media> Mediae { get; set; }
        IDbSet<MediaType> MediaTypes { get; set; }
        IDbSet<Company> Companies { get; set; }
        IDbSet<Employee> Employees { get; set; }
        IDbSet<CardType> CardTypes { get; set; }
        IDbSet<LoginLog> LoginLogs { get; set; }
        IDbSet<WeChatUser> WeChatUsers { get; set; }
        IDbSet<MangoCard> MangoCards { get; set; }
        int SaveChanges();
        IDbSet<TEntity> Set<TEntity>() where TEntity : class;
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    }
}
