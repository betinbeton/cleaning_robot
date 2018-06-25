using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace cleaning_robotApp_CallingApi.Class
{
    class WebCall
    {
        private string url;
        private string input;
        public WebCall(string url, string input)
        {
            this.url = url;
            this.input = input;
        }

        /// <summary>
        /// This method is used to call the rest api
        /// </summary>
        /// <param name="url">the url where the API es located</param>
        /// <param name="input">Json object like string</param>
        /// <returns>String with the output of the call</returns>
        public string callWebResApi()
        {
            string responseApi = "";

            try
            {
                var baseAddress = this.url;

                var http = (HttpWebRequest)WebRequest.Create(new Uri(baseAddress));
                http.Accept = "text/plain";
                http.ContentType = "text/plain";
                http.Method = "POST";

                string parsedContent = this.input;
                ASCIIEncoding encoding = new ASCIIEncoding();
                Byte[] bytes = encoding.GetBytes(parsedContent);

                Stream newStream = http.GetRequestStream();
                newStream.Write(bytes, 0, bytes.Length);
                newStream.Close();

                var response = http.GetResponse();

                var stream = response.GetResponseStream();
                var sr = new StreamReader(stream);
                var content = sr.ReadToEnd();
                responseApi = content;




            }
            catch (Exception e)
            {
                Console.WriteLine(" The call to the robot API failed {0}", e.ToString());
            }
            return responseApi;
        }



    }
}
