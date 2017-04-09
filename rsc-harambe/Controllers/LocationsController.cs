using rsc_harambe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace rsc_harambe.Controllers
{
    public class LocationsController : ApiController
    {
        harambeDBEntities db = new harambeDBEntities();
        
        public List<ModelLocation> Get()
        {
            List<ModelLocation> lista = new List<ModelLocation>();
            List<locations> dbList = db.locations.ToList();

            foreach(locations loc in dbList)
            {
                ModelLocation ml = new ModelLocation(loc);
                lista.Add(ml);
            }

            return lista;
        }

        public List<ModelLocation> Get(string naziv)
        {
            List<ModelLocation> lista = new List<ModelLocation>();
            List<locations> dbList = db.locations.Where(x => x.loc_name.Equals(naziv)).ToList();

            foreach (locations loc in dbList)
            {
                ModelLocation ml = new ModelLocation(loc);
                lista.Add(ml);
            }

            return lista;
        }

        public List<ModelLocation> Get(int id)
        {
            List<ModelLocation> lista = new List<ModelLocation>();
            locations loc = null;
            loc = db.locations.Find(id);

            if (loc != null)
            {
                ModelLocation ml = new ModelLocation(loc);
                lista.Add(ml);
            }

            return lista;
        }
    }
}
