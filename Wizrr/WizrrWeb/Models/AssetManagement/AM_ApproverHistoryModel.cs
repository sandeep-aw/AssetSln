using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WizrrWeb.Models.AssetManagement
{
    public class AM_ApproverHistoryModel
    {
        public int ID { get; set; }

        public string LID { get; set; }

        public string Status { get; set; }

        public string ApproverName { get; set; }

        public string ApproverCode { get; set; }
    }
}