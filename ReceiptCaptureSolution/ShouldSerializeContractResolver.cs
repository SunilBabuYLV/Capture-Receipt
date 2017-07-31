using System;
using System.Reflection;
using Google.Cloud.Vision.V1;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ReceiptCapture
{
    /// <summary>
    /// To include or ignore , Image.Content property in serializtion
    /// </summary>
    public class ShouldSerializeContractResolver : CamelCasePropertyNamesContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member,
            MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);
            if (property.DeclaringType == typeof(Image) &&
                property.PropertyName.Equals("Content", StringComparison.InvariantCultureIgnoreCase))
                property.ShouldSerialize = instance =>
                {
                    var i = (Image) instance;
                    return !i.Content.IsEmpty;
                };
            return property;
        }
    }
}