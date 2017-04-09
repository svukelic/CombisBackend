using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rsc_harambe.Models
{
    public class ModelActivities
    {
        public int id { get; set; }
        public string activity_name { get; set; }
        public string activity_description { get; set; }
        public string season { get; set; }
        public int duration { get; set; }

        public ModelActivities(activities x)
        {
            this.id = x.id;
            this.activity_description = x.activity_description;
            this.activity_name = x.activity_name;
            this.season = x.season;
            this.duration = x.duration.Value;
        }
    } 
}