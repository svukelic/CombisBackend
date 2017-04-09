using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rsc_harambe.Models
{
    public class ModelPlans
    {
        public int id { get; set; }
        public string plan_name { get; set; }
        public int hotel_id { get; set; }
        public int transport_id { get; set; }

        public string hotel_name { get; set; }
        public string destination { get; set; }

        public ModelPlans(plans x)
        {
            this.id = x.id;
            this.plan_name = x.plan_naziv;
            this.hotel_id = x.hotel_id.Value;
            this.transport_id = x.transport_id.Value;
        }
    }
}