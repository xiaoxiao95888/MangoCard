﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using AutoMapper;
using Mango_Cards.Library.Models;
using Mango_Cards.Library.Models.Enum;
using Mango_Cards.Library.Services;
using Mango_Cards.Web.MapperHelper;
using Mango_Cards.Web.MapperHelper.IMapperInterfaces;
using Mango_Cards.Web.Models;

namespace Mango_Cards.Web.Controllers.API
{
    public class CardTypeController : BaseApiController
    {
        private readonly ICardTypeService _cardTypeService;
        private readonly string _cardThumbnailPath;
        public CardTypeController(ICardTypeService cardTypeService, IMapperFactory mapperFactory)
        {
            _cardTypeService = cardTypeService;
            mapperFactory.GetMangoCardMapper().Create();
            mapperFactory.GetCardTypeMapper().Create();
            _cardThumbnailPath = ConfigurationManager.AppSettings["CardThumbnailPath"];
        }
        public object Get()
        {
            var host = HttpContext.Current.Request.Url.Host;
            var all = _cardTypeService.GetCardTypes().ToList();
            var roots = all.Where(n => n.Parent == null).ToList();
            var result = new List<CardTypeModel>();
            Recursion(roots, all, result);
            return result;
        }

        public object Get(Guid id)
        {
            var host = HttpContext.Current.Request.Url.Host;
            var all = _cardTypeService.GetCardTypes().ToList();
            var root = all.FirstOrDefault(n => n.Id == id);
            var result = new CardTypeModel();
            Recursion(root, all, result);
            return result;
        }

        /// <summary>
        /// 父类迭代子类返回实体
        /// </summary>
        /// <param name="roots"></param>
        /// <param name="all"></param>
        /// <param name="models"></param>
        /// <param name="currentModel"></param>
        public static void Recursion(IEnumerable<CardType> roots, List<CardType> all, ICollection<CardTypeModel> models,
            CardTypeModel currentModel = null)
        {
            foreach (var root in roots)
            {
                var model = Mapper.Map<CardType, CardTypeModel>(root);

                if (currentModel != null && currentModel.Id == root.Parent.Id)
                {
                    if (currentModel.SubCardTypeModels == null)
                    {
                        currentModel.SubCardTypeModels = new List<CardTypeModel>();
                    }
                    currentModel.SubCardTypeModels.Add(model);
                }
                else
                {
                    models.Add(model);
                }
                var subs = all.Where(n => n.Parent == root).ToArray();
                if (subs.Any())
                {
                    Recursion(subs, all, models, model);
                }
            }
        }
        /// <summary>
        /// 父类迭代子类返回实体
        /// </summary>
        /// <param name="root"></param>
        /// <param name="all"></param>
        /// <param name="currentModel"></param>
        public static void Recursion(CardType root, List<CardType> all, CardTypeModel currentModel = null)
        {
            currentModel = Mapper.Map<CardType, CardTypeModel>(root);
            var subs = all.Where(n => n.Parent.Id == currentModel.Id);
            foreach (var cardType in subs)
            {
                if (currentModel.SubCardTypeModels == null)
                {
                    currentModel.SubCardTypeModels = new List<CardTypeModel>();
                }
                currentModel.SubCardTypeModels.Add(Mapper.Map<CardType, CardTypeModel>(cardType));
                Recursion(cardType, all, currentModel);
            }
        }
    }
}
