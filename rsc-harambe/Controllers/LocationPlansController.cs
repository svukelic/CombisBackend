using rsc_harambe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace rsc_harambe.Controllers
{
    public class LocationPlansController : ApiController
    {
        harambeDBEntities db = new harambeDBEntities();

        public List<ModelPlans> Get(int id)
        {
            List<ModelPlans> lista = new List<ModelPlans>();

            List<hotels> listaH = db.hotels.Where(x => x.loc_id == id).ToList();
            List<plans> listaP = new List<plans>();

            foreach (hotels h in listaH)
            {
                listaP.AddRange(db.plans.Where(x => x.hotel_id == h.id).ToList());
            }

            foreach (plans x in listaP)
            {
                ModelPlans temp = new ModelPlans(x);
                lista.Add(temp);
            }

            return lista;
        }
    }
}
