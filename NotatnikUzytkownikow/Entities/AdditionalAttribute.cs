namespace NotatnikUzytkownikow.Entities
{
    public class AdditionalAttribute
    {
        public Guid Id { get; set; }
        public string AttributeName { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;

        public Guid UserId { get; set; }
        public virtual User User { get; set; }

    }
}
