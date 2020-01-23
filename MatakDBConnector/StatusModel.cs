using System;
using System.Collections.Generic;
using Npgsql;

namespace MatakDBConnector
{
    public class StatusModel : Status
    {
        public List<Status> getAllStati(out string errorMessage)
        {
            List<Status> allStati = new List<Status>();
            errorMessage = null;

            using (var connection = new NpgsqlConnection(ConfigParser.ConnString))
            {
                try
                {
                    connection.Open();

                    NpgsqlCommand command = new NpgsqlCommand();
                    command.Connection = connection;
                    
                    command.CommandText = "SELECT * FROM status";
                    NpgsqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Status status = new Status();
                        allStati.Add(status.StatusMaker(reader));
                    }

                    return allStati;
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