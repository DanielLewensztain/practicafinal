using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using practicafinal.Models;

namespace practicafinal.Controllers
{
    [RoutePrefix("api")]
    public class clasesController : ApiController
    {

        private DataContext db = new DataContext();

        // GET: api/clases
        public IQueryable<clase> Getclases()
        {
            return db.clases;
        }
        
        [HttpGet]
        [Route("{numero:int}")]
        public string Operacion(int numero)
        {
            if (numero < 0)
                return "Error";
            if (numero == 0)
                return "Realizado por Daniel";
            return "usted ingreso el numero" + numero.ToString();

        
        
        }


        // GET: api/clases/5
        [ResponseType(typeof(clase))]
        public IHttpActionResult Getclase(int id)
        {
            clase clase = db.clases.Find(id);
            if (clase == null)
            {
                return NotFound();
            }

            return Ok(clase);
        }

        // PUT: api/clases/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putclase(int id, clase clase)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != clase.numero)
            {
                return BadRequest();
            }

            db.Entry(clase).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!claseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/clases
        [ResponseType(typeof(clase))]
        public IHttpActionResult Postclase(clase clase)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.clases.Add(clase);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = clase.numero }, clase);
        }

        // DELETE: api/clases/5
        [ResponseType(typeof(clase))]
        public IHttpActionResult Deleteclase(int id)
        {
            clase clase = db.clases.Find(id);
            if (clase == null)
            {
                return NotFound();
            }

            db.clases.Remove(clase);
            db.SaveChanges();

            return Ok(clase);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool claseExists(int id)
        {
            return db.clases.Count(e => e.numero == id) > 0;
        }
    }
}