using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using WSVenta.Models;
using WSVenta.Models.Request;
using WSVenta.Models.Response;

namespace WSVenta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                using (VentaRealContext db = new VentaRealContext())
                {
                    var lst = db.Cliente.OrderByDescending(d=>d.Id).ToList();
                    respuesta.Exito = 1;
                    respuesta.Data = lst;
                }
            }
            catch (Exception ex)
            {
                respuesta.Mensaje= ex.Message;
            }
            return Ok(respuesta);
        }

        [HttpPost]
        public IActionResult Add(ClienteRequest model)
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                using (VentaRealContext db = new VentaRealContext())
                {
                    var cliente = new Cliente();
                    cliente.Nombre = model.Nombre;
                    db.Cliente.Add(cliente);
                    db.SaveChanges();
                    respuesta.Exito = 1;                    
                }
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = ex.Message;
            }
            return Ok(respuesta);
        }

        [HttpPut]
        public IActionResult Edit(ClienteRequest model)
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                using (VentaRealContext db = new VentaRealContext())
                {
                    var cliente = db.Cliente.Find(model.Id);
                    cliente.Nombre = model.Nombre;
                    db.Entry(cliente).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    db.SaveChanges();
                    respuesta.Exito = 1;
                }
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = ex.Message;
            }
            return Ok(respuesta);
        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                using (VentaRealContext db = new VentaRealContext())
                {
                    var cliente = db.Cliente.Find(Id);
                    db.Remove(cliente);
                    db.SaveChanges();
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
