using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mango_Cards.Library.Models;
using Mango_Cards.Library.Services;

namespace Mango_Cards.Service.Services
{
    public class MediaTypeService : BaseService, IMediaTypeService
    {
        public MediaTypeService(MangoCardsDataContext dbContext)
            : base(dbContext)
        {
        }

        public MediaType GetMediaType(Guid id)
        {
            return DbContext.MediaTypes.FirstOrDefault(n => n.Id == id);
        }

        public IQueryable<MediaType> GetMediaTypes()
        {
            return DbContext.MediaTypes;
        }
    }
}
