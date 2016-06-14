﻿using Mango_Cards.Library.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using AutoMapper;
using Mango_Cards.Library.Models;
using Mango_Cards.Web.Infrastructure.Filters;
using Mango_Cards.Web.MapperHelper;
using Mango_Cards.Web.Models;
using Microsoft.AspNet.Identity;

namespace Mango_Cards.Web.Controllers.API
{
    [Authorize]
    public class MediaController : BaseApiController
    {
        private readonly IMediaService _mediaService;
        private readonly IWeChatUserService _weChatUserService;
        public MediaController(IMediaService mediaService, IWeChatUserService weChatUserService, IMapperFactory mapperFactory)
        {
            _mediaService = mediaService;
            _weChatUserService = weChatUserService;
            mapperFactory.GetMediaMapper().Create();
        }

        public object Get()
        {
            var mediaTypeId = HttpContext.Current.Request["MediaTypeId"];
            var wechatuser = _weChatUserService.GetWeChatUser(User.Identity.GetUserId());
            var source = wechatuser.Mediae.Where(n => !n.IsDeleted);
            if (!string.IsNullOrEmpty(mediaTypeId))
            {
                var typeId = new Guid(mediaTypeId);
                source = source.Where(n => n.MediaTypeId == typeId);
            }
            var model = source.OrderByDescending(n => n.CreatedTime).Select(Mapper.Map<Media, MediaModel>).ToArray();
            return model;
        }
        public object Delete(Guid id)
        {
            var wechatuser = _weChatUserService.GetWeChatUser(User.Identity.GetUserId());
            var media = wechatuser.Mediae.FirstOrDefault(n => n.IsDeleted == false && n.Id == id);
            if (media != null)
            {
                var fileFullPath = ConfigurationManager.AppSettings["UploadFilePath"] + wechatuser.Id + @"\" + media.Name;
                if (media.Fields != null && media.Fields.Any())
                {
                    if (media.Fields.Any(p => p.CardTemplateId != null))
                    {
                        return Failed("系统素材,禁止删除。");
                    }
                    return Failed("该素材正在被使用，禁止删除。");
                }
                try
                {
                    _mediaService.Delete(id);
                    System.IO.File.Delete(fileFullPath);
                }
                catch
                {

                }
                return Success();
            }
            return Failed("找不到该文件，删除失败。");
        }
    }

}
