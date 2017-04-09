using rsc_harambe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace rsc_harambe.Controllers
{
    public class StatisticsActivitiesController : ApiController
    {
        harambeDBEntities db = new harambeDBEntities();

        public List<statistics_activities> Get()
        {
            List<statistics_activities> lista = db.statistics_activities.ToList();

            return lista;
        }

        public List<statistics_activities> Get(int id)
        {
            List<statistics_activities> lista = db.statistics_activities.Where(x => x.hotel_id == id).ToList();

            return lista;
        }
    }
}
