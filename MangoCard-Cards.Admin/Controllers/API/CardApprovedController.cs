using Mango_Cards.Library.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MangoCard_Cards.Admin.Models;
using System.Web;

namespace MangoCard_Cards.Admin.Controllers.API
{
    [Authorize]
    public class CardApprovedController : BaseApiController
    {
        private readonly ICardTemplateService _cardTemplateService;
        private readonly IMangoCardService _mangoCardService;
        private readonly ICardApprovedService _cardApprovedService;
        public CardApprovedController(IMangoCardService mangoCardService, ICardTemplateService cardTemplateService, ICardApprovedService cardApprovedService)
        {
            _mangoCardService = mangoCardService;
            _cardTemplateService = cardTemplateService;
            _cardApprovedService = cardApprovedService;
        }

        public object Get()
        {
            var model = _cardApprovedService.GetCardApproveds().ToArray().Select(n => new CardApprovedModel
            {
                Code = n.MangoCard.Code,
                Description = n.MangoCard.Description,
                Id = n.Id,
                MangocardId = n.MangoCardId,
                Title = n.MangoCard.Title,
                UpdateTime = n.UpdateTime.GetValueOrDefault(),
                Url = $"{ConfigurationManager.AppSettings["MangoCardWebHost"]}/Cards/View/{n.MangoCardId}"
            }).ToArray();
            return model;
        }
    }
}
