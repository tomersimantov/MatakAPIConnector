using System;
using System.Collections.Generic;
using Npgsql;

namespace MatakDBConnector
{
    public class RouteModel : Route
    {
        public int AddNewRoute(Route newRoute, out string errorMessage)
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
                        "INSERT INTO route (name, start_datetime, end_datetime, geojson_doc_id, reason_id, priority_id, status_id, org_id, created_by_user_id, sent_to_user_id, approved_by_user_id, note, created, updated, trip_area, driver_name, phone_num) VALUES (@name, @start_datetime, @end_datetime, @geojson_doc_id, @reason_id, @priority_id, @status_id, @org_id, @created_by_user_id, @sent_to_user_id, @approved_by_user_id, @note, @created, @updated, st_geomfromgeojson(@trip_area), @driver_name, @phone_num) RETURNING route_id";
                    newRouteCommandHelper(newRoute, command, true);
                    
                    newRoute.RouteId = Convert.ToInt32(command.ExecuteScalar());

                    RouteHistoryModel routeHistoryModel = new RouteHistoryModel();
                    routeHistoryModel.AddNewRouteHistory(newRoute, out errorMessage);

                    return newRoute.RouteId;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    errorMessage = e.ToString();
                    throw;
                }
            }
        }

        public int UpdateRouteId(Route route, out string errorMessage)
        {
            errorMessage = null;

            if (route.RouteId == 0)
            {
                errorMessage = "Invalid route ID (0)";
                return -1;
            }

            using (var connection = new NpgsqlConnection(ConfigParser.ConnString))
            {
                try
                {
                    connection.Open();

                    NpgsqlCommand command = new NpgsqlCommand();
                    command.Connection = connection;

                    command.CommandText =
                        "UPDATE route SET name = (@name), start_datetime = (@start_datetime), end_datetime = (@end_datetime), _doc_id = (@geojson_doc_id), reason_id = (@reason_id), priority_id = (@priority_id), status_id = (@status_id), org_id = (@org_id), sent_to_user_id = (@sent_to_user_id), approved_by_user_id = (@approved_by_user_id), note = (@note), created = (@created), updated = (@updated), trip_area = st_geomfromgeojson(@trip_area), driver_name = (@driver_name), phone_num = (@phone_num) WHERE route_id = (@routeId) RETURNING route_id";
                    command.Parameters.AddWithValue("routeId", route.RouteId);
                    newRouteCommandHelper(route, command, false);

                    route.RouteId = Convert.ToInt32(command.ExecuteScalar());

                    RouteHistoryModel routeHistoryModel = new RouteHistoryModel();
                    routeHistoryModel.AddNewRouteHistory(route, out errorMessage);

                    return route.RouteId;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    errorMessage = e.ToString();
                    throw;
                }
            }
        }

        public Route GetRouteById(int routeId, out string errorMessage)
        {
            errorMessage = null;
            
            using (var connection = new NpgsqlConnection(ConfigParser.ConnString))
            {
                try
                {
                    connection.Open();

                    NpgsqlCommand command = new NpgsqlCommand();
                    command.Connection = connection;
                    
                    command.CommandText = "SELECT route_id, name, start_datetime, end_datetime, geojson_doc_id, reason_id, priority_id, status_id, org_id, created_by_user_id, sent_to_user_id, approved_by_user_id, note, st_asgeojson(trip_area, 15, 0), driver_name, phone_num FROM route WHERE route_id = (@routeId)";
                    command.Parameters.AddWithValue("routeId", routeId);
                    NpgsqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        return RouteMaker(reader);
                    }

                    errorMessage = "Route not found";
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
        
        public int GetRoutesCountByOrgId(int orgId, out string errorMessage)
        {   
            errorMessage = null;
            var count = -1;

            using (var connection = new NpgsqlConnection(ConfigParser.ConnString))
            {
                try
                {
                    connection.Open();

                    NpgsqlCommand command = new NpgsqlCommand();
                    command.Connection = connection;
                    
                    command.CommandText = "SELECT count(*) FROM route WHERE org_id = (@orgId)";
                    command.Parameters.AddWithValue("orgId", orgId);
                    NpgsqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        count = reader.GetInt32(0);
                    }

                    return count;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    errorMessage = e.ToString();
                    throw;
                }
            }
        }

        public List<Route> GetAllRoutes(out string errorMessage)
        {
            errorMessage = null;
            List<Route> allRoutes = new List<Route>();

            using (var connection = new NpgsqlConnection(ConfigParser.ConnString))
            {
                try
                {
                    connection.Open();

                    NpgsqlCommand command = new NpgsqlCommand();
                    command.Connection = connection;
                    
                    command.CommandText = "SELECT route_id, name, start_datetime, end_datetime, geojson_doc_id, reason_id, priority_id, status_id, org_id, created_by_user_id, sent_to_user_id, approved_by_user_id, note, st_asgeojson(trip_area, 15, 0), driver_name, phone_num FROM route";
                    NpgsqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Route route = new Route();
                        allRoutes.Add(route.RouteMaker(reader));
                    }
                    return allRoutes;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    errorMessage = e.ToString();
                    throw;
                }
            }
        }

        public List<Route> GetAllRoutesByOrgId(int orgId, out string errorMessage)
        {
            errorMessage = null;
            List<Route> allRoutesByOrgId = new List<Route>();

            using (var connection = new NpgsqlConnection(ConfigParser.ConnString))
            {
                try
                {                    
                    connection.Open();  
                    
                    NpgsqlCommand command = new NpgsqlCommand();
                    command.Connection = connection;
                    
                    command.CommandText = "SELECT route_id, name, start_datetime, end_datetime, geojson_doc_id, reason_id, priority_id, status_id, org_id, created_by_user_id, sent_to_user_id, approved_by_user_id, note, st_asgeojson(trip_area, 15, 0), driver_name, phone_num FROM route WHERE org_id = (@orgId)";
                    command.Parameters.AddWithValue("orgId", orgId);
                    NpgsqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Route route = new Route();
                        allRoutesByOrgId.Add(route.RouteMaker(reader));          
                    }

                    return allRoutesByOrgId;

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    errorMessage = e.ToString();
                    throw;
                }
            }
        }
        
         public List<Route> GetAllRoutesByCreatorId(int createdByUserId, out string errorMessage)
        {
            errorMessage = null;
            List<Route> allRoutesByCreatorId = new List<Route>();

            using (var connection = new NpgsqlConnection(ConfigParser.ConnString))
            {
                try
                {
                    connection.Open();  
                    
                    NpgsqlCommand command = new NpgsqlCommand();
                    command.Connection = connection;

                    command.CommandText = "SELECT route_id, name, start_datetime, end_datetime, geojson_doc_id, reason_id, priority_id, status_id, org_id, created_by_user_id, sent_to_user_id, approved_by_user_id, note, st_asgeojson(trip_area, 15, 0), driver_name, phone_num FROM route WHERE created_by_user_id = (@createdByUserId)";
                    command.Parameters.AddWithValue("createdByUserId", createdByUserId);
                    NpgsqlDataReader reader = command.ExecuteReader();
                
                    while (reader.Read())
                    {
                        Route route = new Route();
                        allRoutesByCreatorId.Add(route.RouteMaker(reader));   
                    }

                    return allRoutesByCreatorId;

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    errorMessage = e.ToString();
                    throw;
                }      
            }
        }
          
        public List<Route> GetAllRoutesByReceiverId(int sentToUserId, out string errorMessage)
        {
            errorMessage = null;
            List<Route> allRoutesByReceiverId = new List<Route>();

            using (var connection = new NpgsqlConnection(ConfigParser.ConnString))
            {
                try
                {
                    connection.Open();  
                    
                    NpgsqlCommand command = new NpgsqlCommand();
                    command.Connection = connection;

                    command.CommandText = "SELECT route_id, name, start_datetime, end_datetime, geojson_doc_id, reason_id, priority_id, status_id, org_id, created_by_user_id, sent_to_user_id, approved_by_user_id, note, st_asgeojson(trip_area, 15, 0), driver_name, phone_num FROM route WHERE sent_to_user_id = (@sentToUserId)";
                    command.Parameters.AddWithValue("sentToUserId", sentToUserId);
                    NpgsqlDataReader reader = command.ExecuteReader();
                
                    while (reader.Read())
                    {
                        Route route = new Route();
                        allRoutesByReceiverId.Add(route.RouteMaker(reader));   
                    }

                    return allRoutesByReceiverId;

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