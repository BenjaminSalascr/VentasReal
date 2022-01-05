using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using WSVenta.Models;
using WSVenta.Models.Request;
using WSVenta.Models.Response;

namespace WSVenta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VentaController : ControllerBase
    {
        [HttpPost]
        public IActionResult Add(VentaRequest model)
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                using (var db = new VentaRealContext())
                {
                    var venta = new Venta();
                    //venta.Total = model.Total;//transacción hecha en la BD.
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

                    respuesta.Exito = 1;
                }
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = ex.Message;
            }
            return Ok(respuesta);
        }
    }
}
