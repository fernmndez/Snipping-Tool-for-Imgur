using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Windows.Forms;
using Snipping_Tool.API.models;
using Snipping_Tool.Properties;

namespace Snipping_Tool.API.resources
{
    public partial class ImgurAuthorize : Form
    {
        private readonly ImgurApi _imgurApi;

        /// <summary>
        ///     Sets up an instance of our ImgurAuthorize object that will be used
        ///     to get a pin from the user, and then verify it.
        /// </summary>
        /// <param name="imgurApi">A reference to the ImgurApi Object</param>
        public ImgurAuthorize(ImgurApi imgurApi)
        {
            _imgurApi = imgurApi;
            InitializeComponent();
        }

        /// <summary>
        /// </summary>
        /// <param name="method">The OAuth2 parameter that is being used</param>
        /// <remarks>Valid methods for OAuth2 are authorize, and token</remarks>
        /// <param name="parameters">The post parameters that are going to be encoded, posted to the OAuth api.</param>
        /// <returns>The webresponse we got from the OAuth api, returning null if an error occured.</returns>
        public static HttpWebResponse ApiOAuthWebRequest(string method, string parameters)
        {
            var bytes = Encoding.ASCII.GetBytes(parameters);
            var imgurWebRequest = (HttpWebRequest) WebRequest.Create("https://api.imgur.com/oauth2/" + method);
            imgurWebRequest.KeepAlive = false;
            imgurWebRequest.ProtocolVersion = HttpVersion.Version10;
            imgurWebRequest.ContentLength = bytes.Length;
            imgurWebRequest.ContentType = "application/x-www-form-urlencoded";
            imgurWebRequest.Method = "POST";
            Stream imgurRequestStream = imgurWebRequest.GetRequestStream();
            imgurRequestStream.Write(bytes, 0, bytes.Length);
            imgurRequestStream.Close();
            try
            {
                return (HttpWebResponse) imgurWebRequest.GetResponse();
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        ///     Verify button handler, calls the VerifyPin()
        /// </summary>
        private void buttonVerify_Click(object sender, EventArgs e)
        {
            VerifyPin();
        }

        /// <summary>
        ///     Tries to verify the validity of the pin the user inputs.
        /// </summary>
        private void VerifyPin()
        {
            var parameters = GetParameters();
            var authWebRequest = ApiOAuthWebRequest("token", parameters);
            if (authWebRequest == null)
            {
                MessageBox.Show(Resources.ErrorOccuredValidating);
                return;
            }
            var responseStream = authWebRequest.GetResponseStream();
            if (responseStream != null)
            {
                var pinSerializer = new DataContractJsonSerializer(typeof(ImgurAuthorizationResponse));
                var pinSerialized = (ImgurAuthorizationResponse)pinSerializer.ReadObject(responseStream);
                _imgurApi.SetAuthorizationResponse(pinSerialized);
                Close();
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
        /// <exception cref="ApplicationException">Thrown when pin is empty.</exception>
        private string GetParameters()
        {
            var pinText = pinTextBox.Text;
            if (pinText.Length == 0)
            {
                throw new ApplicationException("Empty pin");
            }
            var parameters = "client_id=" + _imgurApi.ClientId + "&";
            parameters += "client_secret=" + _imgurApi.ClientSecret + "&";
            parameters += "grant_type=pin" + "&";
            parameters += "pin=" + pinText;

            return parameters;
        }

        private void ImgurAuthorize_Activated(object sender, EventArgs e)
        {
            pinTextBox.Focus();
        }


        private void pinTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonVerify_Click(sender, new EventArgs());
            }
        }
    }
}