using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using StoredProcedure_EFCore6.Models;
using System.Data;
using System.Diagnostics;

namespace StoredProcedure_EFCore6.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _applicationDbContext;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext applicationDbContext)
        {
            _logger = logger;
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IActionResult> Index()
        {
            var sqlParameters = new List<SqlParameter>()
            {
                new SqlParameter("@Id", 0),
                new SqlParameter("@Code", "")
            };
            var data = new List<Department>();
            var departmentsSP = await GetDataTableFromSP("GetDepartment", sqlParameters);
            var models =departmentsSP.ConvertDataTable<List<Department>>();

            return View(models.Take(1000));
        }

        private async Task<DataTable> GetDataTableFromSP(string storedProcedure, List<SqlParameter> sqlParameters)
        {
            using (var command = _applicationDbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = storedProcedure;
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddRange(sqlParameters.ToArray());
                await _applicationDbContext.Database.OpenConnectionAsync();

                using (var result = command.ExecuteReader())
                {
                    var dataTable = new DataTable();
                    dataTable.Load(result);
                    await _applicationDbContext.Database.CloseConnectionAsync();
                    return dataTable;
                }
            }
        }    
    }
}
