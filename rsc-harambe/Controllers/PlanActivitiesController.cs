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
    public class PlanActivitiesController : ApiController
    {
        harambeDBEntities db = new harambeDBEntities();

        public List<ModelActivities> Get(int id)
        {
            plans planFilter = null;
            planFilter = db.plans.Find(id);
            List<activities> dbList = new List<activities>();
            List<ModelActivities> lista = new List<ModelActivities>();

            if (planFilter != null)
            {
                dbList.AddRange(planFilter.activities);
                foreach (activities x in dbList)
                {
                    ModelActivities temp = new ModelActivities(x);
                    lista.Add(temp);
                }
            }

            return lista;
        }

        public async Task<List<ModelActivities>> Post()
        {
            string result = await Request.Content.ReadAsStringAsync();

            List<ModelActivities> newList = new List<ModelActivities>();

            dynamic jobj = JsonConvert.DeserializeObject(result);
            int pid = jobj.pid;
            int aid = jobj.aid;

            plans newPlan = null;
            activities a = null;
            newPlan = db.plans.Find(pid);
            a = db.activities.Find(aid);

            if (newPlan != null && a != null)
            {
                newPlan.activities.Add(a);
                db.SaveChanges();
            }

            ModelActivities temp = new ModelActivities(a);
            newList.Add(temp);

            StatisticsActivities(a.activity_name, newPlan.hotel_id.Value);

            return newList;
        }

        private void StatisticsActivities(string naziv, int hotel_id)
        {
            statistics_activities sf = new statistics_activities();

            List<statistics_activities> sfCheck = db.statistics_activities.Where(x => x.naziv.Equals(naziv) && x.hotel_id == hotel_id).ToList();
            if (sfCheck.Count() > 0)
            {
                sf = sfCheck.First();
                if (sf.cnt != null)
                {
                    sf.cnt = sf.cnt + 1;
                }
                else
                {
                    sf.cnt = 1;
                }
            }
            else
            {
                sf.hotel_id = hotel_id;
                sf.naziv = naziv;
                sf.cnt = 1;

                hotels h = null;
                h = db.hotels.Find(hotel_id);
                sf.hotel_name = h.hotel_name;

                db.statistics_activities.Add(sf);
            }

            db.SaveChanges();
        }
    }
}
