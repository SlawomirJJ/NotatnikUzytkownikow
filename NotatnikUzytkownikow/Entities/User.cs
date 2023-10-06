namespace NotatnikUzytkownikow.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; } = string.Empty;

        public virtual List<AdditionalAttribute>? AdditionalAttributes { get; set; }

    }
}
