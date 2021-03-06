﻿using System;
using System.Configuration;
using AutoMapper;
using Mango_Cards.Library.Models;
using Mango_Cards.Web.MapperHelper.IMapperInterfaces;
using Mango_Cards.Web.Models;
using System.Web;

namespace Mango_Cards.Web.MapperHelper.Implementation
{
    public class CardTemplateMapper : ICardTemplateMapper
    {
        private readonly string _cardThumbnailPath;
        public CardTemplateMapper()
        {
            _cardThumbnailPath = ConfigurationManager.AppSettings["CardThumbnailPath"];
        }
        public void Create()
        {
            throw new NotImplementedException();
        }

        public void Detail()
        {
            var mediaMapper = new MediaMapper();
            mediaMapper.Create();
            Mapper.CreateMap<Field, FieldModel>().ForMember(n => n.FieldType, opt => opt.MapFrom(src => src.FieldType))
                .ForMember(n => n.MediaModel, opt => opt.MapFrom(src => Mapper.Map<Media, MediaModel>(src.Media)));
            Mapper.CreateMap<CardTemplate, CardTemplateDetailModel>()
                .ForMember(n => n.UserId, opt => opt.MapFrom(src => src.WeChatUser.Id))
                .ForMember(n => n.UserName, opt => opt.MapFrom(src => src.WeChatUser.Name))
                .ForMember(n => n.CardTypeId, opt => opt.MapFrom(src => src.CardType.Id))
                .ForMember(n => n.CardTypeName, opt => opt.MapFrom(src => src.CardType.Name))
                .ForMember(n => n.ThumbnailUrl, opt => opt.MapFrom(src => _cardThumbnailPath + src.ThumbnailUrl))
                .ForMember(n => n.FieldModels, opt => opt.MapFrom(src => src.Fields));
        }

        public void Normal()
        {
            Mapper.CreateMap<CardTemplate, CardTemplateModel>()
                .ForMember(n => n.UserId, opt => opt.MapFrom(src => src.WeChatUser.Id))
                .ForMember(n => n.UserName, opt => opt.MapFrom(src => src.WeChatUser.Name))
                .ForMember(n => n.CardTypeId, opt => opt.MapFrom(src => src.CardType.Id))
                .ForMember(n => n.CardTypeName, opt => opt.MapFrom(src => src.CardType.Name))
                .ForMember(n => n.ThumbnailUrl, opt => opt.MapFrom(src => _cardThumbnailPath + src.ThumbnailUrl))
                 .ForMember(n => n.Url,
                    opt =>
                        opt.MapFrom(
                            src => $"http://{HttpContext.Current.Request.Url.Host}/Cards/RedirectCardTemplateView/{src.Id}"));
        }
    }
}