using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mango_Cards.Library.Models;
using Mango_Cards.Library.Services;

namespace Mango_Cards.Service.Services
{
    public class MediaService : BaseService, IMediaService
    {
        public MediaService(MangoCardsDataContext dbContext)
            : base(dbContext)
        {
        }

        public void Delete(Guid id)
        {
            var item = GetMedia(id);
            if (item != null)
            {
                item.IsDeleted = true;
                Update();
            }
        }

        public Media GetMedia(Guid id)
        {
            return DbContext.Mediae.FirstOrDefault(n => n.Id == id);
        }

        public IQueryable<Media> GetMediae()
        {
            return DbContext.Mediae.Where(n => !n.IsDeleted);
        }

        public void Insert(Media media)
        {
            DbContext.Mediae.Add(media);
            Update();
        }

        public void Update()
        {
            DbContext.SaveChanges();
        }
    }
}
