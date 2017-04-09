using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rsc_harambe.Models
{
    public class ModelLocation
    {
        public int id { get; set; }
        public string loc_name { get; set; }
        public string country { get; set; }

        public ModelLocation(locations x)
        {
            this.id = x.id;
            this.loc_name = x.loc_name;
            this.country= x.country;
        }
    }
}