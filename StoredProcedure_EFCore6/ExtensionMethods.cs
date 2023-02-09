using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Data;

namespace StoredProcedure_EFCore6
{
    public static class ExtensionMethods
    {
        public static T ConvertDataTable<T>(this DataTable dataTable)
        {
            var jsonText = JsonConvert.SerializeObject(dataTable);
            var models = JsonConvert.DeserializeObject<T>(jsonText);
            if (models != null)
                return models;
            return default(T);
        }
    }
}
