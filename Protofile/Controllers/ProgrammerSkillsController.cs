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
    public class ProgrammerSkillsController : ApiController
    {
        private Database1Entities db = new Database1Entities();

        // GET: api/ProgrammerSkills
        public IQueryable<ProgrammerSkill> GetProgrammerSkills()
        {
            return db.ProgrammerSkills;
        }

        // GET: api/ProgrammerSkills/5
        [ResponseType(typeof(ProgrammerSkill))]
        public async Task<IHttpActionResult> GetProgrammerSkill(int id)
        {
            ProgrammerSkill programmerSkill = await db.ProgrammerSkills.FindAsync(id);
            if (programmerSkill == null)
            {
                return NotFound();
            }

            return Ok(programmerSkill);
        }

        // PUT: api/ProgrammerSkills/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutProgrammerSkill(int id, ProgrammerSkill programmerSkill)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != programmerSkill.IDProgrammerSkills)
            {
                return BadRequest();
            }

            db.Entry(programmerSkill).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProgrammerSkillExists(id))
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

        // POST: api/ProgrammerSkills
        [ResponseType(typeof(ProgrammerSkill))]
        public async Task<IHttpActionResult> PostProgrammerSkill(ProgrammerSkill programmerSkill)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProgrammerSkills.Add(programmerSkill);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = programmerSkill.IDProgrammerSkills }, programmerSkill);
        }

        // DELETE: api/ProgrammerSkills/5
        [ResponseType(typeof(ProgrammerSkill))]
        public async Task<IHttpActionResult> DeleteProgrammerSkill(int id)
        {
            ProgrammerSkill programmerSkill = await db.ProgrammerSkills.FindAsync(id);
            if (programmerSkill == null)
            {
                return NotFound();
            }

            db.ProgrammerSkills.Remove(programmerSkill);
            await db.SaveChangesAsync();

            return Ok(programmerSkill);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProgrammerSkillExists(int id)
        {
            return db.ProgrammerSkills.Count(e => e.IDProgrammerSkills == id) > 0;
        }
    }
}