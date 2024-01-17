namespace Pets.Models
{
    //
    // This is the Pet model, meant to store data in the Pets table in my database.
    //
    public class Pet
    {
        public int PetId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public string PhotoFileName { get; set; } = string.Empty;
    }
}
