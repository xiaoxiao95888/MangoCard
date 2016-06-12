using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Mango_Cards.Library.Services;
using Mango_Cards.Web.Infrastructure;
using Mango_Cards.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace Mango_Cards.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWeChatUserService _weChatUserService;
        public HomeController( IWeChatUserService weChatUserService)
        {
            _weChatUserService = weChatUserService;
        }
        public ActionResult Index()
        {
            //test
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult LoginConfirmation()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //[HttpPost]
        //public ActionResult Upload()
        //{
        //    var file = System.Web.HttpContext.Current.Request.Files[0];
        //    var uploadFilePath = ConfigurationManager.AppSettings["UploadFilePath"];
        //    if (!Directory.Exists(uploadFilePath))
        //    {
        //        Directory.CreateDirectory(uploadFilePath);
        //    }
        //    var fileNamePrefix = Guid.NewGuid().ToString();
        //    if (file.ContentLength > 0)
        //    {
        //        var fileName = Path.GetFileName(file.FileName);
        //        var fileExtension = Path.GetExtension(fileName);
        //        var newFileName = fileNamePrefix + fileExtension;
        //        var fileFullPath = uploadFilePath + newFileName;
        //        try
        //        {
        //            file.SaveAs(uploadFilePath + newFileName);
        //            var list = TemplateTransformHelp.GetDataList(selectedDistributor, fileFullPath).Where(n => n.Product != null).ToArray();
        //            var salesFile = new SalesFlowsFile
        //            {
        //                Name = fileName,
        //                Employee = user,
        //                Id = Guid.NewGuid(),
        //                LocalFileName = newFileName,
        //                //ReportMonth = model.ReporTime,
        //                Distributor = selectedDistributor,
        //                Code = salesFileCode
        //                //Code = string.Format("{0}-{1}-{2}-{3}", selectedDistributor != null ? selectedDistributor.Code : string.Empty, model.ReporTime.Year, model.ReporTime.Month, salesFlowsFilescount + 1)
        //            };
        //            var salesFlows = TransformModel(list, user)
        //                .ToArray();
        //            salesFile.SalesFlows = salesFlows;
        //            var salesDate = salesFile.SalesFlows.Select(n => n.SalesDate).Max().AddMonths(1);
        //            salesFile.ReportMonth = new DateTime(salesDate.Year, (salesDate.Month), 1);

        //        }
        //        catch (Exception ex)
        //        {
        //            System.IO.File.Delete(fileFullPath);
        //            return 
        //        }
        //    }
        //    ViewBag.Message = "File(s) uploaded successfully";
        //    return RedirectToAction("Index");

        //}
    }
}