using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DMCore.Data;
using DMCore.Data.Models;
using DMCore.Data.Repositories;
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
    public class DealsController : Controller
    {
        private readonly UserManager<AuthUser> _userManager;
        private readonly SignInManager<AuthUser> _signInManager;
        private readonly RoleManager<AuthRole> _roleManager;
        private readonly ILogger _logger;
        private readonly IConfigurationRoot _config;
        private readonly IHostingEnvironment _env;
        private readonly IDealRepository _dealRepo;

        //private readonly IMailService _emailSender;
        //private readonly ISmsService _smsSender;

        public DealsController(
            IDealRepository dealRepo,
            UserManager<AuthUser> userManager,
            SignInManager<AuthUser> signInManager,
            RoleManager<AuthRole> roleManager,
            ILoggerFactory loggerFactory,
            IConfigurationRoot config,
            IHostingEnvironment env)
        {
            _dealRepo = dealRepo;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _logger = loggerFactory.CreateLogger<DealsController>();
            _config = config;
            _env = env;
        }
        private async Task<bool> DealExists(long id)
        {
            return await _dealRepo.Exist(id);
        }


        // GET: api/<controller>
        [HttpGet]
        [Produces(typeof(DbSet<Deal>))]
        public IActionResult GetDeals()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var results = new ObjectResult(_dealRepo.GetAll())
            {
                StatusCode = (int)HttpStatusCode.OK
            };

            if (results == null)
            {
                return NotFound();
            }
            Request.HttpContext.Response.Headers.Add("X-Total-Count", _dealRepo.GetCount().ToString());

            return results;
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        [Produces(typeof(Deal))]
        public async Task<IActionResult> GetDeal(int id)
        {
            long Id = (long)id;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var deal = await _dealRepo.GetById(Id);

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

            _dealRepo.Add(deal);
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
                await _dealRepo.Update(deal);
                return Ok(deal);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await DealExists(Id))
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

            if (!await DealExists(Id))
            {
                return NotFound();
            }

            await _dealRepo.Remove(Id);

            return Ok();
        }
    }
}
