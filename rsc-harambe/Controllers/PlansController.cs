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
    public class PlansController : ApiController
    {
        harambeDBEntities db = new harambeDBEntities();

        public List<ModelPlans> Get()
        {
            List<ModelPlans> lista = new List<ModelPlans>();
            List<plans> dbList = db.plans.ToList();

            foreach (plans loc in dbList)
            {
                ModelPlans temp = new ModelPlans(loc);
                transport t = null;
                t = db.transport.Find(loc.transport_id);
                if (t != null)
                {
                    locations l = null;
                    l = db.locations.Find(t.end_id);
                    if (l != null)
                    {
                        temp.destination = l.loc_name;
                    }
                }
                hotels h = null;
                h = db.hotels.Find(loc.hotel_id);
                if (h != null)
                {
                    temp.hotel_name = h.hotel_name;
                }
                lista.Add(temp);
            }

            return lista;
        }

        public List<ModelPlans> Get(int id)
        {
            plans plan = null;
            plan = db.plans.Find(id);
            List<ModelPlans> lista = new List<ModelPlans>();

            if (plan != null)
            {
                ModelPlans temp = new ModelPlans(plan);
                transport t = null;
                t = db.transport.Find(plan.transport_id);
                if (t != null)
                {
                    locations l = null;
                    l = db.locations.Find(t.end_id);
                    if (l != null)
                    {
                        temp.destination = l.loc_name;
                    }
                }
                hotels h = null;
                h = db.hotels.Find(plan.hotel_id);
                if (h != null)
                {
                    temp.hotel_name = h.hotel_name;
                }
                lista.Add(temp);
            }

            return lista;
        }

        public async Task<List<ModelPlans>> Post()
        {
            string result = await Request.Content.ReadAsStringAsync();

            List<ModelPlans> newList = new List<ModelPlans>();

            dynamic jobj = JsonConvert.DeserializeObject(result);

            plans newPlan = new plans();
            newPlan.plan_naziv = jobj.plan_name;
            newPlan.transport_id = jobj.transport_id;
            newPlan.hotel_id = jobj.hotel_id;

            db.plans.Add(newPlan);

            int userId = jobj.user_id;
            users user = null;
            user = db.users.Find(userId);
            if(user != null)
            {
                user.plans.Add(newPlan);
            }

            db.SaveChanges();

            ModelPlans temp = new ModelPlans(newPlan);
            transport t = null;
            t = db.transport.Find(newPlan.transport_id);
            if (t != null)
            {
                locations l = null;
                l = db.locations.Find(t.end_id);
                if(l != null)
                {
                    temp.destination = l.loc_name;
                }
            }
            hotels h = null;
            h = db.hotels.Find(newPlan.hotel_id);
            if (h != null)
            {
                temp.hotel_name = h.hotel_name;
            }
            newList.Add(temp);

            //StatisticsPrijevozi(newPlan.hotel_id.Value, db.transport.Find(newPlan.transport).start_id.Value);

            return newList;
        }

        private void StatisticsPrijevozi(int hotel_id, int source_id)
        {
            statistics_transport sf = new statistics_transport();

            List<statistics_transport> sfCheck = db.statistics_transport.Where(x => x.source_id.Equals(source_id) && x.hotel_id == hotel_id).ToList();
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
                sf.source_id = source_id;
                sf.cnt = 1;

                hotels h = null;
                h = db.hotels.Find(hotel_id);
                sf.hotel_name = h.hotel_name;

                db.statistics_transport.Add(sf);
            }

            db.SaveChanges();
        }
    }
}
