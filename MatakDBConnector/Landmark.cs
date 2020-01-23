using System;
using Npgsql;

namespace MatakDBConnector
{
    public class Landmark
    {
        private int _landmarkId;
        private string _name;
        private DateTime _startDatetime;
        private DateTime _endDatetime;
        private int _landmarkStatusId;
        private int _createdByUserId;
        private int _updatedByUserId;
        private string _note;
        private string _geoJsonString;

        public Landmark()
        {
            _landmarkId = 0;
            _name = "0";
            _startDatetime = DateTime.Now;
            _endDatetime = DateTime.Now;
            _landmarkStatusId = 0;
            _createdByUserId = 0;
            _updatedByUserId = 0;
            _note = "0";
            _geoJsonString = "0";
        }
        
        public Landmark(int landmarkId, string name, DateTime startDatetime, DateTime endDatetime, int landmarkStatusId, int createdByUserId,
            int updatedByUserId, string note, string geoJsonString)
        {
            _landmarkId = landmarkId;
            _name = name;
            _startDatetime = startDatetime;
            _endDatetime = endDatetime;
            _landmarkStatusId = landmarkStatusId;
            _createdByUserId = createdByUserId;
            _updatedByUserId = updatedByUserId;
            _note = note;
            _geoJsonString = geoJsonString;
        }

        public Landmark LandmarkMaker(NpgsqlDataReader reader)
        {
            LandmarkId = reader.GetInt32(0);
            Name = reader.GetString(1);
            StartDatetime = reader.GetDateTime(2);
            EndDatetime = reader.GetDateTime(3);
            LandmarkStatusId = reader.GetInt32(4);
            CreatedByUserId = reader.GetInt32(5);
            UpdatedByUserId = reader.GetInt32(6);
            Note = reader.GetString(7);
            GeoJsonString = reader.GetString(8);
                
            return this;
        }
        
        protected void newLandmarkCommandHelper(Landmark landmark, NpgsqlCommand command)
        {
            command.Parameters.AddWithValue("name", landmark.Name);
            command.Parameters.AddWithValue("start_datetime", landmark.StartDatetime);
            command.Parameters.AddWithValue("end_datetime", landmark.EndDatetime);
            command.Parameters.AddWithValue("landmark_status_id", landmark.LandmarkStatusId);
            command.Parameters.AddWithValue("created_by_user_id", landmark.CreatedByUserId);
            command.Parameters.AddWithValue("updated_by_user_id", landmark.UpdatedByUserId);
            command.Parameters.AddWithValue("note", landmark.Note);
            command.Parameters.AddWithValue("created", DateTime.Now);
            command.Parameters.AddWithValue("updated", DateTime.Now);
            command.Parameters.AddWithValue("landmark_area", landmark.GeoJsonString);
        }
        
        public int LandmarkId
        {
            get => _landmarkId;
            set => _landmarkId = value;
        }

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public DateTime StartDatetime
        {
            get => _startDatetime;
            set => _startDatetime = value;
        }

        public DateTime EndDatetime
        {
            get => _endDatetime;
            set => _endDatetime = value;
        }

        public int LandmarkStatusId
        {
            get => _landmarkStatusId;
            set => _landmarkStatusId = value;
        }

        public int CreatedByUserId
        {
            get => _createdByUserId;
            set => _createdByUserId = value;
        }
        
        public int UpdatedByUserId
        {
            get => _updatedByUserId;
            set => _updatedByUserId = value;
        }
        
        public string Note
        {
            get => _note;
            set => _note = value;
        }

        public string GeoJsonString
        {
            get => _geoJsonString;
            set => _geoJsonString = value;
        }
    }
}