using Microsoft.EntityFrameworkCore;
using Movimientos.Api.Model;

namespace Movimientos.Api.Context
{
    public class ContextMovimiento : DbContext
    {
        public ContextMovimiento(DbContextOptions<ContextMovimiento> options) : base(options) { }
        public DbSet<Movimiento> Movimiento { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
