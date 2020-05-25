using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WizrrWeb.Models.AssetManagement
{
    public class AM_WorkFlowModel
    {
        public int MyProperty { get; set; }

        public string ActionType { get; set; }

        public string Status { get; set; }

        public string InternalStatus { get; set; }
    }
}