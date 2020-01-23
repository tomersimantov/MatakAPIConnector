using System;
using System.Collections.Generic;
using Npgsql;

namespace MatakDBConnector
{
    public class EscortOrgModel : EscortOrg
    {
        public void AddNewEscortOrg(EscortOrg escortOrg, out string errorMessage)
        {
            errorMessage = null;

            using (var connection = new NpgsqlConnection(ConfigParser.ConnString))
            {
                try
                {
                    connection.Open();

                    NpgsqlCommand command = new NpgsqlCommand();
                    command.Connection = connection;
                    
                    command.CommandText =
                        "INSERT INTO escort_org (org_id, route_id, created, updated) VALUES (@org_id, @route_id, @created, @updated)";
                    newEscortOrgCommandHelper(escortOrg, command);

                    command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    errorMessage = e.ToString();
                    throw;
                }
            }
        }
        public List<EscortOrg> GetAllEscortOrgs(out string errorMessage)
        {
            List<EscortOrg> allEscortOrgs = new List<EscortOrg>();
            errorMessage = null;

            using (var connection = new NpgsqlConnection(ConfigParser.ConnString))
            {
                try
                {
                    connection.Open();

                    NpgsqlCommand command = new NpgsqlCommand();
                    command.Connection = connection;
                    
                    command.CommandText = "SELECT * FROM escort_org";
                    NpgsqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        EscortOrg escortOrg = new EscortOrg();
                        allEscortOrgs.Add(escortOrg.EscortOrgMaker(reader));
                    }

                    return allEscortOrgs;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    errorMessage = e.ToString();
                    throw;
                }
            }
        }
        
        public List<EscortOrg> GetAllEscortOrgsByRouteId(int routeId, out string errorMessage)
        {
            List<EscortOrg> allEscortOrgs = new List<EscortOrg>();
            errorMessage = null;

            using (var connection = new NpgsqlConnection(ConfigParser.ConnString))
            {
                try
                {
                    connection.Open();

                    NpgsqlCommand command = new NpgsqlCommand();
                    command.Connection = connection;
                    
                    command.CommandText = "SELECT * FROM organization WHERE route_id = (@p)";
                    command.Parameters.AddWithValue("p", routeId);
                    NpgsqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        EscortOrg escortOrg = new EscortOrg();
                        allEscortOrgs.Add(escortOrg.EscortOrgMaker(reader));
                    }

                    return allEscortOrgs;
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