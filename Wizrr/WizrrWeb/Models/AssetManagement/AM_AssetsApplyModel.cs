using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WizrrWeb.Models.AssetManagement
{
    public class AM_AssetsApplyModel
    {
        public int ID { get; set; }

        public string RequestNo { get; set; }

        public string EmployeeName { get; set; }

        public string EmployeeCode { get; set; }

        public string CreatedName { get; set; }

        public string CreatedCode { get; set; }

        public string Asset { get; set; }

        public string AssetType { get; set; }

        public string AssetCount { get; set; }

        public string Warranty { get; set; }

        public string AssetDetails { get; set; }

        public string ReasonToApply { get; set; }

        public string RequestDate { get; set; }

        public string ReturnDate { get; set; }

        public string Status { get; set; }

        public string InternalStatus { get; set; }
    }
}