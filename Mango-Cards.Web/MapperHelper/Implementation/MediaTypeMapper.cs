using AutoMapper;
using Mango_Cards.Library.Models;
using Mango_Cards.Web.MapperHelper.IMapperInterfaces;
using Mango_Cards.Web.Models;

namespace Mango_Cards.Web.MapperHelper.Implementation
{
    public class MediaTypeMapper : IMediaTypeMapper
    {
        public void Create()
        {
            Mapper.CreateMap<MediaType, MediaTypeModel>();
        }
    }
}