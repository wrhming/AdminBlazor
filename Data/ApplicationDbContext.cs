using AdminBlazor.Authentication;
using Microsoft.EntityFrameworkCore;

namespace AdminBlazor.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<SysRole> SysRole { get; set; }

        public DbSet<SysUser> SysUser { get; set; }

        public DbSet<SysRolePermissions> SysRolePermissions { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Send();
        }
    }
}
