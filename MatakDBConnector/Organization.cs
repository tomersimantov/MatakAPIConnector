using Npgsql;

namespace MatakDBConnector
{
    public class Organization
    {
        private int _orgId;
        private string _name;
        private int _mainUserId;
        private int _countryId;
        private int _addressId;
        private int _faxId;
        private int _phoneId;

        public Organization()
        {
            _orgId = 0;
            _name = "0";
            _mainUserId = 0;
            _countryId = 0;
            _addressId = 0;
            _faxId = 0;
            _phoneId = 0;
        }

        public Organization(int orgId, string name, int mainUserId, int countryId, int addressId, int faxId, int phoneId)
        {
            _orgId = orgId;
            _name = name;
            _mainUserId = mainUserId;
            _countryId = countryId;
            _addressId = addressId;
            _faxId = faxId;
            _phoneId = phoneId;
        }
        
        public Organization OrganizationMaker(NpgsqlDataReader reader)
        {
            OrgId = reader.GetInt32(0);
            Name = reader.GetString(1);
            MainUserId = reader.GetInt32(2);
            CountryId = reader.GetInt32(3);
            AddressId = reader.GetInt32(4);
            FaxId = reader.GetInt32(5);
            PhoneId = reader.GetInt32(8);
                
            return this;
        }

        public int OrgId
        {
            get => _orgId;
            set => _orgId = value;
        }

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public int MainUserId
        {
            get => _mainUserId;
            set => _mainUserId = value;
        }

        public int CountryId
        {
            get => _countryId;
            set => _countryId = value;
        }

        public int AddressId
        {
            get => _addressId;
            set => _addressId = value;
        }

        public int FaxId
        {
            get => _faxId;
            set => _faxId = value;
        }

        public int PhoneId
        {
            get => _phoneId;
            set => _phoneId = value;
        }
    }
}