using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                SET '{0}'
                WHERE ID_Doctor = '{1}'",
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
                        WHERE ID_Medicine = '{0}'", medicineId);

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
                          @"SELECT *
		                  FROM Medicine
                            INNER JOIN MedicineType 
                            ON MedicineType.ID_MedicineType = Medicine.ID_MedicineType
                          WHERE ID_Medicine = '{0}'", medicineId);

            return query;
        }

        public static string GetMedicineByName(string medicineName)
        {
            var query = String.Format(
                         @"SELECT *
		                  FROM Medicine
                            INNER JOIN MedicineType 
                            ON MedicineType.ID_MedicineType = Medicine.ID_MedicineType
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
            var query = @"SELECT *
		                  FROM Medicine
                            INNER JOIN MedicineType 
                            ON MedicineType.ID_MedicineType = Medicine.ID_MedicineType";

            return query;
        }

        public static List<string> InsertMedicine(Medicine medicine)
        {
            var medicineId = Guid.NewGuid();

            var medicineQuery = string.Format(@"INSERT INTO Medicine(ID_Medicine, ID_MedicineType, Price, Medicine_Name, Description, ImageUrl, Image)
                                      OUTPUT inserted.ID_Medicine                          
                                      VALUES ('{0}', '{1}', {2}, '{3}', '{4}', '{5}', @Image)", medicineId, medicine.Type.Id, medicine.Price, medicine.Name.Replace("'", "''"), medicine.Description.Replace("'", "''"), medicine.ImageUrl);

            var medicineIngredientsQuery = @"INSERT INTO MedicineIngredient(ID_MedicineIngredient, ID_Medicine, ID_Ingredient)
                                                           VALUES";

            var values = (from ingredient in medicine.Ingredients
                          let entityId = Guid.NewGuid()
                          select string.Format("('{0}', '{1}', '{2}')", entityId, medicineId, ingredient.Id))
                          .ToList();

            medicineIngredientsQuery += string.Join(",", values);


            return new List<string> { medicineQuery, medicineIngredientsQuery };
        }

        public static List<string> UpdateMedicine(Medicine medicine)
        {
            var fieldsToUpdate = new List<string>();
            var query = string.Empty;
            var medicineIngredientsDeleteQuery = string.Empty;
            var medicineIngredientsQuery = string.Empty;

            if (!string.IsNullOrWhiteSpace(medicine.Name))
            {
                fieldsToUpdate.Add(string.Format("Medicine_Name = '{0}'", medicine.Name));
            }
            if (medicine.Type.Id != Guid.Empty)
            {
                fieldsToUpdate.Add(string.Format("ID_MedicineType = '{0}'", medicine.Type.Id));
            }
            if (medicine.Image != null)
            {
                fieldsToUpdate.Add("Image = @Image");
            }
            if (!string.IsNullOrWhiteSpace(medicine.Description))
            {
                fieldsToUpdate.Add(string.Format("Description = '{0}'", medicine.Description));
            }
            if (medicine.Price > default(decimal))
            {
                fieldsToUpdate.Add(string.Format("Price = {0}", medicine.Price));
            }
            if (!string.IsNullOrWhiteSpace(medicine.ImageUrl))
            {
                fieldsToUpdate.Add(string.Format("ImageUrl = '{0}'", medicine.ImageUrl));
            }

            if (fieldsToUpdate.Any())
            {
                query = string.Format(
                @"UPDATE Medicine
                  SET {0}
                  OUTPUT inserted.Description, inserted.Medicine_Name, inserted.ID_Medicine, inserted.ID_MedicineType, inserted.Image, inserted.Price, inserted.ImageUrl
                  WHERE ID_Medicine = '{1}'",
                string.Join(",", fieldsToUpdate.ToArray()),
                medicine.Id);
            }

            if (medicine.Ingredients != null && medicine.Ingredients.Count > 0)
            {
                medicineIngredientsDeleteQuery = string.Format(@"DELETE  FROM MedicineIngredient
                                                   WHERE ID_Medicine = '{0}'", medicine.Id);

                medicineIngredientsQuery = @"INSERT INTO MedicineIngredient(ID_MedicineIngredient, ID_Medicine, ID_Ingredient)
                                                           VALUES";

                var values = (from ingredient in medicine.Ingredients
                              let entityId = Guid.NewGuid()
                              select string.Format("('{0}', '{1}', '{2}')", entityId, medicine.Id, ingredient.Id))
                              .ToList();

                medicineIngredientsQuery += string.Join(",", values);
            }

            return new List<string> { query, medicineIngredientsDeleteQuery, medicineIngredientsQuery };
        }

        public static List<string> DeleteMedicine(Medicine medicine)
        {
            var queryDeleteMedicine = string.Format(@"DELETE  FROM Medicine
                                        WHERE ID_Medicine = '{0}'", medicine.Id);

            var queryDeleteMedicineIngredients = string.Format(@"DELETE  FROM MedicineIngredient
                                        WHERE ID_Medicine = '{0}'", medicine.Id);

            return new List<string> { queryDeleteMedicineIngredients, queryDeleteMedicine};
        }

        #endregion

        #region MedicineType

        public static string GetUseMethodById(Guid useMethodId)
        {
            var query = String.Format(
                          @"SELECT DISTINCT 
                          ID_MedicineType,
                          Type_Of
                          FROM MedicineType
                          WHERE ID_MedicineType = '{0}'", useMethodId);

            return query;
        }

        public static string GetUseMethodByName(string medicineTypeName)
        {
            var query = String.Format(
                         @"SELECT DISTINCT 
                          ID_MedicineType,
                          Type_Of
                          FROM MedicineType
                          WHERE Title LIKE '%{0}%'", medicineTypeName);

            return query;
        }

        public static string GetAllUseMethods()
        {
            var query = @"SELECT DISTINCT 
                          ID_MedicineType,
                          Type_Of
                          FROM MedicineType";

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
