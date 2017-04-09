using rsc_harambe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace rsc_harambe.Controllers
{
    public class TransportController : ApiController
    {
        harambeDBEntities db = new harambeDBEntities();

        public List<ModelTransport> Get()
        {
            List<ModelTransport> lista = new List<ModelTransport>();
            List<transport> dbList = db.transport.ToList();

            foreach (transport x in dbList)
            {
                ModelTransport temp = new ModelTransport(x);
                lista.Add(temp);
            }

            return lista;
        }

        public List<ModelTransport> Get(int id)
        {
            List<ModelTransport> lista = new List<ModelTransport>();
            transport trans = null;
            trans = db.transport.Find(id);

            if(trans != null)
            {
                ModelTransport temp = new ModelTransport(trans);
                lista.Add(temp);
            }

            return lista;
        }
    }
}
