using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Windows.Forms;
using Snipping_Tool.API.models;
using Snipping_Tool.API.resources;
using Snipping_Tool.Properties;


namespace Snipping_Tool.API
{
    public class ImgurApi
    {
        private readonly string _clientId;
        private readonly string _clientSecret;
        private ImgurAuthorize _authorizeWindow;
        private bool _loggedIn;

        /// <summary>
        ///     The ImgurApi we're going to be using for everything in the snipping tool
        /// </summary>
        /// <param name="clientId">Registered application client id from Imgur</param>
        /// <param name="clientSecret">Registered application client secret from Imgur</param>
        public ImgurApi(string clientId, string clientSecret)
        {
            _clientId = clientId;
            _clientSecret = clientSecret;
            IsLoggedIn();
        }

        /// <summary>
        ///     Returns the client id the api was set up with
        /// </summary>
        public string ClientId
        {
            get { return _clientId; }
        }

        /// <summary>
        ///     Returns the client secret the api was set up with
        /// </summary>
        public string ClientSecret
        {
            get { return _clientSecret; }
        }

        /// <summary>
        //      If the user's refresh_token is not valid, or if they have not logged in before
        ///     will prompt the user to login to the application by presenting them a new
        ///     authorize window.
        /// </summary>
        private void LaunchLogin()
        {
            var authorizeUrl = "https://api.imgur.com/oauth2/authorize?client_id=" + ClientId + "&response_type=pin";
            Process.Start(authorizeUrl);
            _authorizeWindow = new ImgurAuthorize(this);
            _authorizeWindow.ShowDialog();
        }

        /// <summary>
        ///     Checks if the user's refresh_token is valid, and if they are logged in.
        /// </summary>
        /// <returns>If the user is logged in</returns>
        public bool IsLoggedIn()
        {
            if (Properties.Settings.Default.account_username.Length > 0)
            {
                _loggedIn = true;
            }
            else
            {
                LaunchLogin();
            }
            return _loggedIn;
        }

        public void RefreshToken()
        {
            var param = "refresh_token=" + Settings.Default.refresh_token;
            param += "&client_id=" + ClientId + "&client_secret=" + ClientSecret;
            param += "&grant_type=refresh_token";
            var refreshRequest = ImgurAuthorize.ApiOAuthWebRequest("token", param);
            if (refreshRequest == null)
            {
                MessageBox.Show(Resources.ErrorOccuredRefreshing);
                return;
            }
            var responseStream = refreshRequest.GetResponseStream();
            if (responseStream != null)
            {
                var jsonSerializer = new DataContractJsonSerializer(typeof(ImgurAuthorizationResponse));
                var readObject = (ImgurAuthorizationResponse)jsonSerializer.ReadObject(responseStream);
                SetAuthorizationResponse(readObject);
                
            }
            else
            {
                MessageBox.Show(Resources.ErrorOccuredValidating);
            }
        }
        /// <summary>
        ///     Checks if the user has logged in before/is logged in and gets their username.
        /// </summary>
        /// <returns>The username of the user, or Not logged in if they're not logged in</returns>
        public string GetUsername()
        {
            if (IsLoggedIn())
            {
                return "Welcome back, " + Settings.Default.account_username;
            }
            return "Not logged in.";
        }

        /// <summary>
        ///     Sets the settings for the application, using the ImgurAuthorize response.
        /// </summary>
        /// <param name="serializedAuthorizationResponse">The ImgurAuthorizationResponse object to save</param>
        /// <see cref="ImgurAuthorizationResponse" />
        public void SetAuthorizationResponse(ImgurAuthorizationResponse serializedAuthorizationResponse)
        {
            Settings.Default.access_token = serializedAuthorizationResponse.access_token;
            Settings.Default.refresh_token = serializedAuthorizationResponse.refresh_token;
            Settings.Default.account_username = serializedAuthorizationResponse.account_username;
            Settings.Default.Save();
            _loggedIn = true;
        }
    }
}