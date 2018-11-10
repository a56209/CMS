using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
        }
    }
}