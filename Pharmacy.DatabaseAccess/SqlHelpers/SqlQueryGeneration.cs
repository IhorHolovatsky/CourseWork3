using System;
using System.Collections.Generic;
using System.Linq;
using Pharmacy.DatabaseAccess.Classes;

namespace Pharmacy.DatabaseAccess.SqlHelpers
{
    public static class SqlQueryGeneration
    {
        #region Doctor
        public static string GetDoctorById(Guid doctorId)
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
            var doctorId = Guid.NewGuid();
            var query = string.Format(@"INSERT INTO Doctor(ID_Doctor, Surname, Doctor_Name, PName)
                          OUTPUT inserted.ID_Doctor
                          VALUES({0}, {1}, {2}, {3})", doctorId, doctor.LastName, doctor.FirstName, doctor.SecondaryName);

            return query;
        }

        public static string UpdateDoctor(Doctor doctor)
        {
            var fieldsToUpdate = new List<string>();

            if (!string.IsNullOrWhiteSpace(doctor.LastName))
            {
                fieldsToUpdate.Add(string.Format("Surname = {0}", doctor.LastName));
            }
            if (!string.IsNullOrWhiteSpace(doctor.FirstName))
            {
                fieldsToUpdate.Add(string.Format("Doctor_Name = {0}", doctor.FirstName));
            }
            if (!string.IsNullOrWhiteSpace(doctor.SecondaryName))
            {
                fieldsToUpdate.Add(string.Format("PName = {0}", doctor.SecondaryName));
            }

            if (!fieldsToUpdate.Any()) return string.Empty;

            var query = string.Format(
                @"UPDATE Doctor
                SET {0}
                WHERE ID_Doctor = {1}",
                string.Join(",", fieldsToUpdate.ToArray()),
                doctor.Id);

            return query;
        }

        #endregion

        #region Ingredient

        public static string GetIngredientById(Guid ingredientId)
        {
            var query = String.Format(
                          @"SELECT DISTINCT 
                          ID_Ingredient,
                          Title,
                          Quantity,
                          Reserved
                          FROM Ingredient
                          WHERE ID_Ingredient = '{0}'", ingredientId);

            return query;
        }

        public static string GetIngredientByName(string ingredientName)
        {
            var query = String.Format(
                         @"SELECT DISTINCT 
                          ID_Ingredient,
                          Title,
                          Quantity,
                          Reserved
                          FROM Ingredient
                          WHERE Title LIKE '{0}%'", ingredientName);

            return query;
        }

        public static string GetAllIngredients()
        {
            var query = @"SELECT DISTINCT 
                          ID_Ingredient,
                          Title,
                          Quantity,
                          Reserved
                          FROM Ingredient";

            return query;
        }

        public static string GetIngredientByMedicineIdId(Guid medicineId)
        {
            var query = string.Format(
                        @"SELECT 
                             Ingredient.ID_Ingredient,
                             Title,
                             Quantity,
                             Reserved 
                        FROM MedicineIngredient
                        INNER JOIN  Ingredient
                        ON Ingredient.ID_Ingredient = MedicineIngredient.ID_Ingredient
                        WHERE ID_Medicine = {0}", medicineId);

            return query;
        }

        public static string InsertIngredient(Ingredient ingredient)
        {
            var ingredientId = Guid.NewGuid();

            var query = string.Format(@"INSERT INTO Ingredient(ID_Ingredient, Title, Quantity, Reserved)
                               OUTPUT inserted.ID_Ingredient
                               VALUES ('{0}', '{1}', {2}, {3})", ingredientId, ingredient.Name, ingredient.Count, ingredient.ReservedCount);

            return query;
        }

        #endregion

        #region Medicine

        public static string GetMedicineById(Guid medicineId)
        {
            var query = String.Format(
                          @"SELECT DISTINCT 
                          ID_Medicine,
                          Medicine_Name,
                          Price,
                          Image,
                          Description,
                          ImageUrl
                          MedicineUse.ID_MedicineUse,
		                  MedicineUse.Type_Of,
		                  MedicineUse.Use_of
		                  FROM Medicine
                            INNER JOIN MedicineUse 
                            ON MedicineUse.ID_MedicineUse = Medicine.ID_MedicineUse
                          WHERE ID_Medicine = '{0}'", medicineId);

            return query;
        }

        public static string GetMedicineByName(string medicineName)
        {
            var query = String.Format(
                         @"SELECT DISTINCT 
                          ID_Medicine,
                          Medicine_Name,
                          Price,
                          Image,
                          Description,
                          ImageUrl
                          MedicineUse.ID_MedicineUse,
		                  MedicineUse.Type_Of,
		                  MedicineUse.Use_of
		                  FROM Medicine
                            INNER JOIN MedicineUse 
                            ON MedicineUse.ID_MedicineUse = Medicine.ID_MedicineUse
                          WHERE Medicine_Name LIKE '%{0}%'", medicineName);

            return query;
        }

        public static string GetAllMedicinesByIngredientId(Guid ingredientId)
        {
            var query = String.Format(@"SELECT * FROM MedicineIngredient
                                        INNER JOIN Medicine 
                                        ON MedicineIngredient.ID_Medicine = Medicine.ID_Medicine
                                        WHERE MedicineIngredient.ID_Ingredient = '{0}'
                                     ", ingredientId);

            return query;
        }

        public static string GetAllMedicines()
        {
            var query = @"SELECT DISTINCT 
                          ID_Medicine,
                          Medicine_Name,
                          Price,
                          Image,
                          Description,
                          ImageUrl
                          MedicineUse.ID_MedicineUse,
		                  MedicineUse.Type_Of,
		                  MedicineUse.Use_of
		                  FROM Medicine
                            INNER JOIN MedicineUse 
                            ON MedicineUse.ID_MedicineUse = Medicine.ID_MedicineUse";

            return query;
        }

        public static string InsertMedicine(Medicine medicine)
        {
            var medicineId = Guid.NewGuid();

            var query = string.Format(@"INSERT INTO Medicine(ID_Medicine, ID_MedicineUse, Price, Medicine_Name, Description, ImageUrl, Image)
                                      OUTPUT inserted.ID_Medicine                          
                                      VALUES ({0}, {1}, {2}, {3}, {4}, {5}, @Image)", medicineId, medicine.UseMethod.Id, medicine.Price, medicine.Name, medicine.Description, medicine.ImageUrl);

            return query;
        }

        public static string UpdateMedicine(Medicine medicine)
        {
            var fieldsToUpdate = new List<string>();

            if (!string.IsNullOrWhiteSpace(medicine.Name))
            {
                fieldsToUpdate.Add(string.Format("Medicine_Name = {0}", medicine.Name));
            }
            if (medicine.UseMethod.Id != Guid.Empty)
            {
                fieldsToUpdate.Add(string.Format("ID_MedicineUse = {0}", medicine.UseMethod.Id));
            }
            if (!string.IsNullOrWhiteSpace(medicine.Description))
            {
                fieldsToUpdate.Add(string.Format("Description = {0}", medicine.Description));
            }
            if (medicine.Price > default(decimal))
            {
                fieldsToUpdate.Add(string.Format("Price = {0}", medicine.Price));
            }
            if (!string.IsNullOrWhiteSpace(medicine.ImageUrl))
            {
                fieldsToUpdate.Add(string.Format("ImageUrl = {0}", medicine.ImageUrl));
            }

            if (!fieldsToUpdate.Any()) return string.Empty;

            var query = string.Format(
                @"UPDATE Medicine
                  SET {0}
                  WHERE ID_Medicine = {1}",
                string.Join(",", fieldsToUpdate.ToArray()),
                medicine.Id);

            return query;
        }

        #endregion

        #region UseMethod

        public static string GetUseMethodById(Guid useMethodId)
        {
            var query = String.Format(
                          @"SELECT DISTINCT 
                          ID_MedicineUse,
                          Type_Of,
                          Use_of
                          FROM MedicineUse
                          WHERE ID_MedicineUse = '{0}'", useMethodId);

            return query;
        }

        public static string GetUseMethodByName(string medicineUseName)
        {
            var query = String.Format(
                         @"SELECT DISTINCT 
                          ID_MedicineUse,
                          Type_Of,
                          Use_of
                          FROM MedicineUse
                          WHERE Title LIKE '%{0}%'", medicineUseName);

            return query;
        }

        public static string GetAllUseMethods()
        {
            var query = @"SELECT DISTINCT 
                          ID_MedicineUse,
                          Type_Of,
                          Use_of
                          FROM MedicineUse";

            return query;
        }

        #endregion

        #region Patient

        public static string GetPatientById(Guid patientId)
        {
            var query = string.Format(
                                    @"SELECT DISTINCT *
                                    FROM Patient
                                    WHERE ID_Patient = '{0}'", patientId);

            return query;
        }

        public static string GetPatientByName(string patientName)
        {
            var query = string.Format(
                         @"SELECT DISTINCT *
                           FROM Patient
                           WHERE Patient_Name LIKE '%{0}%'
                           OR Surname LIKE '%{0}%'
                           OR PName LIKE '%{0}%'", patientName);

            return query;
        }

        public static string GetPatientByPhone(string phoneNumber)
        {
            var query = string.Format(
                         @"SELECT DISTINCT *
                           FROM Patient
                           WHERE PhoneNumber LIKE '%{0}%'", phoneNumber);

            return query;
        }


        public static string GetPatientByOrderId(Guid orderId)
        {
            var query = string.Format(
                         @"SELECT * 
                           FROM Patient 
                           INNER JOIN OrderTable
                           ON Patient.ID_Patient = OrderTable.ID_Patient
                           WHERE ID_OrderTable = '{0}'", orderId);

            return query;
        }

        public static string GetAllPatients()
        {
            var query = @"SELECT * 
                          FROM Patient";

            return query;
        }


        public static string InsertPatient(Patient patient)
        {
            var entityId = Guid.NewGuid();

            var query = string.Format(
                        @"INSERT INTO Patient(ID_Patient, Surname, Patient_Name, PName, Date_Of_Birth, PhoneNumber, Address)
                          OUTPUT inserted.ID_Patient
                          VALUES ('{0}','{1}','{2}','{3}','{4}','{5}', '{6}')", entityId, patient.LastName, patient.FirstName, patient.SecondaryName, patient.DateOfBirth, patient.PhoneNumber, patient.Address);

            return query;
        }

        #endregion

    }
}
