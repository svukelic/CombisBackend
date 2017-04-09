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
    public class StatisticsFoodController : ApiController
    {
        harambeDBEntities db = new harambeDBEntities();

        public List<statistics_food> Get()
        {
            List<statistics_food> lista = db.statistics_food.ToList();

            return lista;
        }

        public List<statistics_food> Get(int id)
        {
            List<statistics_food> lista = db.statistics_food.Where(x => x.hotel_id == id).ToList();

            return lista;
        }

        public async Task<statistics_food> Post()
        {
            string result = await Request.Content.ReadAsStringAsync();

            List<ModelActivities> newList = new List<ModelActivities>();

            dynamic jobj = JsonConvert.DeserializeObject(result);
            int users_id = jobj.users_id;
            int hotel_id = jobj.hotel_id;
            string naziv = jobj.naziv;

            statistics_food sf = new statistics_food();

            List<statistics_food> sfCheck = db.statistics_food.Where(x => x.naziv.Equals(naziv) && x.hotel_id == hotel_id).ToList();
            if(sfCheck.Count() > 0)
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
                sf.users_id = users_id;
                sf.hotel_id = hotel_id;
                sf.naziv = naziv;
                sf.cnt = 1;

                hotels h = null;
                h = db.hotels.Find(hotel_id);
                sf.hotel_name = h.hotel_name;

                db.statistics_food.Add(sf);
            }

            db.SaveChanges();

            return sf;
        }
    }
}
