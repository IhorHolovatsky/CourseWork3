using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pharmacy.BusinessLogic.SqlHelpers;
using Pharmacy.Objects.Classes;

namespace Pharmacy.BusinessLogic.Data
{
    class UseMethodManager
    {
        private static SqlExecuteManager _sqlManager;

        static UseMethodManager()
        {
            _sqlManager = new SqlExecuteManager();
        }

        #region GET
        public static UseMethod GetUseMethodById(Guid useMethodId)
        {
            var query = SqlQueryGeneration.GetUseMethodById(useMethodId);
            var useMethod = _sqlManager.GetUseMethod(query);

            return useMethod.Count > 0 ? useMethod.First() : null;
        }

        public static UseMethod GetUseMethodByName(string useMethodName)
        {
            var query = SqlQueryGeneration.GetUseMethodByName(useMethodName);
            var useMethod = _sqlManager.GetUseMethod(query);

            return useMethod.Count > 0 ? useMethod.First() : null;
        }

        public static List<UseMethod> GetAll()
        {
            var query = SqlQueryGeneration.GetAllUseMethods();
            var useMethod = _sqlManager.GetUseMethod(query);

            return useMethod;
        }
        #endregion
    }
}
