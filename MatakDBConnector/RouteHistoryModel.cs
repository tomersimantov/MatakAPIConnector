using System;
using System.Collections.Generic;
using Npgsql;

namespace MatakDBConnector
{
    public class RouteHistoryModel : RouteHistory
    {
        public int AddNewRouteHistory(Route route, out string errorMessage)
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
                        "INSERT INTO route_history (route_id, name, start_datetime, end_datetime, geojson_doc_id, reason_id, priority_id, status_id, org_id, created_by_user_id, sent_to_user_id, approved_by_user_id, note, created, updated, trip_area) VALUES (@route_id, @name, @start_datetime, @end_datetime, @geojson_doc_id, @reason_id, @priority_id, @status_id, @org_id, @created_by_user_id, @sent_to_user_id, @approved_by_user_id, @note, @created, @updated, st_geomfromgeojson(@trip_area)) RETURNING route_history_id";
                    newRouteHistoryCommandHelper(route, command);

                    return Convert.ToInt32(command.ExecuteScalar());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    errorMessage = e.ToString();
                    throw;
                }
            }
        }
        
        public List<RouteHistory> GetAllRouteHistories(out string errorMessage)
        {
            errorMessage = null;
            List<RouteHistory> allRouteHistories = new List<RouteHistory>();

            using (var connection = new NpgsqlConnection(ConfigParser.ConnString))
            {
                try
                {
                    connection.Open();

                    NpgsqlCommand command = new NpgsqlCommand();
                    command.Connection = connection;
                    
                    command.CommandText = "SELECT route_history_id, route_id, name, start_datetime, end_datetime, geojson_doc_id, reason_id, priority_id, status_id, org_id, created_by_user_id, sent_to_user_id, approved_by_user_id, note, st_asgeojson(trip_area, 15, 0) FROM route_history";
                    NpgsqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        RouteHistory routeHistory = new RouteHistory();
                        allRouteHistories.Add(routeHistory.RouteHistoryMaker(reader));
                    }
                    return allRouteHistories;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    errorMessage = e.ToString();
                    throw;
                }
            }
        }
        
        public List<RouteHistory> GetRouteHistoryListByRouteId(int routeId, out string errorMessage)
        {
            errorMessage = null;
            List<RouteHistory> allRouteHistories = new List<RouteHistory>();

            using (var connection = new NpgsqlConnection(ConfigParser.ConnString))
            {
                try
                {
                    connection.Open();

                    NpgsqlCommand command = new NpgsqlCommand();
                    command.Connection = connection;
                    
                    command.CommandText = "SELECT route_history_id, route_id, name, start_datetime, end_datetime, geojson_doc_id, reason_id, priority_id, status_id, org_id, created_by_user_id, sent_to_user_id, approved_by_user_id, note, st_asgeojson(trip_area, 15, 0) FROM route_history WHERE route_id = (@routeId)";
                    command.Parameters.AddWithValue("routeId", routeId);
                    NpgsqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        RouteHistory routeHistory = new RouteHistory();
                        allRouteHistories.Add(routeHistory.RouteHistoryMaker(reader));
                    }
                    return allRouteHistories;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    errorMessage = e.ToString();
                    throw;
                }
            }
        }
        
        public List<RouteHistory> GetRouteHistoryListByOrgId(int orgId, out string errorMessage)
        {
            errorMessage = null;
            List<RouteHistory> allRouteHistories = new List<RouteHistory>();

            using (var connection = new NpgsqlConnection(ConfigParser.ConnString))
            {
                try
                {
                    connection.Open();

                    NpgsqlCommand command = new NpgsqlCommand();
                    command.Connection = connection;
                    
                    command.CommandText = "SELECT route_history_id, route_id, name, start_datetime, end_datetime, geojson_doc_id, reason_id, priority_id, status_id, org_id, created_by_user_id, sent_to_user_id, approved_by_user_id, note, st_asgeojson(trip_area, 15, 0) FROM route_history WHERE org_id = (@orgId)";
                    command.Parameters.AddWithValue("orgId", orgId);
                    NpgsqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        RouteHistory routeHistory = new RouteHistory();
                        allRouteHistories.Add(routeHistory.RouteHistoryMaker(reader));
                    }
                    return allRouteHistories;
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