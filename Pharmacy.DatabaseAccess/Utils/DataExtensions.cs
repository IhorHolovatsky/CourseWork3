using System;
using System.Data;

namespace Pharmacy.DatabaseAccess.Utils
{
    public static class DataExtensions
    {
        public static T Get<T>(this DataRow row, string key)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(key) || row[key] == DBNull.Value) return default(T);

                if (typeof (T) == typeof (Guid))
                {
                    return (T)Convert.ChangeType(Guid.Parse((string)row[key]), typeof(T));
                }

                return (T)Convert.ChangeType(row[key], typeof(T));
            }
            catch
            {
                return default(T);
            }
        }
    }
}
