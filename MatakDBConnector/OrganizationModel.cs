using System;
using System.Collections.Generic;
using Npgsql;

namespace MatakDBConnector
{
    public class OrganizationModel : Organization
    {
        public List<Organization> getAllOrganizations(out string errorMessage)
        {
            List<Organization> allOrganizations = new List<Organization>();
            errorMessage = null;

            using (var connection = new NpgsqlConnection(ConfigParser.ConnString))
            {
                try
                {
                    connection.Open();

                    NpgsqlCommand command = new NpgsqlCommand();
                    command.Connection = connection;

                    command.CommandText = "SELECT * FROM organization";
                    NpgsqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Organization organization = new Organization();
                        allOrganizations.Add(organization.OrganizationMaker(reader));
                    }

                    return allOrganizations;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    errorMessage = e.ToString();
                    throw;
                }
            }
        }
        
        public Organization getOrganizationById(int orgid, out string errorMessage)
        {
            errorMessage = null;

            using (var connection = new NpgsqlConnection(ConfigParser.ConnString))
            {
                try
                {
                    connection.Open();

                    NpgsqlCommand command = new NpgsqlCommand();
                    command.Connection = connection;
                    
                    command.CommandText = "SELECT * FROM organization WHERE org_id = (@p)";
                    command.Parameters.AddWithValue("p", orgid);
                    NpgsqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        return OrganizationMaker(reader);;
                    }

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
    }
}