using System.Runtime.Serialization;

namespace FavoriteFeats_Kingmaker.Serializers
{
    [DataContract]
    internal class Feature
    {
        [DataMember]
        public string Name { get; set; }
    }
}
