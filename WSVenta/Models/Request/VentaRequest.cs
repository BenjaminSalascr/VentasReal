using System.Collections.Generic;

namespace WSVenta.Models.Request
{
    public class VentaRequest
    {
        public int IdCliente { get; set; }
        public decimal Total { get; set; }
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
}
