using System;
using Npgsql;

namespace MatakDBConnector
{
    public class EscortOrg
    {
        private int _orgId;
        private int _routeId;

        public EscortOrg()
        {
            _orgId = 0;
            _routeId = 0;
        }

        public EscortOrg(int orgId, int routeId)
        {
            _orgId = orgId;
            _routeId = routeId;
        }
        
        protected void newEscortOrgCommandHelper(EscortOrg escortOrg, NpgsqlCommand command)
        {
            command.Parameters.AddWithValue("org_id", escortOrg.OrgId);
            command.Parameters.AddWithValue("route_id", escortOrg.RouteId);
            command.Parameters.AddWithValue("created", DateTime.Now);
            command.Parameters.AddWithValue("updated", DateTime.Now);
        }

        public EscortOrg EscortOrgMaker(NpgsqlDataReader reader)
        {
            OrgId = reader.GetInt32(0);
            RouteId = reader.GetInt32(1);
                
            return this;
        }

        public int OrgId
        {
            get => _orgId;
            set => _orgId = value;
        }

        public int RouteId
        {
            get => _routeId;
            set => _routeId = value;
        }
    }
}