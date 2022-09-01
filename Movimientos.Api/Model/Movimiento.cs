
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Movimientos.Api.Model
{
    [Table("tbMovimiento", Schema = "mov")]
    public class Movimiento
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdMovimiento { get; set; }
        [Required]
        public string TipoMovimiento { get; set; }
        public DateTime FechaMovimiento { get; set; }
        [Required]
        public decimal Valor { get; set; }
        [Required]
        public decimal Saldo { get; set; }
        [Required]
        public string NumeroCuentaId { get; set; }
    }
}
