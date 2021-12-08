using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ProjetcLibrary.Utils
{
    public static class ApiHelper
    {
        public static string Get(string param, string controller)
        {
            HttpWebRequest WebReq = string.IsNullOrEmpty(param) ? (HttpWebRequest)WebRequest.Create($"https://localhost:44311/api/{controller}") : (HttpWebRequest)WebRequest.Create($"https://localhost:44311/api/{controller}/{param}");

            WebReq.Method = "GET";
            WebReq.Headers.Add("APIKey", "b+zcArCc+BPkJVljCq5PNg==");
            HttpWebResponse WebResp = (HttpWebResponse)WebReq.GetResponse();

            string jsonString;
            using (Stream stream = WebResp.GetResponseStream())
            {
                StreamReader reader = new StreamReader(stream, System.Text.Encoding.UTF8);
                jsonString = reader.ReadToEnd();
            }

            return jsonString;
        }

        public static void Put(string controller, int id, object model)
        {
            var url = $"https://localhost:44311/api/{controller}/{id}";

            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Headers.Add("APIKey", "b+zcArCc+BPkJVljCq5PNg==");
            httpRequest.Method = "PUT";
            httpRequest.ContentType = "application/json";
            var data = JsonConvert.SerializeObject(model);

            using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
            {
                streamWriter.Write(data);
            }

            httpRequest.GetResponse();
        }

        public static void Post(string controller, object model)
        {
            var url = $"https://localhost:44311/api/{controller}";

            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Method = "POST";
            httpRequest.Headers.Add("APIKey", "b+zcArCc+BPkJVljCq5PNg==");
            httpRequest.ContentType = "application/json";
            var data = JsonConvert.SerializeObject(model);

            using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
            {
                streamWriter.Write(data);
            }

            httpRequest.GetResponse();
        }

        public static void Delete(string controller, int id, object model)
        {
            var url = $"https://localhost:44311/api/{controller}/{id}";

            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Headers.Add("APIKey", "b+zcArCc+BPkJVljCq5PNg==");
            httpRequest.Method = "DELETE";
            httpRequest.ContentType = "application/json";
            var data = JsonConvert.SerializeObject(model);

            using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
            {
                streamWriter.Write(data);
            }

            httpRequest.GetResponse();
        }
    }
}
