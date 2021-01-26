using ConsoleWeb.Database;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;//z pętli albo z tego korzystaj - to te Any() Where()

public class UserRepository
{
    //private readonly static List<UserModel> Users = new List<UserModel>();

    public UserModel AddUser(UserModel input)
    {
        using (var dbContext = new DbContext())
        {

            string sql = $"INSERT INTO users (Username) VALUES (@Username)";
            dbContext.Connection.Open();

            var user = new UserModel()
            {
                Id = input.Id,
                Username = input.Username
            };
            using (SqlCommand command = new SqlCommand(sql, dbContext.Connection))
            {
                command.Parameters.AddWithValue("@Username", input.Username);
                command.ExecuteNonQuery();
            }
            dbContext.Connection.Close();

            return user;
        }
    }

    public UserModel DeleteUser(int id)
    {
       using(var dbContext = new DbContext())
        {
            string sql = "DELETE FROM users WHERE id = @id";
            dbContext.Connection.Open();
            using(SqlCommand command = new SqlCommand(sql, dbContext.Connection))
            {
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
            }
            dbContext.Connection.Close();
        }
        return GetUser(id);
    }


    public UserModel PatchUser(int id)
    {
        using(var dbContext = new DbContext())
        {
            dbContext.Connection.Open();
            string sql = "UPDATE users SET Username = 'DEAD' WHERE id = @id"; 
            using (SqlCommand command = new SqlCommand(sql, dbContext.Connection))
            {
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
            }
            dbContext.Connection.Close();           
            
        }    
        return GetUser(id);
    }
      

    public UserModel GetUser(int id)
    {
        using (var dbContext = new DbContext())
        {
            UserModel user = new UserModel();
            string sql = @"SELECT * FROM users WHERE Id = @id";
            dbContext.Connection.Open();
            using(SqlCommand command = new SqlCommand(sql, dbContext.Connection))
            {
                command.Parameters.AddWithValue("@id", id);
                var reader = command.ExecuteReader();

                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        user.Id = (int)reader["id"];
                        user.Username = (string)reader["Username"];
                    }              
                }
                reader.Close();
            }
            dbContext.Connection.Close();
            return user;
        }
    }    
    

    public List<UserModel> GetUsers()
    {
        using (var dbContext = new DbContext())
        {
            List<UserModel> users = new List<UserModel>();
            string sql = $"SELECT * FROM users";
            dbContext.Connection.Open();
            using(SqlCommand command = new SqlCommand(sql, dbContext.Connection))
            {
                var reader = command.ExecuteReader();
                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        var ObecnyId = (int)reader["Id"];
                        var ObecnyUsername = (string)reader["Username"];

                        UserModel user = new UserModel
                        {
                            Id = ObecnyId,
                            Username = ObecnyUsername
                        };
                        users.Add(user);
                    }
                }
                reader.Close();
            }
            dbContext.Connection.Close();
            return users;
        }    
    }
}