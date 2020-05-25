using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WizrrWeb.BAL.AssetManagement;
using WizrrWeb.Models.AssetManagement;

namespace WizrrWeb.Controllers.AssetManagement
{
    public class ApplyAssetController : Controller
    {
        // GET: ApplyAsset
        public ActionResult Index()
        {
            List<AM_AssetsModel> AssetModelValue = new List<AM_AssetsModel>();
            AssetModelValue = GetAssetsModels();
            ViewBag.AssetArr = AssetModelValue;
            return View();
        }

        [SharePointContextFilter]
        [ActionName("GetAssetsModels")]
        private List<AM_AssetsModel> GetAssetsModels()
        {
            List<AM_AssetsModel> AssetModel = new List<AM_AssetsModel>();

            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using(var clientContext=spContext.CreateUserClientContextForSPHost())
            {
                AM_AssetsBal assetsBal = new AM_AssetsBal();
                AssetModel = assetsBal.GetAssets(clientContext);
            }
            return AssetModel;
        }

    }
}