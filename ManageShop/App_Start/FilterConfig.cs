﻿using System.Web;
using System.Web.Mvc;

namespace ManageShop
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new BasicAuthenticationAttribute());
            filters.Add(new HandleErrorAttribute());
        }
    }
}
