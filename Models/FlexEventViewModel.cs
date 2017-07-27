using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NM.Modules.FlexEventsCreate.Models
{
    public class FlexEventViewModel
    {

        public string EventName { get; set; }
        public string Summary { get; set; }
        public string FullDescription { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public Location Location { get; set; }
        public List<SelectListItem> LocationList { get; set; }
        public int LocationId { get; set; }
        public HttpPostedFileBase Logo { get; set; }

    }
}