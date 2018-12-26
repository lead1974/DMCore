using System.Net;
using System.Threading.Tasks;
using DMCore.Data;
using DMCore.Data.Core;
using DMCore.Data.Core.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DMCore.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class DealCategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;
        private readonly IConfigurationRoot _config;
        private readonly IHostingEnvironment _env;

        //private readonly IMailService _emailSender;
        //private readonly ISmsService _smsSender;

        public DealCategoryController(
            IUnitOfWork unitOfWork,
            ILoggerFactory loggerFactory,
            IConfigurationRoot config,
            IHostingEnvironment env)
        {
            _unitOfWork = unitOfWork;
            _logger = loggerFactory.CreateLogger<DealCategoryController>();
            _config = config;
            _env = env;
        }
        private async Task<bool> Exists(long id)
        {
            return await _unitOfWork.Deals.Exist(id);
        }


        // GET: api/<controller>
        [HttpGet]
        [Produces(typeof(DbSet<DealCategory>))]
        public IActionResult GetAll()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var results = new ObjectResult(_unitOfWork.DealCategories.GetAll())
            {
                StatusCode = (int)HttpStatusCode.OK
            };

            if (results == null)
            {
                return NotFound();
            }
            Request.HttpContext.Response.Headers.Add("X-Total-Count", _unitOfWork.DealCategories.GetCount().ToString());

            return results;
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        [Produces(typeof(DealCategory))]
        public async Task<IActionResult> GetById(int id)
        {
            long Id = (long)id;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dealCategory = await _unitOfWork.DealCategories.Find(d=> d.Id==Id);

            if (dealCategory == null)
            {
                return NotFound();
            }

            return Ok(dealCategory);
        }

        // POST api/<controller>
        [HttpPost]
        [Produces(typeof(DealCategory))]
        [Authorize(Policy=SD.PolicyCanManageSite)]
        public IActionResult Post([FromBody] DealCategory dealCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _unitOfWork.DealCategories.Add(dealCategory);
            return CreatedAtAction("FindById", new { id = dealCategory.Id }, dealCategory);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        [Produces(typeof(DealCategory))]
        [Authorize(Policy = SD.PolicyCanManageSite)]
        public async Task<IActionResult> PutAsync([FromRoute] int id, [FromBody] DealCategory dealCategory)
        {
            long Id = (long)id;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (Id != dealCategory.Id)
            {
                return BadRequest();
            }

            try
            {
                _unitOfWork.DealCategories.Update(dealCategory);
                return Ok(dealCategory);
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
        [Produces(typeof(DealCategory))]
        [Authorize(Policy = SD.PolicyCanManageSite)]
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
            
            _unitOfWork.DealCategories.Remove(id);
            return Ok();
        }
    }
}
