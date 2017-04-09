using Newtonsoft.Json;
using rsc_harambe.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace rsc_harambe.Controllers
{
    public class ImagesController : ApiController
    {

        public async Task<ImageDataModel> Post()
        {
            string result = await Request.Content.ReadAsStringAsync();

            ImageDataModel idm = new ImageDataModel();

            dynamic jobj = JsonConvert.DeserializeObject(result);

            string imgCode = jobj.code;
            int hid = jobj.hid;

            idm.response = idm.RecognizeImage(imgCode);
            //idm = idm.CheckWiki(idm.response);

            idm.ImageStatistics(idm.response, hid);

            return idm;
        }
    }
}
