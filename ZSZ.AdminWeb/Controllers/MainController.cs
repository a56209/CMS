using CaptchaGen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZSZ.AdminWeb.Models;
using ZSZ.COMMON;
using ZSZ.CommonMVC;
using ZSZ.IService;

namespace ZSZ.AdminWeb.Controllers
{
    public class MainController : Controller
    {
        public IAdminUserService userService { get; set; }
        // GET: Main
        public ActionResult Index()
        {
            long? userId = AdminHelper.GetUserId(HttpContext);
            if (userId == null)
            {
                return Redirect("~/Main/Login");
            }
            var user = userService.GetById((long)userId);
            return View(user);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            //string verifyCode = (string)Session["verifyCode"];  //密码111111
            string verifyCode = (string)TempData["verifyCode"];
            if (model.VerifyCode != verifyCode)
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "验证码错误" });
            }
            bool result = userService.CheckLogin(model.PhoneNum, model.Password);
            if (result)
            {
                //Session中保存当前登录用户Id
                Session["LoginUserId"]
                    = userService.GetByPhoneNum(model.PhoneNum).Id;
                return Json(new AjaxResult { Status = "ok" });
            }
            else
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "用户名或者密码错误" });
            }
        }

        public ActionResult CreatVerifyCode()
        {
            string verifyCode = CommonHelper.CreateVerifyCode(4);
            //Session["verifyCode"] = verifyCode;
            //使用TempData,用完删除
            TempData["verifyCode"] = verifyCode;
            MemoryStream ms = ImageFactory.GenerateImage(verifyCode, 60, 100, 20, 6);
            return File(ms, "image/jpeg");
        }
    }
}