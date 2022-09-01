using Microsoft.EntityFrameworkCore;

namespace Cliente.Api.Persistencia
{
    public class ContextCliente : DbContext
    {
        public ContextCliente()
        {

        }
        public ContextCliente( DbContextOptions<ContextCliente> options) : base(options){}
        public virtual DbSet<Cliente.Api.Modelo.Cliente> Cliente { get; set; }
        public virtual DbSet<Cliente.Api.Modelo.Persona> Persona { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
