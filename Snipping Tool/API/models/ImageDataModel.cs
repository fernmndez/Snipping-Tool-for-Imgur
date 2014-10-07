using System.Runtime.Serialization;

namespace Snipping_Tool.API.models
{
    [DataContract]
    public class ImageDataModel
    {

        [DataMember]
        public ImageData data { get; set; }

        [DataMember]
        public bool success { get; set; }
        [DataMember]
        public int status { get; set; }
    }

    [DataContract]
    public class ImageData
    {

        [DataMember]
        public string title { get; set; }

        [DataMember]
        public string description { get; set; }

        [DataMember]
        public int datetime { get; set; }

        [DataMember]
        public string type { get; set; }

        [DataMember]
        public bool animated { get; set; }

        [DataMember]
        public int width { get; set; }

        [DataMember]
        public int height { get; set; }

        [DataMember]
        public int views { get; set; }

        [DataMember]
        public int bandwidth { get; set; }
        [DataMember]
        public string deletehash { get; set; }
        [DataMember]
        public string section { get; set; }
        [DataMember]
        public string link { get; set; }
    }
}