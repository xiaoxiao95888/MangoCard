using System;
using Mango_Cards.Web.MapperHelper.IMapperInterfaces;

namespace Mango_Cards.Web.MapperHelper
{
    public class MapperFactory : IMapperFactory
    {
        public ICardTypeMapper GetCardTypeMapper()
        {
            return new CardTypeMapper();
        }

        public IMangoCardMapper GetMangoCardMapper()
        {
            return new MangoCardMapper();
        }
    }
}