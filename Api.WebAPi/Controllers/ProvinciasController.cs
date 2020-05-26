﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.WebAPi.Data;
using Api.WebAPi.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.WebAPi.Controllers
{
    // api/provincias
    [Route("api/[controller]")]
    [ApiController]
    public class ProvinciasController : ControllerBase
    {
        private readonly ApiContext context;

        public ProvinciasController(ApiContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Provincia>> Get()
        {
            return context.Provincias.Include(p => p.Pais).ToList();
        }

        //api/provincias/ProvbyId/id. [int]
        [HttpGet("ProvBy/{id}", Name = "ProvById")]
        public ActionResult<Provincia> GetProvById(int id)
        {
            var provincia = context.Provincias.FirstOrDefault(p => p.Id == id);
            if (provincia == null)
            {
                return NotFound();

            }
            return provincia;
        }
        //api/provincias/GetProvByCod/cod [string]
        [HttpGet("ProvByCod/{cod}")]
        public ActionResult<Provincia> GetProvByCod (string cod)
        {
            var provincia = context.Provincias.Include(p => p.Pais).FirstOrDefault(
                p => p.CodProv == cod);
            if (provincia== null)
            {
                return NotFound();
            }
            return provincia;
        }


        [HttpPost]
        public ActionResult<Provincia> Post([FromBody]Provincia  provincia)
        //public async Task<ActionResult<Provincia>> Post([FromBody]Provincia provincia)
        {
            context.Provincias.Add(provincia);
            //await context.Provincia.AddAsync(Provincia);
            context.SaveChanges();
            //await context.SaveChangesAsync();
            return new CreatedAtRouteResult("ProvById", new { id = provincia.Id }, provincia);
            //return pais;

        }

        [HttpPut("{id}")]
        public ActionResult<Provincia> Put(int id,[FromBody] Provincia provincia)
        {
            if (id!= provincia.Id)
            {
                return BadRequest();
            }
            context.Entry(provincia).State = EntityState.Modified;
            context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult<Provincia> Delete(int id)
        {
            var provincia = context.Provincias.FirstOrDefault(p => p.Id == id);
            if (provincia == null)
            {
                return NotFound();
            }
            context.Provincias.Remove(provincia);
            context.SaveChanges();
            return Ok();
        }
    }
}