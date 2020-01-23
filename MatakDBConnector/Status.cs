using Npgsql;

namespace MatakDBConnector
{
    public class Status
    {
        private int _id;
        private string _description;
        private string _color;

        public Status()
        {
            _id = 0;
            _description = "0";
            _color = "0";
        }

        public Status(int id, string description, string color)
        {
            _id = id;
            _description = description;
            _color = color;
        }
        
        public Status StatusMaker(NpgsqlDataReader reader)
        {
            Status status = new Status();
            
            status.Id = reader.GetInt32(0);
            status.Description = reader.GetString(1);
            status.Color = reader.GetString(2);
                
            return status;
        }

        public int Id
        {
            get => _id;
            set => _id = value;
        }

        public string Description
        {
            get => _description;
            set => _description = value;
        }

        public string Color
        {
            get => _color;
            set => _color = value;
        }
    }
}