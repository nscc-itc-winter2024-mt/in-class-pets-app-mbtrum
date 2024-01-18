using Microsoft.Data.Sqlite;
using Pets.Models;

namespace Pets.Data
{
    //
    // This is a utility class to execute database CRUD function
    //
    public static class DbContext
    {

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
            string sql = "SELECT PetId, Name, Age, PhotoFileName from Pet";

            // Create a "command" (sql to execute in database) to execute SQL
            SqliteCommand cmd = connection.CreateCommand ();
            cmd.CommandText = sql;

            // Execute the command (use a Data Reader so we can read() the rows returned by the command)
            SqliteDataReader reader = cmd.ExecuteReader ();

            // Read through the results
            while(reader.Read ())
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

    }
}
