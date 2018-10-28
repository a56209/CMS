using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZSZ.IService;

namespace ZSZ.FrontWeb.Controllers
{
    public class MainController : Controller
    {
        public ICityService cityService { get; set; }
        // GET: Main
        public ActionResult Index()
        {
            string Id = cityService.AddNewe("深圳").ToString();
            //if (Session["test"] != null)
            //{
            //    return Content((string)Session["test"]);
            //}
            //string s = "abc";
            //Session["test"] = s;
            return Content("OK" + Id);
                      
        }
    }
}