using Microsoft.EntityFrameworkCore;
using StoredProcedure_EFCore6.Models;

namespace StoredProcedure_EFCore6
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Department> Departments { get; set; }
    }
}
