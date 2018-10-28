using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZSZ.IService;
using ZSZ.CommonMVC;
using ZSZ.AdminWeb.Models;

namespace ZSZ.AdminWeb.Controllers
{
    public class PermissionController : Controller
    {
        public IPermissionService permSvc { get; set; }
        // GET: Permission
        public ActionResult List()
        {
            var perms = permSvc.GetAll();
            return View(perms);
        }

        public ActionResult Delete(long Id)
        {
            permSvc.MarkDeleted(Id);
            return Json(new AjaxResult { Status = "ok" });
        }

        public ActionResult GetDelete(long Id)
        {
            permSvc.MarkDeleted(Id);
            //重定向，页面刷新。不建议用，有个可能有预加载的问题
            return RedirectToAction(nameof(List));
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(PermissionAddNewModel model)
        {
            var perm = permSvc.GetByName(model.Name);
            if (perm == null)
            { 
            permSvc.AddPermission(model.Name, model.Description);            
            return Json(new AjaxResult { Status = "ok" });
            }
            else
            {
                return Json(new AjaxResult { ErrorMsg = "该权限已存在:" + model.Name });
            }
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            var perm = permSvc.GetById(id);
            return View(perm);
        }

        public ActionResult Edit(PermissionEditModel model)
        {
            permSvc.UpdatePermission(model.Id, model.Name, model.Description);
            return Json(new AjaxResult { Status = "ok" });
        }

    }
}