using System;
using Npgsql;

namespace MatakDBConnector
{
    public class Route
    {
        //TODO add created and updated dates 
        //TODO newRouteCommandHelper should differentiate between updated and new route for created timestamp
        //TODO delete this
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
        private string _driverName;
        private string _phone_num;
        private DateTime _last_changed;

        public Route()
        {
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
            _driverName = "0";
            _phone_num = "0";
            _last_changed = DateTime.Now;
        }
        
        public Route(string name, DateTime startDatetime, DateTime endDatetime, int geojsonDocId,
            int reasonId, int priorityId, int statusId, int orgId, int createdByUserId, int sentToUserId,
            int approvedByUserId, string note, string geoJsonString, string driverName, string phone_num, DateTime last_changed)
        {
            _routeId = 0;
            _name = name;
            _startDatetime = startDatetime;
            _endDatetime = endDatetime;
            _geojsonDocId = geojsonDocId;
            _reasonId = reasonId;
            _priorityId = priorityId;
            _statusId = statusId;
            _orgId = orgId;
            _createdByUserId = createdByUserId;
            _sentToUserId = sentToUserId;
            _approvedByUserId = approvedByUserId;
            _note = note;
            _geoJsonString = geoJsonString;
            _driverName = driverName;
            _phone_num = phone_num;
            _last_changed = last_changed;
        }
        
        public Route(int routeId, string name, DateTime startDatetime, DateTime endDatetime, int geojsonDocId,
            int reasonId, int priorityId, int statusId, int orgId, int createdByUserId, int sentToUserId,
            int approvedByUserId, string note, string geoJsonString, string driverName, string phone_num, DateTime last_changed)
        {
            _routeId = routeId;
            _name = name;
            _startDatetime = startDatetime;
            _endDatetime = endDatetime;
            _geojsonDocId = geojsonDocId;
            _reasonId = reasonId;
            _priorityId = priorityId;
            _statusId = statusId;
            _orgId = orgId;
            _createdByUserId = createdByUserId;
            _sentToUserId = sentToUserId;
            _approvedByUserId = approvedByUserId;
            _note = note;
            _geoJsonString = geoJsonString;
            _driverName = driverName;
            _phone_num = phone_num;
            _last_changed = last_changed;
        }

        public Route RouteMaker(NpgsqlDataReader reader)
        {
            RouteId = reader.GetInt32(0);
            Name = reader.GetString(1);
            StartDatetime = reader.GetDateTime(2);
            EndDatetime = reader.GetDateTime(3);
            GeojsonDocId = reader.GetInt32(4);
            ReasonId = reader.GetInt32(5);
            PriorityId = reader.GetInt32(6);
            StatusId = reader.GetInt32(7);
            OrgId = reader.GetInt32(8);
            CreatedByUserId = reader.GetInt32(9);
            SentToUserId = reader.GetInt32(10);
            ApprovedByUserId = reader.GetInt32(11);
            Note = reader.GetString(12);
            GeoJsonString = reader.GetString(13);
            driverName = reader.GetString(14);
            phone_num = reader.GetString(15);
            last_changed = reader.GetDateTime(16);
            return this;
        }
        
        protected void newRouteCommandHelper(Route route, NpgsqlCommand command, Boolean isNew)
        {
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
            if (isNew)
            {
                command.Parameters.AddWithValue("approved_by_user_id", 0);
            }
            else
            {
                command.Parameters.AddWithValue("approved_by_user_id", route.ApprovedByUserId);
                //TODO add created and updated to route
            }
            command.Parameters.AddWithValue("note", route.Note);
            command.Parameters.AddWithValue("created", DateTime.Now);
            command.Parameters.AddWithValue("updated", DateTime.Now);
            command.Parameters.AddWithValue("trip_area", route.GeoJsonString);
            command.Parameters.AddWithValue("driver_name", route.driverName);
            command.Parameters.AddWithValue("phone_num", route.phone_num);
            command.Parameters.AddWithValue("last_changed", DateTime.Now);
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
        
        public string driverName
        {
            get => _driverName;
            set => _driverName = value;
        }

        public string phone_num
        {
            get => _phone_num;
            set => _phone_num = value;
        }

        public DateTime last_changed
        {
            get => _last_changed;
            set => _last_changed = value;
        }
    }
}