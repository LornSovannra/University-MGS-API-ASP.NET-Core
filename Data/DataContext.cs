using Microsoft.EntityFrameworkCore;
using University_MGS_API.Models;

namespace University_MGS_API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options ) : base( options ) { }

        public DbSet<Student> students { get; set; }
    }
}
