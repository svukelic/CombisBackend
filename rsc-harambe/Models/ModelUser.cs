using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rsc_harambe.Models
{
    public class ModelUser
    {
        public int id { get; set; }
        public string ime { get; set; }
        public string email { get; set; }
        public int current_hotel { get; set; }

        public ModelUser(users x)
        {
            this.id = x.id;
            this.ime = x.ime;
            this.email = x.email;
        }
    }
}