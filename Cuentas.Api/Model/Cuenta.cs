using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cuentas.Api.Model
{
    [Table("tbCuenta", Schema = "cta")]
    public class Cuenta
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [MaxLength(350)]
        public string NumeroCuenta { get; set; }
        [MaxLength(100)]
        public string TipoCuenta { get; set; }
        public decimal SaldoInicial { get; set; }
        public bool Estado { get; set; }
        [Required]
        public int IdCliente { get; set; }
    }
}
