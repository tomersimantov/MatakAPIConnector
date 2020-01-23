using System;
using Npgsql;

namespace MatakDBConnector
{
    public class RefreshToken
    {
        //TODO method will send refreshToken and I will return true or false whether token is found AND datetime.now <= exp_date
        private int _refreshTokenId;
        private int _userId;
        private string _refreshTokenHash;
        private DateTime _expDate;

        public RefreshToken()
        {
            _refreshTokenId = 0;
            _userId = 0;
            _refreshTokenHash = "0";
            _expDate = DateTime.Now;
        }

        public RefreshToken(int userId, string refreshTokenHash, DateTime expDate)
        {
            _refreshTokenId = 0;
            _userId = userId;
            _refreshTokenHash = refreshTokenHash;
            _expDate = expDate;
        }

        public RefreshToken(int refreshTokenId, int userId, string refreshTokenHash, DateTime expDate)
        {
            _refreshTokenId = refreshTokenId;
            _userId = userId;
            _refreshTokenHash = refreshTokenHash;
            _expDate = expDate;
        }

        public RefreshToken RefreshTokenMaker(NpgsqlDataReader reader)
        {
            RefreshTokenId = reader.GetInt32(0);
            UserId = reader.GetInt32(1);
            RefreshTokenHash = reader.GetString(2);
            ExpDate = reader.GetDateTime(3);
                
            return this;
        }
        
        protected void newRefreshTokenCommandHelper(RefreshToken refreshToken, NpgsqlCommand command) 
        {
            command.Parameters.AddWithValue("user_id", refreshToken.UserId);
            command.Parameters.AddWithValue("refresh_token_hash", refreshToken.RefreshTokenHash);
            command.Parameters.AddWithValue("exp_date", refreshToken.ExpDate);
        }

        public int RefreshTokenId
        {
            get => _refreshTokenId;
            set => _refreshTokenId = value;
        }

        public int UserId
        {
            get => _userId;
            set => _userId = value;
        }

        public string RefreshTokenHash
        {
            get => _refreshTokenHash;
            set => _refreshTokenHash = value;
        }

        public DateTime ExpDate
        {
            get => _expDate;
            set => _expDate = value;
        }
    }
}