using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Mango_Cards.Library.Services;

namespace Mango_Cards.Web.Controllers.API
{
    public class MangoCardController : BaseApiController
    {
        private readonly IMangoCardService _mangoCardService;
        private readonly string _cardThumbnailPath;
        public MangoCardController(IMangoCardService mangoCardService)
        {
            _mangoCardService = mangoCardService;
            _cardThumbnailPath = ConfigurationManager.AppSettings["CardThumbnailPath"];
        }

        
    }
}
