using System.Runtime.Serialization;

namespace Snipping_Tool.API.models
{
    [DataContract]
    public class BasicDataModel
    {
        [DataMember]
        public object data { get; set; }
        [DataMember]
        public bool success { get; set; }
        [DataMember]
        public int status { get; set; }
    }
}