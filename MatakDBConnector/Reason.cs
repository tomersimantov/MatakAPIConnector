using Npgsql;

namespace MatakDBConnector
{
    public class Reason
    {
        private int _reason_id;
        private string _description;

        public Reason()
        {
            _reason_id = -1;
            _description = "Empty";
        }

        public Reason(int reasonId, string description)
        {
            _reason_id = reasonId;
            _description = description;
        }
        
        public Reason ReasonMaker(NpgsqlDataReader reader)
        {
            ReasonId = reader.GetInt32(0);
            Description = reader.GetString(1);
                
            return this;
        }

        public int ReasonId
        {
            get => _reason_id;
            set => _reason_id = value;
        }

        public string Description
        {
            get => _description;
            set => _description = value;
        }
    }
}