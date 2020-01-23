using Npgsql;

namespace MatakDBConnector
{
    public class User
    {
        private int _userId;
        private string _password;
        private int _phoneId;
        private string _lastName;
        private string _firstName;
        private int _permissionId;
        private int _orgId;
        private string _email;
        private string _nickname;

        public User()
        {
            _userId = 0;
            _password = "0";
            _phoneId = 0;
            _lastName = "0";
            _firstName = "0";
            _permissionId = 0;
            _orgId = 0;
            _email = "0";
            _nickname = "0";
        }

        public User(int userId, string password, int phoneId, string lastName, string firstName, int permissionId, int orgId, string email, string nickname)
        {
            _userId = userId;
            _password = password;
            _phoneId = phoneId;
            _lastName = lastName;
            _firstName = firstName;
            _permissionId = permissionId;
            _orgId = orgId;
            _email = email;
            _nickname = nickname;
        }
        
        public User UserMaker(NpgsqlDataReader reader)
        {
            User user = new User();
            
            user.UserId = reader.GetInt32(0);
            user.Password = reader.GetString(1);
            user.PhoneId = reader.GetInt32(2);
            user.LastName = reader.GetString(3);
            user.FirstName = reader.GetString(4);
            user.PermissionId = reader.GetInt32(5);
            user.OrgId = reader.GetInt32(6);
            user.Email = reader.GetString(7);
            user.Nickname = reader.GetString(8);
                
            return user;
        }

        public int UserId
        {
            get => _userId;
            set => _userId = value;
        }

        public string Password
        {
            get => _password;
            set => _password = value;
        }

        public int PhoneId
        {
            get => _phoneId;
            set => _phoneId = value;
        }

        public string LastName
        {
            get => _lastName;
            set => _lastName = value;
        }

        public string FirstName
        {
            get => _firstName;
            set => _firstName = value;
        }

        public int PermissionId
        {
            get => _permissionId;
            set => _permissionId = value;
        }

        public int OrgId
        {
            get => _orgId;
            set => _orgId = value;
        }

        public string Email
        {
            get => _email;
            set => _email = value;
        }

        public string Nickname
        {
            get => _nickname;
            set => _nickname = value;
        }
    }
}