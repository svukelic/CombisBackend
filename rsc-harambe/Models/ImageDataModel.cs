using Google.Cloud.Vision.V1;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace rsc_harambe.Models
{
    public class ImageDataModel
    {
        public int success = 0;
        public string response = "";
        public string short_text = "";
        public string long_text = "";

        public void LoadImage(string code, string path)
        {
            byte[] bytes = Convert.FromBase64String(code);

            System.Drawing.Image image = null;
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                image = System.Drawing.Image.FromStream(ms);
            }

            image.Save(path , System.Drawing.Imaging.ImageFormat.Png);
        }

        public string RecognizeImage(string code)
        {
            string rezultat = "";

            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://vision.googleapis.com/v1/images:annotate?key=AIzaSyBsCg-qgggEmB4jzL0Ctf-yzwpMiA4MXJE");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = "{" +
                                "\"requests\": [" +
                                    "{" +
                                    "\"image\": {" +
                                    "\"content\": \"" + code + "\"" +
                                    "}," +
                                    "\"features\": [" +
                                    "{" +
                                    "\"type\": \"LANDMARK_DETECTION\"" +
                                    "}" +
                                    "]" +
                                    "}" +
                                    "]" +
                                    "}";

                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            dynamic jobj;
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                jobj = JsonConvert.DeserializeObject(result);
            }

            foreach (dynamic j1 in jobj.responses)
            {
                foreach (dynamic j2 in j1.landmarkAnnotations)
                {
                    rezultat = j2.description;
                }
            }

            return rezultat;
        }

        public ImageDataModel CheckWiki(string check)
        {
            ImageDataModel idm = new ImageDataModel();
            idm.response = check;

            if (check.Contains("Eiffel"))
            {
                idm.short_text = "The Eiffel Tower is a wrought iron lattice tower on the Champ de Mars in Paris, France.";
                idm.long_text = "It is named after the engineer Gustave Eiffel, whose company designed and built the tower. Constructed from 1887–89 as the entrance to the 1889 World's Fair, it was initially criticized by some of France's leading artists and intellectuals for its design, but it has become a global cultural icon of France and one of the most recognisable structures in the world.";
            }
            else if (check.Contains("Liberty"))
            {
                idm.short_text = "The Statue of Liberty is a colossal neoclassical sculpture on Liberty Island in New York Harbor in New York City, in the United States.";
                idm.long_text = "he Statue of Liberty is a robed female figure representing Libertas, the Roman goddess. She holds a torch above her head, and in her left arm carries a tabula ansata inscribed \"July 4, 1776\", the date of the American Declaration of Independence. A broken chain lies at her feet. The statue became an icon of freedom and of the United States, and was a welcoming sight to immigrants arriving from abroad.";
            }
            else if (check.Contains("Castle"))
            {
                idm.short_text = "Trakošćan Castle is a castle located in northern Croatia that dates back to the 13th century. It is open to visitors, but in need for repairs.";
                idm.long_text = "Trakošćan was built in the 13th century within Croatia's northwestern fortification system, as a rather small observation fortress for monitoring the road from Ptuj to Bednja Valley. According to a legend, Trakošćan was named after another fortification(arx Thacorum) that was alleged to have stood in the same spot back in antiquity.Another source claims that it was named after the knights of Drachenstein who were in control of the region in early Middle Ages.";
            }

            return idm;
        }

        public void ImageStatistics(string naziv, int hotel_id)
        {
            statistics_images sf = new statistics_images();
            harambeDBEntities db = new harambeDBEntities();
            List<statistics_images> sfCheck = db.statistics_images.Where(x => x.naziv.Equals(naziv) && x.hotel_id == hotel_id).ToList();
            if (sfCheck.Count() > 0)
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
                sf.hotel_id = hotel_id;
                sf.naziv = naziv;
                sf.cnt = 1;

                hotels h = null;
                h = db.hotels.Find(hotel_id);
                sf.hotel_name = h.hotel_name;

                db.statistics_images.Add(sf);
            }

            db.SaveChanges();
        }
    }
}