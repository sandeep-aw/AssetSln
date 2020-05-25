using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WizrrWeb.Models.AssetManagement
{
    public class AM_AssetCountHistoryModel
    {
        public int ID { get; set; }

        public string LID { get; set; }

        public string ActionType { get; set; }

        public string AssetCount { get; set; }
    }
}