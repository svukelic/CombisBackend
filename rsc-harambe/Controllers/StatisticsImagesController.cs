using rsc_harambe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace rsc_harambe.Controllers
{
    public class StatisticsImagesController : ApiController
    {
        harambeDBEntities db = new harambeDBEntities();

        public List<statistics_images> Get()
        {
            List<statistics_images> lista = db.statistics_images.ToList();

            return lista;
        }

        public List<statistics_images> Get(int id)
        {
            List<statistics_images> lista = db.statistics_images.Where(x => x.hotel_id == id).ToList();

            return lista;
        }
    }
}
