using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Windows.Forms;
using Snipping_Tool.API.models;
using Snipping_Tool.Properties;

namespace Snipping_Tool.API.methods
{
    class ImgurImage : ImgurMethod
    {
        private Image imageFile ;
        public ImgurImage(Image screenBuffer)
        {
            imageFile = screenBuffer;
            var parameters = GetParameters();
            var imageWebRequest = ApiPostRequest("image", parameters);
            if (imageWebRequest == null)
            {
                MessageBox.Show(Resources.ErrorOccuredUploading);
                return;
            }
            var responseStream = imageWebRequest.GetResponseStream();
            if (responseStream != null)
            {
                var basicSerializer = new DataContractJsonSerializer(typeof(ImageDataModel));
                var basicSerialized = (ImageDataModel)basicSerializer.ReadObject(responseStream);
                Process.Start(basicSerialized.data.link);
            }
            else
            {
                MessageBox.Show(Resources.ErrorOccuredValidating);
            }
            
        }


        /// <summary>
        ///     Builds a string with the parameters we need to authorize the user.
        ///     The applications client id, and client secret, the grant type pin
        ///     and the pin the user got from authorizing our application on the website.
        /// </summary>
        /// <returns>The parameters to post into the web-request</returns>
        private string GetParameters()
        {
            imageFile.Save(@"tmp");
            var parameters = @"image=";
            const int MAX_URI_LENGTH = 32766;
            string base64img = Convert.ToBase64String(File.ReadAllBytes(@"tmp"));
            var sb = new StringBuilder();

            for (int i = 0; i < base64img.Length; i += MAX_URI_LENGTH)
            {
                sb.Append(Uri.EscapeDataString(base64img.Substring(i, Math.Min(MAX_URI_LENGTH, base64img.Length - i))));
            }

            return parameters + sb;

        }
    }
}
