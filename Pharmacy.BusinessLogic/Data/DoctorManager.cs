using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pharmacy.BusinessLogic.SqlHelpers;
using Pharmacy.Objects.Classes;

namespace Pharmacy.BusinessLogic.Data
{
    public static class DoctorManager
    {
        private static SqlExecuteManager _sqlManager;

        static DoctorManager()
        {
            _sqlManager = new SqlExecuteManager();
        }

        #region GET
        public static Doctor GetDoctorById(int doctorId)
        {
            var query = SqlQueryGeneration.GetDoctorById(doctorId);
            var doctors = _sqlManager.GetDoctor(query);

            return doctors.Count > 0 ? doctors.First() : null;
        }

        public static Doctor GetDoctorByName(string doctorName)
        {
            var query = SqlQueryGeneration.GetDoctorByName(doctorName);
            var doctors = _sqlManager.GetDoctor(query);

            return doctors.Count > 0 ? doctors.First() : null; ;
        }

        public static List<Doctor> GetAllDoctors()
        {
            var query = SqlQueryGeneration.GetAllDoctors();

            return _sqlManager.GetDoctor(query);
        }
        #endregion

        #region INSERT

        public static void Insert(Doctor doctor)
        {
            if (doctor == null)
                throw new ArgumentException("doctor information was not provided");

            var query = SqlQueryGeneration.InsertDoctor(doctor);

            try
            {
                _sqlManager.InsertDoctor(query);
            }
            catch (SqlException e)
            {
                //ToDo: Log Exception
                throw new Exception("Failed to insert Doctor", e);
            }
        }

        #endregion

        #region UPDATE

        public static Doctor Update(Doctor doctor)
        {
            if (doctor == null)
                throw new ArgumentException("doctor information was not provided");

            var query = SqlQueryGeneration.InsertDoctor(doctor);

            return string.IsNullOrEmpty(query) ? doctor : _sqlManager.UpdateDoctor(query);
        }

        #endregion
    }
}
