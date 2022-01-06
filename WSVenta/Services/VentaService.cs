using System;
using System.Linq;
using WSVenta.Models;
using WSVenta.Models.Request;

namespace WSVenta.Services
{
    public class VentaService : IVentaService
    {
        public void Add(VentaRequest model)
        {
            using (var db = new VentaRealContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var venta = new Venta();
                        venta.Total = model.Conceptos.Sum(d => d.Cantidad * d.PrecioUnitario);
                        venta.Fecha = DateTime.Now;
                        venta.IdCliente = model.IdCliente;
                        db.Venta.Add(venta);
                        db.SaveChanges();

                        foreach (var item in model.Conceptos)
                        {
                            var concepto = new Concepto();
                            concepto.Cantidad = item.Cantidad;
                            concepto.IdProducto = item.IdProducto;
                            concepto.PrecioUnitario = item.PrecioUnitario;
                            concepto.Importe = item.Importe;
                            concepto.IdVenta = venta.Id;
                            db.Concepto.Add(concepto);
                            db.SaveChanges();
                        }

                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw new Exception("Error en la inserción");
                    }
                }

            }
        }
    }
}
