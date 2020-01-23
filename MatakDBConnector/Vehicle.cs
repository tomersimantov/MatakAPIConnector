using Npgsql;

namespace MatakDBConnector
{
    public class Vehicle
    {
        private int _vehicleId;
        private string _plateNumber;
        private int _orgId;
        private int _photoId;
        private int _typeId;
        private string _model;
        private string _color;
        private string _manufacturer;

        public Vehicle()
        {
            _vehicleId = 0;
            _plateNumber = "0";
            _orgId = 0;
            _photoId = 0;
            _typeId = 0;
            _model = "0";
            _color = "0";
            _manufacturer = "0";
        }

        public Vehicle(int vehicleId, string plateNumber, int orgId, int photoId, int typeId, string model, string color, string manufacturer)
        {
            _vehicleId = vehicleId;
            _plateNumber = plateNumber;
            _orgId = orgId;
            _photoId = photoId;
            _typeId = typeId;
            _model = model;
            _color = color;
            _manufacturer = manufacturer;
        }

        public Vehicle VehicleMaker(NpgsqlDataReader reader)
        {
            Vehicle vehicle = new Vehicle();
            
            vehicle.VehicleId = reader.GetInt32(0);
            vehicle.PlateNumber = reader.GetString(1);
            vehicle.OrgId = reader.GetInt32(2);
            vehicle.PhotoId = reader.GetInt32(3);
            vehicle.TypeId = reader.GetInt32(4);
            vehicle.Model = reader.GetString(5);
            vehicle.Color = reader.GetString(6);
            vehicle.Manufacturer = reader.GetString(7);
                
            return vehicle;
        }

        public int VehicleId
        {
            get => _vehicleId;
            set => _vehicleId = value;
        }

        public string PlateNumber
        {
            get => _plateNumber;
            set => _plateNumber = value;
        }

        public int OrgId
        {
            get => _orgId;
            set => _orgId = value;
        }

        public int PhotoId
        {
            get => _photoId;
            set => _photoId = value;
        }

        public int TypeId
        {
            get => _typeId;
            set => _typeId = value;
        }

        public string Model
        {
            get => _model;
            set => _model = value;
        }

        public string Color
        {
            get => _color;
            set => _color = value;
        }

        public string Manufacturer
        {
            get => _manufacturer;
            set => _manufacturer = value;
        }
    }
}