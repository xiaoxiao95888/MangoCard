using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mango_Cards.Library.Models;

namespace Mango_Cards.Library.Services
{
    public interface IMediaService : IDisposable
    {
        void Delete(Guid id);
        void Insert(Media media);
        void Update();
        Media GetMedia(Guid id);
        IQueryable<Media> GetMediae();
    }
}
