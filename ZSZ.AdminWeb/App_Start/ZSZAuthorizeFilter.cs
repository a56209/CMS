using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZSZ.CommonMVC;
using ZSZ.IService;

namespace ZSZ.AdminWeb.App_Start
{
    public class ZSZAuthorizeFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            //获得当前要执行的Action上标注的CheckPermissionAttribute实例对象
            CheckPermissionAttribute[] permAtts = (CheckPermissionAttribute[])filterContext
                .ActionDescriptor.GetCustomAttributes(typeof(CheckPermissionAttribute), false);
            
            //没有标注任何的CheckPermissionAttribute，因此也就不需要检查是否登录
            if (permAtts.Length <= 0)                                     
            {
                return;//登录等这些不要求有用户登录的功能
            }
            //得到当前登录用户的id
            long? userId = (long?)filterContext.HttpContext.Session["LoginUserId"];
            if(userId == null)
            {
                //根据不同的请求，给予不同的返回格式。确保ajax请求，浏览器端也能收到json格式
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    AjaxResult ajaxResult = new AjaxResult();
                    ajaxResult.Status = "redirect";
                    ajaxResult.Data = "/Main/Login";
                    ajaxResult.ErrorMsg = "没有登录";
                    filterContext.Result = new JsonNetResult { Data = ajaxResult };
                }
                else
                {
                    //filterContext.Result = new ContentResult() { Content = "没有登录" };
                    filterContext.Result = new RedirectResult("~/Main/Login");
                }
                return;
            }

            //由于ZSZAuthorizeFilter不是被autofac创建，因此不会自动进行属性的注入
            //需要手动获取Service对象
            IAdminUserService userService =
                DependencyResolver.Current.GetService<IAdminUserService>();

            foreach (var permAtt in permAtts)
            {
                if(!userService.HasPermission(userId.Value,permAtt.Permission))
                {
                    //只要有一个没有权限，就禁止访问
                    //在IAuthorizationFilter里面，只要修改filterContext.Result 
                    //那么真正的Action方法就不会执行了
                    if (filterContext.HttpContext.Request.IsAjaxRequest())
                    {
                        AjaxResult ajaxResult = new AjaxResult();
                        ajaxResult.Status = "error";
                        ajaxResult.ErrorMsg = "没有权限" + permAtt.Permission;
                        filterContext.Result = new JsonNetResult { Data = ajaxResult };
                    }
                    else
                    { 
                    filterContext.Result =
                        new ContentResult() { Content = "没有" + permAtt.Permission + "权限" };
                    }
                    return;
                    
                }
            }
            return;
        }
    }
}