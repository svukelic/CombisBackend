using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rsc_harambe.Models
{
    public class ModelTransport
    {
        public int id { get; set; }
        public string transport_type { get; set; }
        public int start_id { get; set; }
        public int end_id { get; set; }
        public int duration { get; set; }

        public ModelTransport(transport x)
        {
            this.id = x.id;
            this.transport_type = x.transport_type;
            this.start_id = x.start_id.Value;
            this.end_id = x.end_id.Value;
            this.duration = x.duration.Value;
        }
    }
}