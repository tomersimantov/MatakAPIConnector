using System;
using Npgsql;

namespace MatakDBConnector
{
    public class Document
    {
        private int _documentId;
        private int _routeId;
        private string _filename;
        private DateTime _created;
        private DateTime _updated;
        private int _createdByUserId;
        private int _updatedByUserId;
        private Boolean _isLandmark;

        public Document()
        {
            _documentId = 0;
            _routeId = 0;
            _filename = "";
            _created = DateTime.Now;
            _updated = DateTime.Now;
            _createdByUserId = 0;
            _updatedByUserId = 0;
            _isLandmark = false;
        }

        public Document(int documentId, int routeId, string filename, DateTime created, DateTime updated, int createdByUserId, int updatedByUserId, Boolean isLandmark)
        {
            _documentId = documentId;
            _routeId = routeId;
            _filename = filename;
            _created = created;
            _updated = updated;
            _createdByUserId = createdByUserId;
            _updatedByUserId = updatedByUserId;
            _isLandmark = isLandmark;
        }

        public Document(int routeId, string filename, DateTime created, DateTime updated, int createdByUserId, int updatedByUserId, Boolean isLandmark)
        {
            _routeId = routeId;
            _filename = filename;
            _created = created;
            _updated = updated;
            _createdByUserId = createdByUserId;
            _updatedByUserId = updatedByUserId;
            _isLandmark = isLandmark;
        }
        
        public Document DocumentMaker(NpgsqlDataReader reader)
        {
            DocumentId = reader.GetInt32(0);
            RouteId = reader.GetInt32(1);
            Filename = reader.GetString(2);
            Created = reader.GetDateTime(3);
            Updated = reader.GetDateTime(4);;
            CreatedByUserId = reader.GetInt32(5);
            UpdatedByUserId = reader.GetInt32(6);
            IsLandmark = reader.GetBoolean(7);
            
            return this;
        }
        
        protected void newDocumentCommandHelper(Document document, NpgsqlCommand command, Boolean isNew)
        {
            command.Parameters.AddWithValue("route_id", document.RouteId);
            command.Parameters.AddWithValue("filename", document.Filename);
            command.Parameters.AddWithValue("created", document.Created);
            command.Parameters.AddWithValue("updated", document.Updated);
            command.Parameters.AddWithValue("is_landmark", document.IsLandmark);
            command.Parameters.AddWithValue("created_by_user_id", document.CreatedByUserId);
            if (isNew)
            {
                command.Parameters.AddWithValue("updated_by_user_id", document.CreatedByUserId);
                command.Parameters.AddWithValue("created", DateTime.Now);
            }
            else
            {
                command.Parameters.AddWithValue("updated_by_user_id", document.UpdatedByUserId);
                command.Parameters.AddWithValue("updated", DateTime.Now);
            }
        }

        public int DocumentId
        {
            get => _documentId;
            set => _documentId = value;
        }

        public int RouteId
        {
            get => _routeId;
            set => _routeId = value;
        }

        public string Filename
        {
            get => _filename;
            set => _filename = value;
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

        public bool IsLandmark
        {
            get => _isLandmark;
            set => _isLandmark = value;
        }
    }
}