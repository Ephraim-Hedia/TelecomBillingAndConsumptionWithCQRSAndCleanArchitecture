using System.Xml.Serialization;

namespace TelecomBillingAndConsumption.Api.Bases
{
    public static class SerializeToSoap
    {
        public static string Serialize<T>(T obj)
        {
            var serializer = new XmlSerializer(typeof(T));
            using (var stringWriter = new StringWriter())
            {
                serializer.Serialize(stringWriter, obj);
                return stringWriter.ToString();
            }
        }
    }
}
