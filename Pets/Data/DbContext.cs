using Microsoft.Data.Sqlite;
using Pets.Models;

namespace Pets.Data
{
    //
    // This is a utility class to execute database CRUD function
    //
    public static class DbContext
    {
        // Get a pet by Id
        public static Pet GetPetById(int petId)
        {
            Pet pet = new Pet();

            //
            // Query the database to get the pet by id
            //

            // Create connection to database
            SqliteConnection connection = new SqliteConnection("Data Source= Data/database.db");

            // Open the connection
            connection.Open();

            // Run SQL to get a pet
            string sql = "SELECT PetId, Name, Age, PhotoFileName from Pet WHERE PetId = @PetId";

            // Create a "command" (sql to execute in database) to execute SQL
            SqliteCommand cmd = connection.CreateCommand();
            cmd.CommandText = sql;

            // Basically do a find and replace in the sql string
            cmd.Parameters.AddWithValue("@PetId", petId);

            // Execute the command (use a Data Reader so we can read() the rows returned by the command)
            SqliteDataReader reader = cmd.ExecuteReader();

            // Read through the result (if any)
            reader.Read();
            pet.PetId = reader.GetInt32(0);
            pet.Name = reader.GetString(1);
            pet.Age = reader.GetInt32(2);
            if (!reader.IsDBNull(3))
            {
                pet.PhotoFileName = reader.GetString(3);
            }
            
            // Close the connection
            connection.Close();

            return pet;
        }

        // Get all the pets in the table
        public static List<Pet> GetAllPets()
        {
            List<Pet> pets = new List<Pet> ();

            //
            // Query the database to get the pets
            //

            // Create connection to database
            SqliteConnection connection = new SqliteConnection("Data Source= Data/database.db");

            // Open the connection
            connection.Open();

            // Run SQL to get all the pets
            string sql = "SELECT PetId, Name, Age, PhotoFileName from Pet ORDER BY Name";

            // Create a "command" (sql to execute in database) to execute SQL
            SqliteCommand cmd = connection.CreateCommand ();
            cmd.CommandText = sql;

            // Execute the command (use a Data Reader so we can read() the rows returned by the command)
            SqliteDataReader reader = cmd.ExecuteReader ();

            // Read through the results
            while(reader.Read())
            {
                // For each database row, create a Pet object and add to the Pet list
                Pet pet = new Pet ();
                pet.PetId = reader.GetInt32(0);
                pet.Name = reader.GetString(1);
                pet.Age = reader.GetInt32(2);
                if (!reader.IsDBNull(3))
                {
                    pet.PhotoFileName = reader.GetString(3);
                }

                // Add this pet to list
                pets.Add(pet);
            }

            // Close the connection
            connection.Close();

            return pets;
        }

        // Add a new "pet" object to the table
        public static void AddNewPet(Pet pet)
        {
            // Create connection to database
            SqliteConnection connection = new SqliteConnection("Data Source= Data/database.db");

            // Open the connection
            connection.Open();

            // Run SQL to insert pet into pet table
            // Use command parameters to add the user's input to the SQL
            string sql = "INSERT INTO Pet(Name, Age, PhotoFileName) VALUES (@Name, @Age, @PhotoFileName)";
            
            SqliteCommand cmd = connection.CreateCommand();
            
            cmd.CommandText = sql;

            // Basically do a find and replace in the sql string
            cmd.Parameters.AddWithValue("@Name", pet.Name);
            cmd.Parameters.AddWithValue("@Age", pet.Age);
            cmd.Parameters.AddWithValue("@PhotoFileName", pet.PhotoFileName);

            // Execute the command
            cmd.ExecuteNonQuery();

            connection.Close();
        }

        // Remove "pet" from the table
        public static void RemovePet(int petId)
        {
            // Create connection to database
            SqliteConnection connection = new SqliteConnection("Data Source= Data/database.db");

            // Open the connection
            connection.Open();

            // Run SQL to remove pet from pet table
            // Use command parameters to add the user's input to the SQL
            string sql = "DELETE FROM Pet WHERE PetId = @PetId";

            SqliteCommand cmd = connection.CreateCommand();

            cmd.CommandText = sql;

            // Basically do a find and replace in the sql string
            cmd.Parameters.AddWithValue("@PetId", petId);

            // Execute the command
            cmd.ExecuteNonQuery();

            connection.Close();
        }
    }
}
