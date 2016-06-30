using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
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
            IMongoClient client = new MongoClient();
            _mdb = client.GetDatabase("pv_mangocard");
        }
        public object Post(PvRecord model)
        {
            var collection = _mdb.GetCollection<PvRecord>("PvRecord");
            try
            {
                model.DateTime = DateTime.Now;
                model.Id = ObjectId.GenerateNewId();
                model.PvUser.Id = ObjectId.GenerateNewId();
                collection.InsertOne(model);
                //var filter = Builders<PvRecord>.Filter.Eq<PvUser>("PvUser.OpenId", model.PvUser.OpenId);//这种写法也可以
                var filter = Builders<PvRecord>.Filter.Eq(n => n.PvUser.OpenId, model.PvUser.OpenId);
                var update = Builders<PvRecord>.Update.Set<PvUser>(n => n.PvUser, model.PvUser);
                collection.UpdateManyAsync(filter, update);
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
            var collection = _mdb.GetCollection<PvRecord>("PvRecord");
            var filter = Builders<PvRecord>.Filter.Eq(n => n.MangoCardId, id);
            var model = new
            {
                ViewCount = collection.Count(filter),
                UserCount =
                    collection.AsQueryable<PvRecord>()
                        .Where(n => n.MangoCardId == id)
                        .Select(n => n.PvUser)
                        .Distinct()
                        .Count(),
                TopUser =
                    collection.AsQueryable()
                        .Where(n => n.MangoCardId == id)
                        .GroupBy(n => n.PvUser)
                        .Select(n => new {WeChartUser = n.Key, Time = n.Select(p => p.DateTime).Last()})
                        .OrderByDescending(p => p.Time)
                        .Take(10)
            };
            return model;
        }
    }
}

