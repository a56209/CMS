using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZSZ.AdminWeb.Models;
using ZSZ.CommonMVC;
using ZSZ.IService;

namespace ZSZ.AdminWeb.Controllers
{
    public class RoleController : Controller
    {
        public IRoleService roleService { get; set; }
        public IPermissionService permService { get; set; }

        // GET: Role
        public ActionResult List()
        {
            var roles = roleService.GetAll();
            return View(roles);
        }

        [HttpPost]
        public ActionResult Delete(long id)
        {
            roleService.MarkDelete(id);
            return Json(new AjaxResult { Status = "ok" });
        }

        [HttpPost]
        //参数名称与前端name保持一致(参数映射与请求一致)
        public ActionResult BatchDelete(long[] selectdIds)
        {
            foreach (long id in selectdIds)
            {
                roleService.MarkDelete(id);
            }
            return Json(new AjaxResult { Status = "ok" });
        }

        [HttpGet]
        public ActionResult Add()
        {
            var perms = permService.GetAll();//所有可用的权限项
            return View(perms);            
        }

        [HttpPost]
        public ActionResult Add(RoleAddModel model)
        {
            long roleId = roleService.AddNew(model.Name);
            permService.AddPermIds(roleId, model.PermissionIds);
            return Json(new AjaxResult { Status = "ok" });
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            var role = roleService.GetById(id);
            var rolePerms = permService.GerByRoleId(id);
            var allPerms = permService.GetAll();

            RoleEditGetModel model = new RoleEditGetModel();
            model.Role = role;
            model.RolePerms = rolePerms;
            model.AllPerms = allPerms;
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(RoleEditModel model)
        {
            roleService.Update(model.Id, model.Name);
            permService.UpdatePermIds(model.Id, model.PermissionIds);            
            return Json(new AjaxResult { Status = "ok" });
        }
    }
}
