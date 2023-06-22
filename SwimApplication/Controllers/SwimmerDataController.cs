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
using SwimApplication.Models;

namespace SwimApplication.Controllers
{
    public class SwimmerDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        /// <summary>
        /// Returns all swimmers in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// </returns>
        /// </example>
        // GET: api/SwimmerData/ListSwimmers
        [HttpGet]
        public IEnumerable<SwimmerDto> ListSwimmers()
        {
            List<Swimmer> Swimmers = db.Swimmers.ToList();
            List<SwimmerDto> SwimmerDtos = new List<SwimmerDto>();

            Swimmers.ForEach(a => SwimmerDtos.Add(new SwimmerDto()
            {
                SwimmerID = a.SwimmerID,
                SwimmerName = a.SwimmerName,
                Age = a.Age,
              
            }));

            return SwimmerDtos;
        }

        /// <summary>
        /// Returns all sessions in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: An session in the system matching up to the session ID primary key
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <param name="id">The primary key of the animal</param>
        /// <example>
        // GET: api/SessionData/FindSession/5

        [ResponseType(typeof(Swimmer))]
        [HttpGet]
        public IHttpActionResult FindSwimmer(int id)
        {
            Swimmer Swimmers = db.Swimmers.Find(id);
            SwimmerDto SwimmerDto = new SwimmerDto()
            {
                SwimmerID = Swimmers.SwimmerID,
                SwimmerName = Swimmers.SwimmerName,
                Age = Swimmers.Age,
            };
            if (Swimmers == null)
            {
                return NotFound();
            }

            return Ok(SwimmerDto);
        }

        // POST: api/SessionData/UpdateSession/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateSwimmer(int id, Swimmer swimmer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != swimmer.SwimmerID)
            {
                return BadRequest();
            }

            db.Entry(swimmer).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SwimmerExists(id))
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

        // POST: api/SwimmerData/AddSwimmer
        [ResponseType(typeof(Swimmer))]
        [HttpPost]
        public IHttpActionResult AddSwimmer(Swimmer swimmer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Swimmers.Add(swimmer);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = swimmer.SwimmerID }, swimmer);
        }

        // POST: api/SessionData/DeleteSession/5
        [ResponseType(typeof(Swimmer))]
        [HttpPost]
        public IHttpActionResult DeleteSwimmer(int id)
        {
            Swimmer swimmer = db.Swimmers.Find(id);
            if (swimmer == null)
            {
                return NotFound();
            }

            db.Swimmers.Remove(swimmer);
            db.SaveChanges();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SwimmerExists(int id)
        {
            return db.Swimmers.Count(e => e.SwimmerID == id) > 0;
        }
    }
}