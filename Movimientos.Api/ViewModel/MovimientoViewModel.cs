using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace Movimientos.Api.ViewModel
{
    public class MovimientoViewModel
    {
        
        [Required]
        public string TipoMovimiento { get; set; }
        [Required]
        public decimal Valor { get; set; }
        [Required]
        public string NumeroCuentaId { get; set; }
    }
}
