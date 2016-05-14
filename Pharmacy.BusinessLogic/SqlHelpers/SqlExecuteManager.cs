using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pharmacy.BusinessLogic.Utils;
using Pharmacy.Objects.Classes;
using Pharmacy.Objects.Interfaces;


namespace Pharmacy.BusinessLogic.Data
{
    internal class SqlExecuteManager
    {
        private readonly string _connectionString =
            ConfigurationManager.ConnectionStrings["Pharmacy.main"].ConnectionString;

        public List<Doctor> GetDoctor(string query)
        {
            var doctors = (from row in RunQuery(query).AsEnumerable()
                                  select new Doctor(row.Get<Guid>("ID_Doctor"))
                                  {
                                    FirstName = row.Get<string>("Doctor_Name"),
                                    LastName = row.Get<string>("Surname"),
                                    SecondaryName = row.Get<string>("PName")
                                  }).ToList();

            return doctors;
        }


        private DataTable RunQuery(string query)
        {
            SqlConnectionHolder holder = null;
            try
            {
                holder = SqlConnectionHelper.GetConnection(_connectionString);
                using (SqlCommand sqlCmd = new SqlCommand(query, holder.Connection) {CommandType = CommandType.Text})
                {
                    using (var adapter = new SqlDataAdapter {SelectCommand = sqlCmd})
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        return dataTable;
                    }
                }
            }
            finally
            {
                if (holder != null)
                {
                    holder.Close();
                }
            }
        }

        private bool RunNonQuery(string query)
        {
            SqlConnectionHolder holder = null;
            try
            {
                holder = SqlConnectionHelper.GetConnection(_connectionString);
                using (SqlCommand sqlCmd = new SqlCommand(query, holder.Connection) {CommandType = CommandType.Text})
                {
                    sqlCmd.ExecuteNonQuery();
                }
                return true;
            }
            finally
            {
                if (holder != null)
                {
                    holder.Close();
                }
            }
        }
    }
}
