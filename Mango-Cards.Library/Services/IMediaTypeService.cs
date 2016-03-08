using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mango_Cards.Library.Models;

namespace Mango_Cards.Library.Services
{
   public  interface IMediaTypeService : IDisposable
    {
        MediaType GetMediaType(Guid id);
        IQueryable<MediaType> GetMediaTypes();
    }

}
