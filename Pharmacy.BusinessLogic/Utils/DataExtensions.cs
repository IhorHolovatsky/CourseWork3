using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.BusinessLogic.Utils
{
    public static class DataExtensions
    {
        public static T Get<T>(this DataRow row, string key)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(key) || row[key] == DBNull.Value) return default(T);
                return (T)Convert.ChangeType(row[key], typeof(T));
            }
            catch
            {
                return default(T);
            }
        }
    }
}
