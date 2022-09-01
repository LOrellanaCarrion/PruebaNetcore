using Cuentas.Api.Model;
using Microsoft.EntityFrameworkCore;

namespace Cuentas.Api.Context
{
    public class ContextCuenta : DbContext
    {
        public ContextCuenta(DbContextOptions<ContextCuenta> options) : base(options) { }
        public DbSet<Cuenta> Cuenta { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
