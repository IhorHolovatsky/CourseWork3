using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pharmacy.BusinessLogic.SqlHelpers;
using Pharmacy.Objects.Classes;

namespace Pharmacy.BusinessLogic.Data
{
    public class PatientManager
    {
        private static SqlExecuteManager _sqlManager;

        static PatientManager()
        {
            _sqlManager = new SqlExecuteManager();
        }

        #region GET
        public static Patient GetPatientById(Guid patientId)
        {
            var query = SqlQueryGeneration.GetPatientById(patientId);
            var patient = _sqlManager.GetPatient(query);

            return patient.Count > 0 ? patient.First() : null;
        }

        public static Patient GetPatientByName(string patientName)
        {
            var query = SqlQueryGeneration.GetPatientByName(patientName);
            var patient = _sqlManager.GetPatient(query);

            return patient.Count > 0 ? patient.First() : null;
        }

        public static List<Patient> GetAll()
        {
            var query = SqlQueryGeneration.GetAllPatients();
            var patient = _sqlManager.GetPatient(query);

            return patient;
        }
        #endregion
    }
}
