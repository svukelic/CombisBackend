using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rsc_harambe.Models
{
    public class ModelHotels
    {
        public int id { get; set; }
        public string hotel_name { get; set; }
        public string hotel_address { get; set; }
        public int stars { get; set; }
        public int loc_id { get; set; }

        public ModelHotels(hotels x)
        {
            this.id = x.id;
            this.hotel_name = x.hotel_name;
            this.hotel_address = x.hotel_address;
            this.stars = x.stars.Value;
            this.loc_id = x.loc_id;
        }
    }
}