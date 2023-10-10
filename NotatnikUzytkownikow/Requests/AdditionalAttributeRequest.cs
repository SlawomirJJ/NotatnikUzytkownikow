using System.ComponentModel.DataAnnotations;

namespace NotatnikUzytkownikow.Requests
{
    public class AdditionalAttributeRequest
    {
        public string AttributeName { get; set; }
        public string Value { get; set; }
    }
}
