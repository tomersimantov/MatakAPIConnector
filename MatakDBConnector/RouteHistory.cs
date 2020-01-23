using System;
using Npgsql;

namespace MatakDBConnector
{
    public class RouteHistory
    {
        private int _routeHistoryId;
        private int _routeId;
        private string _name;
        private DateTime _startDatetime;
        private DateTime _endDatetime;
        private int _geojsonDocId;
        private int _reasonId;
        private int _priorityId;
        private int _statusId;
        private int _orgId;
        private int _createdByUserId;
        private int _sentToUserId;
        private int _approvedByUserId;
        private string _note;
        private string _geoJsonString;

        public RouteHistory()
        {
            _routeHistoryId = 0;
            _routeId = 0;
            _name = "0";
            _startDatetime = DateTime.Now;
            _endDatetime = DateTime.Now;
            _geojsonDocId = 0;
            _reasonId = 0;
            _priorityId = 0;
            _statusId = 0;
            _orgId = 0;
            _createdByUserId = 0;
            _sentToUserId = 0;
            _approvedByUserId = 0;
            _note = "0";
            _geoJsonString = "0";
        }

        public RouteHistory RouteHistoryMaker(NpgsqlDataReader reader)
        {
            RouteHistoryId = reader.GetInt32(0);
            RouteId = reader.GetInt32(1);
            Name = reader.GetString(2);
            StartDatetime = reader.GetDateTime(3);
            EndDatetime = reader.GetDateTime(4);
            GeojsonDocId = reader.GetInt32(5);
            ReasonId = reader.GetInt32(6);
            PriorityId = reader.GetInt32(7);
            StatusId = reader.GetInt32(8);
            OrgId = reader.GetInt32(9);
            CreatedByUserId = reader.GetInt32(10);
            SentToUserId = reader.GetInt32(11);
            ApprovedByUserId = reader.GetInt32(12);
            Note = reader.GetString(13);
            GeoJsonString = reader.GetString(14);
                
            return this;
        }
        
        protected void newRouteHistoryCommandHelper(Route route, NpgsqlCommand command)
        {
            command.Parameters.AddWithValue("route_id", route.RouteId);
            command.Parameters.AddWithValue("name", route.Name);
            command.Parameters.AddWithValue("start_datetime", route.StartDatetime);
            command.Parameters.AddWithValue("end_datetime", route.EndDatetime);
            command.Parameters.AddWithValue("geojson_doc_id", 0);
            command.Parameters.AddWithValue("reason_id", route.ReasonId);
            command.Parameters.AddWithValue("priority_id", route.PriorityId);
            command.Parameters.AddWithValue("status_id", route.StatusId);
            command.Parameters.AddWithValue("org_id", route.OrgId);
            command.Parameters.AddWithValue("created_by_user_id", route.CreatedByUserId);
            command.Parameters.AddWithValue("sent_to_user_id", route.SentToUserId);
            command.Parameters.AddWithValue("approved_by_user_id", route.ApprovedByUserId);
            command.Parameters.AddWithValue("note", route.Note);
            command.Parameters.AddWithValue("created", DateTime.Now);
            command.Parameters.AddWithValue("updated", DateTime.Now);
            command.Parameters.AddWithValue("trip_area", route.GeoJsonString);
        }

        public int RouteHistoryId
        {
            get => _routeHistoryId;
            set => _routeHistoryId = value;
        }

        public int RouteId
        {
            get => _routeId;
            set => _routeId = value;
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

        public int GeojsonDocId
        {
            get => _geojsonDocId;
            set => _geojsonDocId = value;
        }

        public int ReasonId
        {
            get => _reasonId;
            set => _reasonId = value;
        }

        public int PriorityId
        {
            get => _priorityId;
            set => _priorityId = value;
        }

        public int StatusId
        {
            get => _statusId;
            set => _statusId = value;
        }

        public int OrgId
        {
            get => _orgId;
            set => _orgId = value;
        }

        public int CreatedByUserId
        {
            get => _createdByUserId;
            set => _createdByUserId = value;
        }

        public int SentToUserId
        {
            get => _sentToUserId;
            set => _sentToUserId = value;
        }

        public int ApprovedByUserId
        {
            get => _approvedByUserId;
            set => _approvedByUserId = value;
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