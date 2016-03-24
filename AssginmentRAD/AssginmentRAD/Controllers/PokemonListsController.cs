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
using AssginmentRAD.Models;

namespace AssginmentRAD.Controllers
{
    public class PokemonListsController : ApiController
    {
        private PokemonEntities db = new PokemonEntities();

        // GET: api/PokemonLists
        public IQueryable<PokemonList> GetPokemonLists()
        {
            return db.PokemonLists;
        }

        // GET: api/PokemonLists/5
        [ResponseType(typeof(PokemonList))]
        public IHttpActionResult GetPokemonList(int id)
        {
            PokemonList pokemonList = db.PokemonLists.Find(id);
            if (pokemonList == null)
            {
                return NotFound();
            }

            return Ok(pokemonList);
        }

        // PUT: api/PokemonLists/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPokemonList(int id, PokemonList pokemonList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pokemonList.Id)
            {
                return BadRequest();
            }

            db.Entry(pokemonList).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PokemonListExists(id))
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

        // POST: api/PokemonLists
        [ResponseType(typeof(PokemonList))]
        public IHttpActionResult PostPokemonList(PokemonList pokemonList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PokemonLists.Add(pokemonList);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = pokemonList.Id }, pokemonList);
        }

        // DELETE: api/PokemonLists/5
        [ResponseType(typeof(PokemonList))]
        public IHttpActionResult DeletePokemonList(int id)
        {
            PokemonList pokemonList = db.PokemonLists.Find(id);
            if (pokemonList == null)
            {
                return NotFound();
            }

            db.PokemonLists.Remove(pokemonList);
            db.SaveChanges();

            return Ok(pokemonList);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PokemonListExists(int id)
        {
            return db.PokemonLists.Count(e => e.Id == id) > 0;
        }
    }
}