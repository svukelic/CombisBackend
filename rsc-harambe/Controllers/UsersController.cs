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
    public class UsersController : ApiController
    {
        harambeDBEntities db = new harambeDBEntities();

        public async Task<List<ModelUser>> Post()
        {
            string result = await Request.Content.ReadAsStringAsync();

            List<ModelUser> lista = new List<ModelUser>();

            dynamic jobj = JsonConvert.DeserializeObject(result);
            string ime = jobj.ime;
            string email = jobj.email;

            users userInfo = new users();

            List<users> usersFilter = db.users.Where(x => x.email.Equals(email)).ToList();

            if (usersFilter.Count() > 0)
            {
                userInfo = usersFilter.First();
            }
            else
            {
                userInfo.ime = ime;
                userInfo.email = email;

                db.users.Add(userInfo);
                db.SaveChanges();
            }
            ModelUser finalUser = new ModelUser(userInfo);

            if(userInfo.plans.Count > 0) finalUser.current_hotel = userInfo.plans.Last().hotel_id.Value;

            lista.Add(finalUser);

            return lista;
        }

        public List<ModelPlans> Get(int id)
        {
            users userFilter = null;
            userFilter = db.users.Find(id);
            List<plans> dbList = new List<plans>();
            List<ModelPlans> lista = new List<ModelPlans>();

            if (userFilter != null)
            {
                dbList.AddRange(userFilter.plans);
                foreach (plans loc in dbList)
                {
                    ModelPlans ml = new ModelPlans(loc);

                    hotels h = null;
                    h = db.hotels.Find(ml.hotel_id);
                    if(h != null)
                    {
                        ml.hotel_name = h.hotel_name;
                    }

                    transport t = null;
                    t = db.transport.Find(ml.transport_id);
                    if (t != null)
                    {
                        locations l = null;
                        l = db.locations.Find(t.end_id);
                        if (l != null)
                        {
                            ml.destination = l.loc_name;
                        }
                    }

                    lista.Add(ml);
                }
            }

            return lista;
        }
    }
}
