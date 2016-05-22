using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using Pharmacy.DatabaseAccess.Classes;
using Pharmacy.DatabaseAccess.Utils;

namespace Pharmacy.DatabaseAccess.SqlHelpers
{
    public class SqlExecuteManager
    {
        private readonly string _connectionString =
            ConfigurationManager.ConnectionStrings["Pharmacy.main"].ConnectionString;

        #region Doctor
        public List<Doctor> GetDoctor(string query)
        {
            var doctors = (from row in RunQuery(query, null).AsEnumerable()
                           select new Doctor(row.Get<Guid>("ID_Doctor"))
                           {
                               FirstName = row.Get<string>("Doctor_Name"),
                               LastName = row.Get<string>("Surname"),
                               SecondaryName = row.Get<string>("PName")
                           }).ToList();

            return doctors;
        }

        public Guid InsertDoctor(string query)
        {
            return RunScalarQuery(query, null);
        }

        public Doctor UpdateDoctor(string query)
        {
            var doctors = (from row in RunQuery(query, null).AsEnumerable()
                           select new Doctor(row.Get<Guid>("ID_Doctor"))
                           {
                               FirstName = row.Get<string>("Doctor_Name"),
                               LastName = row.Get<string>("Surname"),
                               SecondaryName = row.Get<string>("PName")
                           }).ToList();

            return doctors.Count > 0 ? doctors.First() : null;
        }

        #endregion

        #region Ingredient

        public List<Ingredient> GetIngredient(string query)
        {
            var ingredients = (from row in RunQuery(query, null).AsEnumerable()
                               select new Ingredient(row.Get<Guid>("ID_Ingredient"))
                               {
                                   Name = row.Get<string>("Title"),
                                   Count = row.Get<int>("Quantity"),
                                   ReservedCount = row.Get<int>("Reserved")
                               }).ToList();

            return ingredients;
        }

        public Guid InsertIngredient(string query)
        {
            var objectId = RunScalarQuery(query, null);

            return objectId;
        }
        #endregion

        #region Medicine

        public List<Medicine> GetMedicine(string query)
        {
            var medicine = (from row in RunQuery(query, null).AsEnumerable()
                            select new Medicine(row.Get<Guid>("ID_Medicine"))
                            {
                                Name = row.Get<string>("Medicine_Name"),
                                Description = row.Get<string>("Description"),
                                Price = row.Get<decimal>("Price"),
                                Image = Image.FromStream(new MemoryStream(row.Get<byte[]>("Image"))),
                                ImageUrl = row.Get<string>("ImageUrl"),
                                Type = new MedicineType(row.Get<Guid>("ID_MedicineType"))
                                {
                                    TypeOf = row.Get<string>("Type_Of"),
                                }
                            }).ToList();

            return medicine;
        }

        public Guid InsertMedicine(List<string> queries, Image image)
        {
            var stream = new MemoryStream();
            image.Save(stream, System.Drawing.Imaging.ImageFormat.Gif);
            var objectId = RunScalarQuery(queries[0], new Dictionary<string, object> { { "@Image", stream.ToArray() } });

            //Insert relationships
            RunNonQuery(string.Format(queries[1], objectId), null);

            return objectId;
        }

        public Medicine UpdateMedicine(List<string> query, Image image)
        {
            var stream = new MemoryStream();
            image.Save(stream, System.Drawing.Imaging.ImageFormat.Gif);

            var medicines = (from row in RunQuery(string.Join("\n", query), new Dictionary<string, object> { { "@Image", stream.ToArray() } }).AsEnumerable()
                             select new Medicine(row.Get<Guid>("ID_Medicine"))
                             {
                                 Name = row.Get<string>("Medicine_Name"),
                                 Description = row.Get<string>("Description"),
                                 Price = row.Get<decimal>("Price"),
                                 Image = Image.FromStream(new MemoryStream(row.Get<byte[]>("Image"))),
                                 ImageUrl = row.Get<string>("ImageUrl"),
                                 Type = new MedicineType(row.Get<Guid>("ID_MedicineType"))
                                 {
                                     TypeOf = row.Get<string>("Type_Of"),
                                 }
                             }).ToList();

            
            return medicines.Count > 0 ? medicines.First() : null;
        }

        public bool DeleteMedicine(List<string> query)
        {
            return RunNonQuery(string.Join("\n", query), null);
        }
        #endregion
        
        #region UseMethod

        public List<MedicineType> GetUseMethod(string query)
        {
            var MedicineType = (from row in RunQuery(query, null).AsEnumerable()
                               select new MedicineType(row.Get<Guid>("ID_MedicineType"))
                               {
                                   TypeOf = row.Get<string>("Type_Of"),
                               }).ToList();

            return MedicineType;
        }

        #endregion

        #region Patient

        public List<Patient> GetPatient(string query)
        {
            var patient = (from row in RunQuery(query, null).AsEnumerable()
                               select new Patient(row.Get<Guid>("ID_Patient"))
                               {
                                   LastName = row.Get<string>("Surname"),
                                   FirstName = row.Get<string>("Patient_Name"),
                                   SecondaryName = row.Get<string>("PName"),
                                   DateOfBirth = row.Get<DateTime>("Date_Of_Birth"),
                                   PhoneNumber = row.Get<string>("PhoneNumber"),
                                   Address = row.Get<string>("Address")
                               }).ToList();

            return patient;
        }

        public Guid InsertPatient(string query)
        {
            var objectId = RunScalarQuery(query, null);
            return objectId;
        }

        #endregion


        private DataTable RunQuery(string query, Dictionary<string, object> parameters)
        {
            SqlConnectionHolder holder = null;
            try
            {
                holder = SqlConnectionHelper.GetConnection(_connectionString);
                using (SqlCommand sqlCmd = new SqlCommand(query, holder.Connection) { CommandType = CommandType.Text })
                {
                    using (var adapter = new SqlDataAdapter { SelectCommand = sqlCmd })
                    {
                        if (parameters != null)
                        {
                            foreach (var param in parameters)
                            {
                                var parameter = new SqlParameter(param.Key, param.Value);
                                sqlCmd.Parameters.Add(parameter);
                            }
                        }

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


        private bool RunNonQuery(string query, Dictionary<string, object> parameters)
        {
            SqlConnectionHolder holder = null;
            try
            {
                holder = SqlConnectionHelper.GetConnection(_connectionString);
                using (SqlCommand sqlCmd = new SqlCommand(query, holder.Connection) { CommandType = CommandType.Text })
                {
                    if (parameters != null)
                    {
                        foreach (var param in parameters)
                        {
                            var parameter = new SqlParameter(param.Key, param.Value);
                            sqlCmd.Parameters.Add(parameter);
                        }
                    }


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

        private Guid RunScalarQuery(string query, Dictionary<string, object> parameters)
        {
            SqlConnectionHolder holder = null;
            try
            {
                holder = SqlConnectionHelper.GetConnection(_connectionString);
                using (SqlCommand sqlCmd = new SqlCommand(query, holder.Connection) { CommandType = CommandType.Text })
                {
                    if (parameters != null)
                    {
                        foreach (var param in parameters)
                        {
                            var parameter = new SqlParameter(param.Key, param.Value);
                            sqlCmd.Parameters.Add(parameter);
                        }
                    }

                    var result = sqlCmd.ExecuteScalar();

                    return Guid.Parse((string)result);
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
    }
}
