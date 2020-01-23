using System;
using System.Collections.Generic;
using Npgsql;

namespace MatakDBConnector
{
    public class ReasonModel : Reason
    {
        public List<Reason> GetAllReasons(out string errorMessage)
        {
            List<Reason> allReasons = new List<Reason>();
            errorMessage = null;

            using (var connection = new NpgsqlConnection(ConfigParser.ConnString))
            {
                try
                {
                    connection.Open();

                    NpgsqlCommand command = new NpgsqlCommand();
                    command.Connection = connection;
                    
                    command.CommandText = "SELECT * FROM reason";
                    NpgsqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Reason reason = new Reason();
                        allReasons.Add(reason.ReasonMaker(reader));
                    }

                    return allReasons;
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