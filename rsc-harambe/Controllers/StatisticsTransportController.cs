using rsc_harambe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace rsc_harambe.Controllers
{
    public class StatisticsTransportController : ApiController
    {
        harambeDBEntities db = new harambeDBEntities();

        public List<statistics_transport> Get()
        {
            List<statistics_transport> lista = db.statistics_transport.ToList();

            return lista;
        }

        public List<statistics_transport> Get(int id)
        {
            List<statistics_transport> lista = db.statistics_transport.Where(x => x.hotel_id == id).ToList();

            return lista;
        }
    }
}
