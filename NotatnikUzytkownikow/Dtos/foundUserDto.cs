using NotatnikUzytkownikow.Requests;
using System.ComponentModel.DataAnnotations;

namespace NotatnikUzytkownikow.Dtos
{
    public class FoundUserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly BirthDate { get; set; }
        public string Gender { get; set; }

        public List<AdditionalAttributeRequest>? AdditionalAttributes { get; set; }
    }
}
