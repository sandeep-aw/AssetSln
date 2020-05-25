using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WizrrWeb.Models.AssetManagement;
using Newtonsoft.Json.Linq;
using WizrrWeb.DAL;
using WizrrWeb.Models;

namespace WizrrWeb.BAL.AssetManagement
{
    public class AM_AssetsBal
    {

        public List<AM_AssetsModel> GetAssets(ClientContext clientContext)
        {
            List<AM_AssetsModel> assetsModelsBal = new List<AM_AssetsModel>();

            JArray jArray = RestGetAssets(clientContext);

            foreach(JObject j in jArray)
            {
                assetsModelsBal.Add(new AM_AssetsModel
                {
                    ID = Convert.ToInt32(j["ID"]),
                    Assets = Convert.ToString(j["Assets"])
                });
            }
            System.Diagnostics.Debug.WriteLine(assetsModelsBal);
            return assetsModelsBal;
        }

        private JArray RestGetAssets(ClientContext clientContext)
        {
            RestService restService = new RestService();
            JArray jArray = new JArray();
            RESTOption rESTOption = new RESTOption();

            rESTOption.select = "ID,Assets";

            jArray = restService.GetAllItemFromList(clientContext, "AM_Assets", rESTOption);

            return jArray;
        }
    }
}