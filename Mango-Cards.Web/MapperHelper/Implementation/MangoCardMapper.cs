using System.Configuration;
using System.Web;
using AutoMapper;
using Mango_Cards.Library.Models;
using Mango_Cards.Web.Infrastructure.Filters;
using Mango_Cards.Web.MapperHelper.IMapperInterfaces;
using Mango_Cards.Web.Models;

namespace Mango_Cards.Web.MapperHelper.Implementation
{
    public class MangoCardMapper : IMangoCardMapper
    {
        private readonly string _cardThumbnailPath;
        public MangoCardMapper()
        {
            _cardThumbnailPath = ConfigurationManager.AppSettings["CardThumbnailPath"];
        }
        public void Create()
        {
            Mapper.CreateMap<MangoCard, MangoCardModel>()
                .ForMember(n => n.CardTypeId, opt => opt.MapFrom(src => src.CardTemplate.CardType.Id))
                .ForMember(n => n.ThumbnailUrl, opt => opt.MapFrom(src => _cardThumbnailPath + src.ThumbnailUrl))
                .ForMember(n => n.Url,
                    opt =>
                        opt.MapFrom(
                            src =>
                                $"http://{HttpContext.Current.Request.Url.Host}/Cards/RedirectCardView/{src.Id}"
                            ));

        }
    }
}