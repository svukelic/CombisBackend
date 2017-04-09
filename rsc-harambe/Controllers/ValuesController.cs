using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using rsc_harambe.Models;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace rsc_harambe.Controllers
{
    public class ValuesController : ApiController
    {
        harambeDBEntities db = new harambeDBEntities();
        // GET api/values
        /*public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }*/
        public List<TestValue> Get()
        {
            List<TestValue> lista = db.TestValues.ToList();
            /*lista.Add("test1");
            lista.Add("ante je gej");
            lista.Add("sisa");*/

            return lista;
        }

        // GET api/values/5
        public List<TestValue> Get(int id)
        {
            List<TestValue> lista = new List<TestValue>();
            lista.Add(db.TestValues.Find(id));

            return lista;
        }

        // POST api/values
        /*public void Post([FromBody]string value)
        {

        }*/
        public async Task<List<TestValue>> Post()
        {
            string result = await Request.Content.ReadAsStringAsync();

            List<TestValue> newList = new List<TestValue>();

            dynamic jobj = JsonConvert.DeserializeObject(result);
            string action = jobj.ACTION;
            if (action.Equals("CREATE"))
            {
                foreach (dynamic jdata in jobj.DATA)
                {
                    TestValue @TestValue = new TestValue();

                    @TestValue.NAZIV = jdata.NAZIV;
                    @TestValue.VRIJEME = jdata.VRIJEME;

                    db.TestValues.Add(@TestValue);
                    db.SaveChanges();
                    newList.Add(@TestValue);
                }
            }

            return newList;
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public List<TestValue> Delete(int id)
        {
            List<TestValue> lista = new List<TestValue>();

            TestValue @TestValue = null;
            @TestValue = db.TestValues.Find(id);

            if(@TestValue != null)
            {
                lista.Add(@TestValue);
                db.TestValues.Remove(@TestValue);
                db.SaveChanges();
            }

            return lista;
        }
    }
}
