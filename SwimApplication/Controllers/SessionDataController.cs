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
    public class SessionDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        /// <summary>
        /// Returns all sessions in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// </returns>
        /// </example>
        // GET: api/SessionData/ListSessions
        [HttpGet]
        public IEnumerable<SessionDto> ListSessions()
        {
            List<Session> Sessions = db.Sessions.ToList();
            List<SessionDto> SessionDtos = new List<SessionDto>();

            Sessions.ForEach(a => SessionDtos.Add(new SessionDto()
            {
                SessionID = a.SessionID,
                Date = a.Date,
                Distance = a.Distance,
                Duration = a.Duration,
                SwimmerName = a.Swimmer.SwimmerName
            })) ; 

            return SessionDtos;
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

        [ResponseType(typeof(Session))]
        [HttpGet]
        public IHttpActionResult FindSession(int id)
        {
            Session Session = db.Sessions.Find(id);
            SessionDto SessionDto = new SessionDto()
            {
                SessionID = Session.SessionID,
                Date = Session.Date,
                Distance = Session.Distance,
                Duration = Session.Duration,
                SwimmerName = Session.Swimmer.SwimmerName
            };
            if (Session == null)
            {
                return NotFound();
            }

            return Ok(SessionDto);
        }

        // POST: api/SessionData/UpdateSession/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateSession(int id, Session session)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != session.SessionID)
            {
                return BadRequest();
            }

            db.Entry(session).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SessionExists(id))
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

        // POST: api/SessionData/AddSession
        [ResponseType(typeof(Session))]
        [HttpPost]
        public IHttpActionResult AddSession(Session session)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Sessions.Add(session);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = session.SessionID }, session);
        }

        // POST: api/SessionData/DeleteSession/5
        [ResponseType(typeof(Session))]
        [HttpPost]
        public IHttpActionResult DeleteSession(int id)
        {
            Session session = db.Sessions.Find(id);
            if (session == null)
            {
                return NotFound();
            }

            db.Sessions.Remove(session);
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

        private bool SessionExists(int id)
        {
            return db.Sessions.Count(e => e.SessionID == id) > 0;
        }
    }
}