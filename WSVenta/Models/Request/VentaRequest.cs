using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WSVenta.Models.Request
{
    public class VentaRequest
    {
        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "El valor del IdCliente debe ser mayor a cero")]
        [ExisteCliente(ErrorMessage = "El cliente no existe")]
        public int IdCliente { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "Deben existir conceptos")]
        public List<ConceptoRequest> Conceptos { get; set; }

        public VentaRequest()
        {
            this.Conceptos = new List<ConceptoRequest>();
        }
    }


    public class ConceptoRequest
    {
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Importe { get; set; }
        public int IdProducto { get; set; }
    }

    #region Validaciones Attribute
    public class ExisteCliente : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            int idCliente = (int)value;
            using (var db = new VentaRealContext())
            {
                if(db.Cliente.Find(idCliente) == null) return false;
            }
            return true;
        }
    }
    #endregion
}
