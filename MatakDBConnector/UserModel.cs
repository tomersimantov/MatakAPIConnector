using System;
using System.Collections.Generic;
using Npgsql;

namespace MatakDBConnector
{
    public class UserModel : User
    {
        public List<User> getAllUsers(out string errorMessage)
        {
            List<User> allUsers = new List<User>();
            errorMessage = null;

            using (var connection = new NpgsqlConnection(ConfigParser.ConnString))
            {
                try
                {
                    connection.Open();

                    NpgsqlCommand command = new NpgsqlCommand();
                    command.Connection = connection;

                    command.CommandText = "SELECT * FROM playground.public.user";
                    NpgsqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        User user = new User();
                        allUsers.Add(user.UserMaker(reader));
                    }

                    return allUsers;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    errorMessage = e.ToString();
                    throw;
                }
            }
        }

        public User getUserByUserId(int userId, out string errorMessage)
        {
            errorMessage = null;
            
            using (var connection = new NpgsqlConnection(ConfigParser.ConnString))
            {
                try
                {
                    connection.Open();

                    NpgsqlCommand command = new NpgsqlCommand();
                    command.Connection = connection;
                    
                    command.CommandText = "SELECT * FROM playground.public.user WHERE user_id = (@userId)";
                    command.Parameters.AddWithValue("userId", userId);
                    NpgsqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        return UserMaker(reader);
                    }

                    errorMessage = "User not found";
                    return null;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    errorMessage = e.ToString();
                    throw;
                }
            }
        }

        public User getUserByEmail(string email, out string errorMessage)
        {
            errorMessage = null;
            
            using (var connection = new NpgsqlConnection(ConfigParser.ConnString))
            {
                try
                {
                    connection.Open();

                    NpgsqlCommand command = new NpgsqlCommand();
                    command.Connection = connection;
                    
                    command.CommandText = "SELECT * FROM playground.public.user WHERE email = (@email)";
                    command.Parameters.AddWithValue("email", email);
                    NpgsqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        return UserMaker(reader);
                    }

                    errorMessage = "User not found";
                    return null;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    errorMessage = e.ToString();
                    throw;
                }
            }
        }
        
        public List<User> getAllUsersByOrgId(int orgId, out string errorMessage)
        {
            List<User> allUsers = new List<User>();
            errorMessage = null;

            using (var connection = new NpgsqlConnection(ConfigParser.ConnString))
            {
                try
                {
                    connection.Open();

                    NpgsqlCommand command = new NpgsqlCommand();
                    command.Connection = connection;
                    
                    command.CommandText = "SELECT * FROM playground.public.user WHERE org_id = (@p)";
                    command.Parameters.AddWithValue("p", orgId);
                    NpgsqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        User user = new User();
                        allUsers.Add(user.UserMaker(reader));
                    }

                    return allUsers;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    errorMessage = e.ToString();
                    throw;
                }
            }
        }

        public Boolean authenticateUser(string username, string password, out string errorMessage)
        {
            errorMessage = null;
            string result = null;

            using (var connection = new NpgsqlConnection(ConfigParser.ConnString))
            {
                try
                {
                    connection.Open();

                    NpgsqlCommand command = new NpgsqlCommand();
                    command.Connection = connection;
                    
                    command.CommandText = "SELECT password FROM playground.public.user WHERE email = (@p)";
                    command.Parameters.AddWithValue("p", username);
                    NpgsqlDataReader reader = command.ExecuteReader();
                
                    while (reader.Read())
                    {
                        result = reader.GetString(0);
                    
                    }

                    if (result == password)
                        return true;
                
                    errorMessage = "Not found or no match";
                    return false;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    errorMessage = e.ToString();
                    throw;
                }
            }
        }
    }
}