using Newtonsoft.Json;
using rsc_harambe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace rsc_harambe.Controllers
{
    public class ActivitiesController : ApiController
    {
        harambeDBEntities db = new harambeDBEntities();

        public List<ModelActivities> Get()
        {
            List<ModelActivities> lista = new List<ModelActivities>();
            List<activities> dbList = db.activities.ToList();

            foreach(activities x in dbList)
            {
                ModelActivities temp = new ModelActivities(x);
                lista.Add(temp);
            }

            return lista;
        }

        public List<ModelActivities> Get(int id)
        {
            hotels hotelFilter = null;
            hotelFilter = db.hotels.Find(id);
            List<ModelActivities> lista = new List<ModelActivities>();
            List<activities> dbList = new List<activities>();

            if(hotelFilter != null)
            {
                dbList.AddRange(hotelFilter.activities);
                foreach (activities x in dbList)
                {
                    ModelActivities temp = new ModelActivities(x);
                    lista.Add(temp);
                }
            }

            return lista;
        }
    }
}
