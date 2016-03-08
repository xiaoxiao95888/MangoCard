using System;
using System.Data.Entity;
using Mango_Cards.Library.Models;
using Mango_Cards.Library.Models.Interfaces;
using Mango_Cards.Library.Services;

namespace Mango_Cards.Service
{
    public class MangoCardsDataContext : DbContext, IDataContext
    {
        public IDbSet<MediaType> MediaTypes { get; set; }
        public IDbSet<Media> Mediae { get; set; }
        public IDbSet<Company> Companies { get; set; }
        public IDbSet<Employee> Employees { get; set; }
        public IDbSet<CardType> CardTypes { get; set; }
        public IDbSet<LoginLog> LoginLogs { get; set; }
        public IDbSet<WeChatUser> WeChatUsers { get; set; }
        public IDbSet<MangoCard> MangoCards { get; set; }
        IDbSet<TEntity> IDataContext.Set<TEntity>()
        {
            return this.Set<TEntity>();
        }

        public override int SaveChanges()
        {
            var entities = ChangeTracker.Entries<IDtStamped>();

            foreach (var dtStamped in entities)
            {
                if (dtStamped.State == EntityState.Added)
                {
                    dtStamped.Entity.CreatedTime = DateTime.Now;
                }

                if (dtStamped.State == EntityState.Modified)
                {
                    dtStamped.Entity.UpdateTime = DateTime.Now;
                }
            }

            return base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Configurations.Add(new CategoryMapping());
            //modelBuilder.Configurations.Add(new AvatarMapping());
            //modelBuilder.Configurations.Add(new LetterMapping());
            //modelBuilder.Configurations.Add(new RetailerMapping());
            //modelBuilder.Configurations.Add(new SiteMapping());

            base.OnModelCreating(modelBuilder);
        }
    }

}
