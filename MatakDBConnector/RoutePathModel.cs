using System;
using System.Collections.Generic;
using Npgsql;

namespace MatakDBConnector
{
    public class RoutePathModel : RoutePath
    {
        public List<RoutePath> GetAllRoutePaths(out String errorMessage)
        {
            errorMessage = null;
            List<RoutePath> routePaths = new List<RoutePath>();

            using (var connection = new NpgsqlConnection(ConfigParser.ConnString))
            {
                try
                {
                    connection.Open();

                    NpgsqlCommand command = new NpgsqlCommand();
                    command.Connection = connection;
                    command.CommandText = "SELECT route_path_id, route_id, reason_id, note, created_by_user_id, updated_by_user_id, st_asgeojson(geo_path,15,0), created, updated FROM route_path";
                    NpgsqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        RoutePath routePath = new RoutePath();
                        routePaths.Add(routePath.RoutePathMaker(reader));

                    }
                    return routePaths;
                }
                catch (Exception error)
                {
                    Console.WriteLine(error);
                    errorMessage = error.ToString();
                    throw;
                }
            }
        }

        public int AddNewRoutePath(RoutePath routePath, out String errorMessage)
        {
            errorMessage = null;
            using (var connection = new NpgsqlConnection(ConfigParser.ConnString))
            {
                try
                {
                    connection.Open();

                    NpgsqlCommand command = new NpgsqlCommand();
                    command.Connection = connection;
                    command.CommandText = "Insert Into route_path (route_id,reason_id,note,created_by_user_id,updated_by_user_id,geo_path,created,updated ) values (@route_id,@reason_id,@note,@created_by_user_id,@updated_by_user_id,st_geomfromgeojson(@geo_path),@created,@updated ) Returning route_path_id";
                    NewPathTripAreaCommanHelper(routePath, command);

                    return Convert.ToInt32(command.ExecuteScalar());
                }catch (Exception error)
                {
                    Console.WriteLine(error);
                    errorMessage = error.ToString();
                    throw;
                }
            }
        }

        public RoutePath GetRoutePathByRoutePathID(int routePathId, out String errorMessage)
        {
            errorMessage = null;
            if (routePathId == 0)
            {
                errorMessage = "Invaild RoutePath Id (0)";
                return null;
            }
            using (var connection = new NpgsqlConnection(ConfigParser.ConnString))
            {
                try
                {
                    connection.Open();

                    NpgsqlCommand command = new NpgsqlCommand();
                    command.Connection = connection;
                    command.CommandText = "SELECT route_path_id, route_id, reason_id, note, created_by_user_id, updated_by_user_id, st_asgeojson(geo_path,15,0), created, updated FROM route_path WHERE route_path_id = (@routePathId)";
                    command.Parameters.AddWithValue("routePathId", routePathId);

                    NpgsqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        return RoutePathMaker(reader);
                    }
                    return null;
                }catch (Exception error)
                {
                    Console.WriteLine(error);
                    errorMessage = error.ToString();
                    throw;
                }
            }
        }

        public List<RoutePath> RoutePathByCreatedByUser(int userId, out String errorMessage)
        {
            errorMessage = null;
            if (userId <= 0)
            {
                errorMessage = "Invaild user Id (0)";
                return null;
            }
            List<RoutePath> routePaths = new List<RoutePath>();
            using (var connection = new NpgsqlConnection(ConfigParser.ConnString))
            {
                try
                {
                    connection.Open();

                    NpgsqlCommand command = new NpgsqlCommand();
                    command.Connection = connection;

                    command.CommandText = "SELECT route_path_id, route_id, reason_id, note, created_by_user_id, updated_by_user_id, st_asgeojson(geo_path, 15 ,0), created,updated FROM route_path WHERE created_by_user_id = (@userId)";
                    command.Parameters.AddWithValue("userId", userId);
                    NpgsqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        RoutePath routePath = new RoutePath();
                        routePaths.Add(routePath.RoutePathMaker(reader));
                    }

                    return routePaths;
                }
                catch (Exception error)
                {
                    Console.WriteLine(error);
                    errorMessage = error.ToString();
                    throw;
                }
            }
        }

        public List<RoutePath> GetRoutePathByRouteId(int routeID, out String errorMessage)
        {
            errorMessage = null;
            List<RoutePath> routePaths = new List<RoutePath>();

            using (var connection = new NpgsqlConnection(ConfigParser.ConnString))
            {
                try
                {
                    connection.Open();
                    NpgsqlCommand command = new NpgsqlCommand();
                    command.Connection = connection;

                    command.CommandText = "SELECT route_path_id, route_id, reason_id, note, created_by_user_id, updated_by_user_id, st_asgeojson(geo_path,15,0), created, updated  FROM route_path WHERE route_id = (@routeID)";
                    command.Parameters.AddWithValue("routeID", routeID);
                    NpgsqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        RoutePath routePath = new RoutePath();
                        routePaths.Add(routePath.RoutePathMaker(reader));
                    }

                    return routePaths;
                }
                catch (Exception error)
                {
                    Console.WriteLine(error);
                    errorMessage = error.ToString();
                    throw;
                }
            }
        }


        public int UpdateRoutePathByRoutePathId(RoutePath routePath, out String errorMessage)
        {
            errorMessage = null;

            if (routePath.RoutePathId <= 0)
            {
                errorMessage = "Invaild RoutePath Id (0)";
                return -1;
            }
            using (var connection = new NpgsqlConnection(ConfigParser.ConnString))
            {
                try
                {
                    connection.Open();

                    NpgsqlCommand command = new NpgsqlCommand();
                    command.Connection = connection;
                    command.CommandText ="UPDATE route_path SET route_id = @route_id, reason_id = @reason_id, note = @note, created_by_user_id = @created_by_user_id, updated_by_user_id = @updated_by_user_id, geo_path = st_geomfromgeojson(@geo_path), created = @created, updated = @updated WHERE route_path_id = (@route_path_id) RETURNING route_path_id";
                    NewPathTripAreaCommanHelper(routePath, command);

                    return Convert.ToInt32(command.ExecuteScalar());
                }
                catch (Exception error)
                {
                    Console.WriteLine(error);
                    errorMessage = error.ToString();
                    throw;
                }
            }
        }
    }
}
