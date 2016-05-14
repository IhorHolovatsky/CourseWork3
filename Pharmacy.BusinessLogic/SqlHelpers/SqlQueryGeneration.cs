using System;
using Pharmacy.Objects.Classes;

namespace Pharmacy.BusinessLogic.SqlHelpers
{
    internal static class SqlQueryGeneration
    {
        public static string GetDoctorById(int doctorId)
        {
            var query = String.Format(
                          @"SELECT DISTINCT 
                          Surname,
                          Doctor_Name,
                          PName
                          FROM Doctor
                          WHERE ID_Doctor = '{0}'", doctorId);

            return query;
        }

        public static string GetDoctorByName(string doctorName)
        {
            var query = String.Format(
                         @"SELECT DISTINCT 
                          Surname,
                          Doctor_Name,
                          PName
                          FROM Doctor
                          WHERE Doctor_Name LIKE '%{0}%'
                          OR Surname LIKE '%{0}%'", doctorName);

            return query;
        }

        public static string GetAllDoctors()
        {
            var query = @"SELECT DISTINCT 
                          Surname,
                          Doctor_Name,
                          PName
                          FROM Doctor";

            return query;
        }

        public static string InsertDoctor(Doctor doctor)
        {
            var query = string.Format(@"INSERT INTO Doctor(ID_Doctor, Surname, Doctor_Name, PName)
                          VALUES({0}, {1}, {2}, {3})", Guid.NewGuid(),doctor.LastName, doctor.FirstName, doctor.SecondaryName);

            return query;
        }
    }
}
