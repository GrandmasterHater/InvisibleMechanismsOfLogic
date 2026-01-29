using Npgsql;
using NpgsqlTypes;

namespace InvisibleMechanismsOfLogic.Task1_SaveStringsToDB
{
    public class DatabaseStorage : Storage
    {
        private readonly string _connectionString;

        public DatabaseStorage(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public void Save(string data)
        {
            using NpgsqlConnection connection = new(_connectionString);
            connection.Open();

            using NpgsqlCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO dataStorage (textData) VALUES (@textData)";
            command.Parameters.AddWithValue("@textData", NpgsqlDbType.Text, data);
            command.ExecuteNonQuery();
        }

        public string Retrieve(int id)
        {
            using NpgsqlConnection connection = new(_connectionString);
            connection.Open();

            using NpgsqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT textData FROM dataStorage WHERE id = @id";
            command.Parameters.AddWithValue("@id", id);
            return command.ExecuteScalar() as string ?? string.Empty;
        }
    }
}