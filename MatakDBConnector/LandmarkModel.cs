using System;
using System.Collections.Generic;
using Npgsql;

namespace MatakDBConnector
{
    public class LandmarkModel : Landmark
    {
        public int AddNewLandmark(Landmark newLandmark, out string errorMessage)
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
                        "INSERT INTO landmark (name, start_datetime, end_datetime, landmark_status_id, created_by_user_id, updated_by_user_id, note, created, updated, landmark_area) VALUES (@name, @start_datetime, @end_datetime, @landmark_status_id, @created_by_user_id, @updated_by_user_id, @note, @created, @updated, st_geomfromgeojson(@landmark_area)) RETURNING landmark_id";
                    newLandmarkCommandHelper(newLandmark, command);

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

        public int UpdateLandmarkId(Landmark landmark, out string errorMessage)
        {
            errorMessage = null;

            if (landmark.LandmarkId == 0)
            {
                errorMessage = "Invalid landmark ID (0)";
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
                        "UPDATE landmark SET name = (@name), start_datetime = (@start_datetime), end_datetime = (@end_datetime), landmark_status_id = (@landmark_status_id), updated_by_user_id = (@updated_by_user_id), note = (@note), created = (@created), updated = (@updated), landmark_area = st_geomfromgeojson(@landmark_area) WHERE landmark_id = (@landmarkId) RETURNING landmark_id";
                    command.Parameters.AddWithValue("landmarkId", landmark.LandmarkId);
                    newLandmarkCommandHelper(landmark, command);

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

        public Landmark GetLandmarkById(int landmarkId, out string errorMessage)
        {
            errorMessage = null;
            
            using (var connection = new NpgsqlConnection(ConfigParser.ConnString))
            {
                try
                {
                    connection.Open();

                    NpgsqlCommand command = new NpgsqlCommand();
                    command.Connection = connection;
                    
                    command.CommandText = "SELECT landmark_id, name, start_datetime, end_datetime, landmark_status_id, created_by_user_id, updated_by_user_id, note, st_asgeojson(landmark_area, 15, 0) FROM landmark WHERE landmark_id = (@landmarkId)";
                    command.Parameters.AddWithValue("landmarkId", landmarkId);
                    NpgsqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        return LandmarkMaker(reader);
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

        public List<Landmark> GetAllLandmarks(out string errorMessage)
        {
            errorMessage = null;
            List<Landmark> allLandmarks = new List<Landmark>();

            using (var connection = new NpgsqlConnection(ConfigParser.ConnString))
            {
                try
                {
                    connection.Open();

                    NpgsqlCommand command = new NpgsqlCommand();
                    command.Connection = connection;
                    
                    command.CommandText = "SELECT landmark_id, name, start_datetime, end_datetime, landmark_status_id, created_by_user_id, updated_by_user_id, note, st_asgeojson(landmark_area, 15, 0) FROM landmark";
                    NpgsqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Landmark landmark = new Landmark();
                        allLandmarks.Add(landmark.LandmarkMaker(reader));
                    }
                    return allLandmarks;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    errorMessage = e.ToString();
                    throw;
                }
            }
        }
        
         public List<Landmark> GetAllLandmarksByCreatorId(int createdByUserId, out string errorMessage)
        {
            errorMessage = null;
            List<Landmark> allLandmarksByCreatorId = new List<Landmark>();

            using (var connection = new NpgsqlConnection(ConfigParser.ConnString))
            {
                try
                {
                    connection.Open();  
                    
                    NpgsqlCommand command = new NpgsqlCommand();
                    command.Connection = connection;

                    command.CommandText = "SELECT landmark_id, name, start_datetime, end_datetime, landmark_status_id, created_by_user_id, updated_by_user_id, note, st_asgeojson(landmark_area, 15, 0) FROM landmark WHERE created_by_user_id = (@createdByUserId)";
                    command.Parameters.AddWithValue("createdByUserId", createdByUserId);
                    NpgsqlDataReader reader = command.ExecuteReader();
                
                    while (reader.Read())
                    {
                        Landmark landmark = new Landmark();
                        allLandmarksByCreatorId.Add(landmark.LandmarkMaker(reader));   
                    }

                    return allLandmarksByCreatorId;

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