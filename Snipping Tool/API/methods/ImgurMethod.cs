using System;
using System.IO;
using System.Net;
using System.Text;
using Snipping_Tool.Application.Helpers;
using Snipping_Tool.Properties;

namespace Snipping_Tool.API.methods
{
    class ImgurMethod
    {
        private const string _baseURL = "https://api.imgur.com/3/";

        /// <summary>
        /// </summary>
        /// <param name="method">The Imgur Api method that is being used</param>
        /// <remarks>Valid methods for the api are listed on the api documentation</remarks>
        /// <returns>The webresponse we got from the api, returning null if an error occured.</returns>
        protected static HttpWebResponse ApiPostRequest(string method, string parameters)
        {
            var bytes = Encoding.ASCII.GetBytes(parameters);
            var imgurWebRequest = (HttpWebRequest)WebRequest.Create(_baseURL + method);
            imgurWebRequest.KeepAlive = false;
            imgurWebRequest.ProtocolVersion = HttpVersion.Version10;
            imgurWebRequest.ContentLength = bytes.Length;
            imgurWebRequest.Headers.Add("Authorization", "Bearer " + Settings.Default.access_token);
            imgurWebRequest.ContentType = "application/x-www-form-urlencoded";
            imgurWebRequest.Method = "POST";
            Stream imgurRequestStream = imgurWebRequest.GetRequestStream();
            imgurRequestStream.Write(bytes, 0, bytes.Length);
            imgurRequestStream.Close();
            try
            {
                return (HttpWebResponse)imgurWebRequest.GetResponse();
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}
