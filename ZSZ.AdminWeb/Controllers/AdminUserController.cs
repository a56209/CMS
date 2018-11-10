using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZSZ.IService;
using ZSZ.AdminWeb.Models;
using ZSZ.CommonMVC;
using ZSZ.AdminWeb.App_Start;

namespace ZSZ.AdminWeb.Controllers
{
    public class AdminUserController : Controller
    {
        public IAdminUserService userService { get; set; }
        public ICityService cityService { get; set; }
        public IRoleService roleService { get; set; }

        // GET: AdminUser
        public ActionResult Index()
        {
            return View();
        }

        [CheckPermission("AdminUser.List")]
        public ActionResult List()
        {
            var users = userService.GetAll();
            return View(users);
        }

        public ActionResult Delete(long id)
        {
            userService.MarkDeleted(id);
            return Json(new AjaxResult { Status = "ok" });
        }

        public ActionResult BatchDelete(long[] selectIds)
        {
            foreach(long id in selectIds)
            {
                userService.MarkDeleted(id);
            }
            return Json(new AjaxResult { Status = "ok" });
        }

        [HttpGet]
        public ActionResult Add()
        {
            var cities = cityService.GetAll().ToList();
            cities.Insert(0, new DTO.CityDTO { Id = 0, Name = "总部" });
            var roles = roleService.GetAll();
            AdminUserAddViewModel model = new AdminUserAddViewModel();
            model.Cities = cities.ToArray();
            model.Roles = roles;
            return View(model);
        }

        [HttpPost]
        public ActionResult Add(AdminUserAddModel model)
        {
            if(!ModelState.IsValid)
            {
                string msg = MVCHelper.GetValidMsg(ModelState);
                return Json(new AjaxResult { Status="error",ErrorMsg = msg});
            }

            //服务器端校验
            bool exists = userService.GetByPhoneNum(model.PhoneNum) != null;
            if (exists)
            {
                return Json(new AjaxResult
                {
                    Status = "error",
                    ErrorMsg = "手机号已经存在"
                });
            }
            long? cityId = null;
            if (model.CityId != 0)//cityId=0的时候为“总部”
            {
                cityId = model.CityId;
            }
            long userId = userService.AddAdminUser(model.Name,
                model.PhoneNum, model.Password, model.Email, cityId);
            roleService.AddRoleIds(userId, model.RoleIds);
            return Json(new AjaxResult { Status = "ok" });            
        }

        [HttpPost]
        public ActionResult CheckPhoneNum(string phoneNum,long? userId)
        {
            var user = userService.GetByPhoneNum(phoneNum);
            bool isOk = false;
            //如果没有给userId，则说明是“插入”，只要检查是不是存在这个手机号
            if(userId == null)
            {
                isOk = (user == null);
            }
            else  //有UserId,说明是修改,则要把自己排除在外
            {
                isOk = (user == null || user.Id == userId);
            }
            return Json(new AjaxResult { Status = isOk ? "ok" : "exists" });
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            var adminUser = userService.GetById(id);
            if (adminUser == null)
            {
                //不要忘了第二个参数的(object) 
                //如果视图在当前文件夹下没有找到，则去Shared下去找
                //一个Error视图，大家共用
                //return Redirect();
                return View("Error", (object)"id指定的操作员不存在");                
            }
            var cities = cityService.GetAll().ToList();
            cities.Insert(0, new DTO.CityDTO { Id = 0, Name = "总部" });
            var roles = roleService.GetAll().ToArray();
            //获得用户拥有的角色
            var userRoles = roleService.GetByAdminUserId(id);

            AdminUserEditViewModel model = new AdminUserEditViewModel();
            model.UserRoleIds = userRoles.Select(r => r.Id).ToArray();
            model.AdminUser = adminUser;
            model.Cities = cities.ToArray();
            model.Roles = roles;
            return View(model);
        }

        public ActionResult Edit(AdminUserEditModel model)
        {
            long? cityId = null;
            if (model.CityId > 0)//==0为总部
            {
                cityId = model.CityId;
            }
            userService.UpdateAdminUser(model.Id, model.Name,
                model.PhoneNum, model.Password, model.Email, cityId);
            roleService.UpdateRoleIds(model.Id, model.RoleIds);
            return Json(new AjaxResult { Status = "ok" });
        }
    }
}