using System;
using System.Data.SqlClient;

namespace Pharmacy.DatabaseAccess.SqlHelpers
{
    public sealed class SqlConnectionHolder
    {
        private readonly SqlConnection _connection;
        private bool _isOpened;

        internal SqlConnection Connection
        {
            get { return _connection; }
        }

        internal SqlConnectionHolder(string connectionString)
        {
            try
            {
                _connection = new SqlConnection(connectionString);
            }
            catch (ArgumentException e)
            {
                throw new ArgumentException("Invalid connection string", "connectionString", e);
            }
        }

        internal void Open()
        {
            if (_isOpened) return;

            Connection.Open();

           _isOpened = true;
        }

        internal void Close()
        {
            if (!_isOpened) return;
            Connection.Close();
            _isOpened = false;
        }
    }
}
