using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pharmacy.BusinessLogic.SqlHelpers;
using Pharmacy.Objects.Classes;

namespace Pharmacy.BusinessLogic.Data
{
    static class DoctorManager
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
            
            return doctors.Count > 0 ? doctors.First() : null;;
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


        }

        #endregion
        
    }
}
