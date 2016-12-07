using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace MakeYourPizza.WebUI.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css")
                .Include("~/Content/bootstrap.css",
                    "~/Content/themes/base/jquery-ui.css",
                    "~/Content/custom.css"));

            bundles.Add(new ScriptBundle("~/bundles/clientfeaturesscripts")
                .Include("~/Scripts/jquery-{version}.js",
                    "~/Scripts/bootstrap.js",
                    "~/Scripts/jquery-ui-{version}.js"));
                
        }
    }
}