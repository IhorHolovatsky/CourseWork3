using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Pharmacy.DatabaseAccess.Classes;
using Pharmacy.DatabaseAccess.SqlHelpers;

namespace Pharmacy.BusinessLogic.Managers
{
    public static class DoctorManager
    {
        private static SqlExecuteManager _sqlManager;

        static DoctorManager()
        {
            _sqlManager = new SqlExecuteManager();
        }

        #region GET
        public static Doctor GetDoctorById(Guid doctorId)
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

        public static Doctor GetDoctorByRecipeId(Guid recipeId)
        {
            var query = SqlQueryGeneration.GetDoctorByRecipeId(recipeId);
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

        public static Guid Insert(Doctor doctor)
        {
            if (doctor == null)
                throw new ArgumentException("doctor information was not provided");

            var query = SqlQueryGeneration.InsertDoctor(doctor);

            try
            {
               return _sqlManager.InsertDoctor(query);
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
