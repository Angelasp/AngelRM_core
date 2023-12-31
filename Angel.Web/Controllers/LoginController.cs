﻿using Microsoft.AspNetCore.Mvc;

namespace Angel.Web.Controllers
{
    /*************************************************************************
     * 文件名称 ：LoginController.cs                          
     * 描述说明 ：用户登录控制器类
     * 
     * 创建信息 : create by QQ：815657032、 Angel_asp@126.com on 2018-02-10
     * 修订信息 : modify by (person) on (date) for (reason)
     * 
     * 版权信息 : Copyright (c) 2009 Angel工作室 www.angelasp.com
     **************************************************************************/
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;

        public LoginController(ILogger<LoginController> logger)
        {
            _logger = logger;
        }
        //
        // GET: /Login/

        public ActionResult Index()
        {
            return View();
        }

    }
}
