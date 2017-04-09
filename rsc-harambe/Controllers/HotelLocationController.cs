using rsc_harambe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace rsc_harambe.Controllers
{
    public class HotelLocationController : ApiController
    {
        harambeDBEntities db = new harambeDBEntities();

        public List<ModelHotels> Get(int id)
        {
            List<ModelHotels> lista = new List<ModelHotels>();
            List<hotels> dbList = db.hotels.Where(x => x.loc_id == id).ToList();

            foreach(hotels x in dbList)
            {
                ModelHotels temp = new ModelHotels(x);
                lista.Add(temp);
            }

            return lista;
        }
    }
}
