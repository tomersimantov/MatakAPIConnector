using Npgsql;
using System;

namespace MatakDBConnector
{
    public class RoutePath
    {
        private int _routePathId;
        private int _routeId;
        private String _geometry;
        private String _note;
        private int _reasonId;
        private int _createdByUserId;
        private int _updatedByUserId;
        private DateTime _created;
        private DateTime _updated;

        public RoutePath()
        {
            _routePathId = 0;
            _routeId = 0;
            _geometry = "0";
            _note = "0";
            _reasonId = 0;
            _createdByUserId = 0;
            _updatedByUserId = 0;
            _created = DateTime.Now;
            _updated = DateTime.Now;
        }
        public RoutePath(int RoutePathId, int routeid, String path, int reason, String note, int createdByUserId, int updatedByUserId,
            DateTime created, DateTime updated)
        {
            this._routePathId = RoutePathId;
            this._routeId = routeid;
            this._geometry = path;
            this._note = note;
            this._createdByUserId = createdByUserId;
            this._reasonId = reason;
            this._created = created;
            this._updated = updated;
        }

        public RoutePath(int routeid, String path, int reason, String note, int createdByUserId, int updatedByUserId,
             DateTime created, DateTime updated)
        {
            this._routePathId = 0;
            this._routeId = routeid;
            this._geometry = path;
            this._note = note;
            this._createdByUserId = createdByUserId;
            this._reasonId = reason;
            this._created = created;
            this._updated = updated;
        }
        public RoutePath RoutePathMaker(NpgsqlDataReader reader)
        {
            RoutePathId = reader.GetInt32(0);
            RouteId = reader.GetInt32(1);
            ReasonId = reader.GetInt32(2);
            Note = reader.GetString(3);
            CreatedByUserId = reader.GetInt32(4);
            UpdatedByUserId = reader.GetInt32(5);
            Geomtry = reader.GetString(6);
            Created = reader.GetDateTime(7);
            Updated = reader.GetDateTime(8);
            return this;
        }

        public void NewPathTripAreaCommanHelper(RoutePath routePath, NpgsqlCommand command)
        {
            command.Parameters.AddWithValue("route_path_id", routePath.RoutePathId);
            command.Parameters.AddWithValue("route_id", routePath.RouteId);
            command.Parameters.AddWithValue("reason_id", routePath.ReasonId);
            command.Parameters.AddWithValue("note", routePath.Note);
            command.Parameters.AddWithValue("created_by_user_id", routePath.CreatedByUserId);
            command.Parameters.AddWithValue("updated_by_user_id", routePath.UpdatedByUserId);
            command.Parameters.AddWithValue("geo_path", routePath.Geomtry);
            command.Parameters.AddWithValue("created", routePath.Created);
            command.Parameters.AddWithValue("updated", routePath.Updated);
        }

        public String Note
        {
            get => _note;
            set => _note = value;
        }

        public int RoutePathId
        {
            get => _routePathId;
            set => _routePathId = value;
        }

        public int RouteId
        {
            get => _routeId;
            set => _routeId = value;
        }

        public String Geomtry
        {
            get => _geometry;
            set => _geometry = value;
        }

        public int ReasonId
        {
            get => _reasonId;
            set => _reasonId = value;
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

        public DateTime Created
        {
            get => _created;
            set => _created = value;
        }

        public DateTime Updated
        {
            get => _updated;
            set => _updated = value;
        }

    }
}
