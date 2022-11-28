using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Protofile.Models;

namespace Protofile.Controllers
{
    public class ProgrammersController : ApiController
    {
        private Database1Entities db = new Database1Entities();

        // GET: api/Programmers
        public IQueryable<Programmer> GetProgrammers()
        {
            return db.Programmers;
        }

        // GET: api/Programmers/5
        [ResponseType(typeof(Programmer))]
        public async Task<IHttpActionResult> GetProgrammer(int id)
        {
            Programmer programmer = await db.Programmers.FindAsync(id);
            if (programmer == null)
            {
                return NotFound();
            }

            return Ok(programmer);
        }

        // PUT: api/Programmers/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutProgrammer(int id, Programmer programmer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != programmer.Id)
            {
                return BadRequest();
            }

            db.Entry(programmer).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProgrammerExists(id))
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

        // POST: api/Programmers
        [ResponseType(typeof(Programmer))]
        public async Task<IHttpActionResult> PostProgrammer(Programmer programmer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Programmers.Add(programmer);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = programmer.Id }, programmer);
        }

        // DELETE: api/Programmers/5
        [ResponseType(typeof(Programmer))]
        public async Task<IHttpActionResult> DeleteProgrammer(int id)
        {
            Programmer programmer = await db.Programmers.FindAsync(id);
            if (programmer == null)
            {
                return NotFound();
            }

            db.Programmers.Remove(programmer);
            await db.SaveChangesAsync();

            return Ok(programmer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProgrammerExists(int id)
        {
            return db.Programmers.Count(e => e.Id == id) > 0;
        }
    }
}