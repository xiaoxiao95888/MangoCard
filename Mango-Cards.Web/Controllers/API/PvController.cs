using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Mango_Cards.Web.Models.PV;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Mango_Cards.Web.Controllers.API
{
    public class PvController : BaseApiController
    {
        private readonly IMongoDatabase _mdb;
        public PvController()
        {
            var mongoClient = new MongoClient();
            _mdb = mongoClient.GetDatabase("pv_mangocard");
        }
        public object Post(PvRecord model)
        {
            var collection = _mdb.GetCollection<PvUser>("PvUsers");
            try
            {
                var filter = Builders<PvUser>.Filter.Eq("OpenId", model.PvUser.OpenId);
                var user = collection.Find(filter).FirstOrDefaultAsync().Result;
                if (user != null)
                {
                    if (user.City != model.PvUser.City
                        || user.Country != model.PvUser.Country
                        || user.Gender != model.PvUser.Gender
                        || user.Headimgurl != model.PvUser.Headimgurl
                        || user.Language != model.PvUser.Language
                        || user.NickName != model.PvUser.NickName
                        || user.Province != model.PvUser.Province)
                    {
                        var update = Builders<PvUser>.Update.Set("Country", model.PvUser.Country)
                            .Set("Gender", model.PvUser.Gender)
                            .Set("Headimgurl", model.PvUser.Headimgurl)
                            .Set("Language", model.PvUser.Language)
                            .Set("NickName", model.PvUser.NickName)
                            .Set("Province", model.PvUser.Province);
                        collection.UpdateOneAsync(filter, update);
                    }

                }
                else
                {
                    //插入user
                    collection.InsertOneAsync(model.PvUser);
                }
                //插入浏览记录
                var pvdetail = new PvDetail
                {
                    Id = ObjectId.GenerateNewId(),
                    OpenId = model.PvUser.OpenId,
                    MangoCardId = model.MangoCardId,
                    ViewDate = DateTime.Now
                };
                var pvdetailcollection = _mdb.GetCollection<PvDetail>("PvDetails");
                pvdetailcollection.InsertOneAsync(pvdetail);
            }
            catch (Exception)
            {

                return Failed();
            }
            return Success();
        }
        /// <summary>
        /// 根据mangocardId查询浏览记录
        /// </summary>
        /// <param name="id">mangocardId</param>
        /// <returns></returns>
        public object Get(Guid id)
        {
            var pvUserCollection = _mdb.GetCollection<PvUser>("PvUsers");
            var ufilter = Builders<PvUser>.Filter.Ne("OpenId", string.Empty);
            var pvDetailCollection = _mdb.GetCollection<PvDetail>("PvDetails");
            var dfilter = Builders<PvDetail>.Filter.Ne("OpenId", string.Empty);
            var model = new PvView
            {
                UserCount = (int?)pvUserCollection.Find(ufilter).CountAsync().Result,
                ViewCount = (int?)pvDetailCollection.Find(dfilter).CountAsync().Result,
            };
            return model;
        }
    }
}
