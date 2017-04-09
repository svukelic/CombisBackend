using rsc_harambe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace rsc_harambe.Controllers
{
    public class TransportLocationController : ApiController
    {
        public List<ModelTransport> Get(int id)
        {
            harambeDBEntities db = new harambeDBEntities();

            List<ModelTransport> lista = new List<ModelTransport>();
            List<transport> dbList = db.transport.Where(x => x.end_id == id).ToList();

            foreach (transport x in dbList)
            {
                ModelTransport temp = new ModelTransport(x);
                lista.Add(temp);
            }

            return lista;
        }
    }
}
