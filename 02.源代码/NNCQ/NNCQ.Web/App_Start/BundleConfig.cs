using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace NNCQ.Web.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-2.1.1.min.js",
                "~/Scripts/jquery.form.min.js",
                "~/Scripts/jquery.unobtrusive-ajax.min.js",
                "~/Scripts/jquery.widget.min.js",
                "~/Scripts/jquery.mousewheel.js",
                "~/Scripts/jquery.fileupload.js",
                "~/Scripts/jquery.iframe-transport.js",
                "~/Scripts/jquery.xdomainrequest.min.js",
                "~/Scripts/jquery.xdr-transport.js"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/metroScript").Include(
                "~/js/metro.min.js"));

            bundles.Add(new StyleBundle("~/metroCss").Include(
                "~/css/metro-bootstrap.css",
                "~/css/iconFont.min.css"
                ));
        }
    }
}