using Mango_Cards.Web.MapperHelper.IMapperInterfaces;

namespace Mango_Cards.Web.MapperHelper
{
    public interface IMapperFactory
    {
        IMangoCardMapper GetMangoCardMapper();
        ICardTypeMapper GetCardTypeMapper();
        ICardTemplateMapper GetCardTemplateMapper();
        IMediaTypeMapper GetMediaTypeMapper();
        IMediaMapper GetMediaMapper();
        IMangoCardAttributeMapper GetMangoCardAttributeMapper();
    }
}
