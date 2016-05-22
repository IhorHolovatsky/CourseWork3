namespace Pharmacy.DatabaseAccess.SqlHelpers
{
    class SqlConnectionHelper
    {
        internal static SqlConnectionHolder GetConnection(string connectionString)
        {
            var holder = new SqlConnectionHolder(connectionString);
            bool closeConn = true;

            try
            {
                holder.Open();
                closeConn = false;
            }
            finally
            {
                if (closeConn)
                {
                    holder.Close();
                    holder = null;
                }
            }
            return holder;
        }
    }
}
