using System;
using System.Collections.Generic;
using System.Linq;
using Pharmacy.Objects.Classes;

namespace Pharmacy.BusinessLogic.SqlHelpers
{
    internal static class SqlQueryGeneration
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
                          Title,
                          Quantity,
                          Reserved
                          FROM Ingredient
                          WHERE Title LIKE '%{0}%'", ingredientName);

            return query;
        }

        public static string GetAllIngredients()
        {
            var query = @"SELECT DISTINCT 
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


    }
}
