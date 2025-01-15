using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Data.SQLite;
namespace DomainModelEditor
{
    public class EntityStore : ObservableCollection<Entity>
    {
        private static int _uniqueId = 0;

        public void Load(IEnumerable<Tuple<int, string, int, int>> entities)
        {
            Clear();
            foreach (var e in entities)
            {
                Add(new Entity(e.Item1, e.Item2, e.Item3, e.Item4));
            }
        }
        public IEnumerable<Tuple<int, string, int, int>> GetEntitiesFromDatabase() //This is our new function which makes us able to read from entities.sqlite
        {
            // path of the entities.sqlite
            string databaseFileName = "C:\\Users\\doruk\\OneDrive\\Masaüstü\\assessment\\InterviewAssessment\\entities.sqlite";

            //we are checking if the file exists (not necessary since we just work with entities.sqlite but can be needed to both debug but also for real-life scenarios)
            if (!File.Exists(databaseFileName))
            {
                throw new FileNotFoundException($"Database file '{databaseFileName}' not found.");
            }
            //here I assumed same object type with the dummy variable will be what we come across within entities.sqlite, but also I checked this sqlite file with a browser sqlite reader and crosschecked that it includes same object structure id-name-x axis-y axis
            var entities = new List<Tuple<int, string, int, int>>();

            // Open a connection to the SQLite database
            using (var connection = new SQLiteConnection($"Data Source={databaseFileName};Version=3;"))
            {
                connection.Open();
                // Join the Entities and Coords tables based on the entity's Id
                string query = @"
                    SELECT e.Id, e.Name, c.X, c.Y
                    FROM Entities e
                    INNER JOIN Coords c ON e.Id = c.id
                ";

                using (var command = new SQLiteCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        // Read the data from the database and store it as tuples
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string name = reader.GetString(1);
                            int x = reader.GetInt32(2);
                            int y = reader.GetInt32(3);

                            // Add the tuple to the list
                            entities.Add(Tuple.Create(id, name, x, y));
                        }
                    }
                }
            }

            return entities;
        }
        public void Add(string name, int x, int y)
        {
            Add(new Entity(_uniqueId++, name, x, y));
        }
    }
}