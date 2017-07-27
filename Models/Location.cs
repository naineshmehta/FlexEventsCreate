using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetNuke.ComponentModel.DataAnnotations;
using DotNetNuke.Data;

namespace NM.Modules.FlexEventsCreate.Models
{
    [TableName("FlexLocation")]
    [Scope("ModuleId")]
    public class Location
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public int ModuleId { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
    }
}