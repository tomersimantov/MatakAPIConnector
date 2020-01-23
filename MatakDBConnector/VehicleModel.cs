using System;
using System.Collections.Generic;
using Npgsql;

namespace MatakDBConnector
{
    public class VehicleModel : Vehicle
    {
        public List<Vehicle> getAllVehicles(out string errorMessage)
        {
            List<Vehicle> allVehicles = new List<Vehicle>();
            errorMessage = null;

            using (var connection = new NpgsqlConnection(ConfigParser.ConnString))
            {
                try
                {
                    connection.Open();

                    NpgsqlCommand command = new NpgsqlCommand();
                    command.Connection = connection;
                    
                    command.CommandText = "SELECT * FROM vehicle";
                    NpgsqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Vehicle vehicle = new Vehicle();
                        allVehicles.Add(vehicle.VehicleMaker(reader));
                    }

                    return allVehicles;
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