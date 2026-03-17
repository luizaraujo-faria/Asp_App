using Asp_App.Repositories.Contracts;
using Asp_App.Models;
using Asp_App.DTOs;
using MySql.Data.MySqlClient;

namespace Asp_App.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _mySqlConnection;
        public UserRepository(IConfiguration config)
        {
            _mySqlConnection = config.GetConnectionString("MySqlConnection");
        }

        public IEnumerable<User> GetAll()
        {
            List<User> users = new List<User>();

            using var conn = new MySqlConnection(_mySqlConnection);
            conn.Open();

            string query = "SELECT * FROM tbUser";

            using var cmd = new MySqlCommand(query, conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                users.Add(new User
                {
                    UserId = reader.GetInt32("userId"),
                    UserName = reader.GetString("userName"),
                    Email = reader.GetString("email"),
                    UserPassword = reader.GetString("userPassword"),
                    Cpf = reader.GetString("cpf"),
                    BirthDate = DateOnly.FromDateTime(reader.GetDateTime("birthDate"))
                });
            }

            return users;
        }

        public User GetUserById(int id)
        {
            using var conn = new MySqlConnection(_mySqlConnection);
            conn.Open();

            string query = "SELECT * FROM tbUser WHERE userId = @id";

            using var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", id);

            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new User
                {
                    UserId = reader.GetInt32("userId"),
                    UserName = reader.GetString("userName"),
                    Email = reader.GetString("email"),
                    UserPassword = reader.GetString("userPassword"),
                    Cpf = reader.GetString("cpf"),
                    BirthDate = DateOnly.FromDateTime(reader.GetDateTime("birthDate"))
                };
            }

            return null;
        }

        public void SignUp(CreateUserDTO newUser)
        {
            using var conn = new MySqlConnection(_mySqlConnection);
            conn.Open();

            using var cmd = new MySqlCommand("spCreateUser", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@vUserName", newUser.UserName);
            cmd.Parameters.AddWithValue("@vEmail", newUser.Email);
            cmd.Parameters.AddWithValue("@vUserP", newUser.UserPassword);
            cmd.Parameters.AddWithValue("@vCpf", newUser.Cpf);
            cmd.Parameters.AddWithValue("@vBirthDate", newUser.BirthDate);

            cmd.ExecuteNonQuery();
        }

        public void SignIn(User newUser)
        {
            using var conn = new MySqlConnection(_mySqlConnection);
            conn.Open();

            string query = @"SELECT * FROM tbUser 
                             WHERE email = @email AND userPassword = @password";

            using var cmd = new MySqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@email", newUser.Email);
            cmd.Parameters.AddWithValue("@password", newUser.UserPassword);

            using var reader = cmd.ExecuteReader();

            if (!reader.Read())
            {
                throw new Exception("Email ou senha inválidos");
            }
        }

        public void UpdateUser(User user)
        {
            using var conn = new MySqlConnection(_mySqlConnection);
            conn.Open();

            string query = @"UPDATE tbUser
                                SET 
                                    userName = COALESCE(@name, userName),
                                    email = COALESCE(@email, email),
                                    userPassword = COALESCE(@password, userPassword),
                                    cpf = COALESCE(@cpf, cpf),
                                    birthDate = COALESCE(@birthDate, birthDate)
                                WHERE userId = @id";

            using var cmd = new MySqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@id", user.UserId);
            cmd.Parameters.AddWithValue("@name", user.UserName ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@email", user.Email ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@password", user.UserPassword ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@cpf", user.Cpf ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@birthDate", user.BirthDate == default ? DBNull.Value : user.BirthDate);

            cmd.ExecuteNonQuery();
        }

        public void DeleteUser(int id)
        {
            using var conn = new MySqlConnection(_mySqlConnection);
            conn.Open();

            string query = "DELETE FROM tbUser WHERE userId = @id";

            using var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();
        }
    }
}