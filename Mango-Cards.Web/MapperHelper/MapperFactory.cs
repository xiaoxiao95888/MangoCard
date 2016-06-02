using System;
using Mango_Cards.Web.MapperHelper.Implementation;
using Mango_Cards.Web.MapperHelper.IMapperInterfaces;

namespace Mango_Cards.Web.MapperHelper
{
    public class MapperFactory : IMapperFactory
    {
        public ICardTemplateMapper GetCardTemplateMapper()
        {
            return new CardTemplateMapper();
        }
        public ICardTypeMapper GetCardTypeMapper()
        {
            return new CardTypeMapper();
        }

        public IMangoCardAttributeMapper GetMangoCardAttributeMapper()
        {
            return new MangoCardAttributeMapper();
        }

        public IMangoCardMapper GetMangoCardMapper()
        {
            return new MangoCardMapper();
        }
        public IMediaMapper GetMediaMapper()
        {
            return new MediaMapper();
        }
        public IMediaTypeMapper GetMediaTypeMapper()
        {
            return new MediaTypeMapper();
        }
    }
}