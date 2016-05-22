using System;
using System.Collections.Generic;
using System.Linq;
using Pharmacy.DatabaseAccess.Classes;
using Pharmacy.DatabaseAccess.SqlHelpers;

namespace Pharmacy.BusinessLogic.Managers
{
    public class MedicineTypeManager
    {
        private static SqlExecuteManager _sqlManager;

        static MedicineTypeManager()
        {
            _sqlManager = new SqlExecuteManager();
        }

        #region GET
        public static MedicineType GetUseMethodById(Guid useMethodId)
        {
            var query = SqlQueryGeneration.GetUseMethodById(useMethodId);
            var useMethod = _sqlManager.GetUseMethod(query);

            return useMethod.Count > 0 ? useMethod.First() : null;
        }

        public static MedicineType GetUseMethodByName(string useMethodName)
        {
            var query = SqlQueryGeneration.GetUseMethodByName(useMethodName);
            var useMethod = _sqlManager.GetUseMethod(query);

            return useMethod.Count > 0 ? useMethod.First() : null;
        }

        public static List<MedicineType> GetAll()
        {
            var query = SqlQueryGeneration.GetAllUseMethods();
            var useMethod = _sqlManager.GetUseMethod(query);

            return useMethod;
        }
        #endregion
    }
}
