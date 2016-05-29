using System;
using System.Collections.Generic;
using System.Linq;
using Pharmacy.DatabaseAccess.Classes;
using Pharmacy.DatabaseAccess.SqlHelpers;

namespace Pharmacy.BusinessLogic.Managers
{
    public class PatientManager
    {
        private static SqlExecuteManager _sqlManager;

        static PatientManager()
        {
            _sqlManager = new SqlExecuteManager();
        }

        #region GET
        public static Patient GetById(Guid patientId)
        {
            var query = SqlQueryGeneration.GetPatientById(patientId);
            var patient = _sqlManager.GetPatient(query);

            return patient.Count > 0 ? patient.First() : null;
        }

        public static List<Patient> GetByName(string patientName)
        {
            var query = SqlQueryGeneration.GetPatientByName(patientName);
            var patient = _sqlManager.GetPatient(query);

            return patient;
        }

        public static List<Patient> GetByPhone(string phoneNumber)
        {
            var query = SqlQueryGeneration.GetPatientByPhone(phoneNumber);
            var patient = _sqlManager.GetPatient(query);

            return patient;
        }

        public static Patient GetByRecipeId(Guid orderId)
        {
            var query = SqlQueryGeneration.GetPatientByRecipeId(orderId);
            var patient = _sqlManager.GetPatient(query);

            return patient.FirstOrDefault();
        }

        public static List<Patient> GetAll()
        {
            var query = SqlQueryGeneration.GetAllPatients();
            var patient = _sqlManager.GetPatient(query);

            return patient;
        }
        #endregion

        #region INSERT

        public static Guid Insert(Patient patient)
        {
            if (patient == null)
                throw new ArgumentException("Patient information was not provided");

            var query = SqlQueryGeneration.InsertPatient(patient);
            return _sqlManager.InsertPatient(query);
        }

        #endregion
    }
}
