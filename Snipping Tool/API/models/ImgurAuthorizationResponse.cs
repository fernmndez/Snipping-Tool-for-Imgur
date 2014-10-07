using System.Runtime.Serialization;

namespace Snipping_Tool.API.models
{
    [DataContract]
    public class ImgurAuthorizationResponse
    {
        [DataMember]
        public string access_token { get; set; }
        [DataMember]
        public int expires_in { get; set; }
        [DataMember]
        public string token_type { get; set; }
        [DataMember]
        public string refresh_token { get; set; }
        [DataMember]
        public string account_username { get; set; }

    }
}
