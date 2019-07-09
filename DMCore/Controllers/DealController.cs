﻿using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using DMCore.Data.Core;
using DMCore.Data.Core.Domain;
using DMCore.Data.Core.Domain.Deal;
using DMCore.Data.Core.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DMCore.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class DealController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;
        private readonly IConfigurationRoot _config;
        private readonly IHostingEnvironment _env;

        //private readonly IMailService _emailSender;
        //private readonly ISmsService _smsSender;

        public DealController(
            IUnitOfWork unitOfWork,
            ILoggerFactory loggerFactory,
            IConfigurationRoot config,
            IHostingEnvironment env)
        {
            _unitOfWork = unitOfWork;
            _logger = loggerFactory.CreateLogger<DealController>();
            _config = config;
            _env = env;
        }
        private async Task<bool> Exists(long id)
        {
            return await _unitOfWork.Deals.Exist(id);
        }


        // GET: api/<controller>
        [HttpGet]
        [Produces(typeof(DbSet<Deal>))]
        public IActionResult GetAll()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var results = new ObjectResult(_unitOfWork.Deals.GetAll())
            {
                StatusCode = (int)HttpStatusCode.OK
            };

            if (results == null)
            {
                return NotFound();
            }
            Request.HttpContext.Response.Headers.Add("X-Total-Count", _unitOfWork.Deals.GetCount().ToString());

            return results;
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        [Produces(typeof(Deal))]
        public async Task<IActionResult> GetById(int id)
        {
            long Id = (long)id;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var deal = await _unitOfWork.Deals.Find(d=> d.Id==Id);

            if (deal == null)
            {
                return NotFound();
            }

            return Ok(deal);
        }

        // POST api/<controller>
        [HttpPost]
        [Produces(typeof(Deal))]
        public IActionResult Post([FromBody] Deal deal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _unitOfWork.Deals.Add(deal);
            return CreatedAtAction("FindById", new { id = deal.Id }, deal);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        [Produces(typeof(Deal))]
        public async Task<IActionResult> PutAsync([FromRoute] int id, [FromBody] Deal deal)
        {
            long Id = (long)id;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (Id != deal.Id)
            {
                return BadRequest();
            }

            try
            {
                _unitOfWork.Deals.Update(deal);
                return Ok(deal);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await Exists(Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        [Produces(typeof(Deal))]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            long Id = (long)id;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await Exists(Id))
            {
                return NotFound();
            }
            
            _unitOfWork.Deals.Remove(id);
            return Ok();
        }
    }
}
