using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.BusinessLogic.Data
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
