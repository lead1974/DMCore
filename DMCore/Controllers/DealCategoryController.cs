using System;
using System.Net;
using System.Threading.Tasks;
using DMCore.Data;
using DMCore.Data.Core;
using DMCore.Data.Core.Domain;
using DMCore.Data.Core.Domain.Deal;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
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
    [ApiController]
    //[Authorize(Policy = SD.PolicyCanManageSite)]
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

        private async Task<bool> CategoryExists(long id)
        {
            return await _unitOfWork.DealCategories.Exist(id);
        }

        // GET: api/<controller>
        [HttpGet]
        [Route("[action]")]
        [Produces(typeof(DbSet<DealCategory>))]
        public IActionResult GetAllCategories([DataSourceRequest]DataSourceRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var categories = _unitOfWork.DealCategories.GetAll();
            if (categories == null)
            {
                return NotFound();
            }
            Request.HttpContext.Response.Headers.Add("X-Total-Count", _unitOfWork.DealCategories.GetCount().ToString());

            return new JsonResult(categories.ToDataSourceResult(request));
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        [Route("[action]")]
        [Produces(typeof(DbSet<DealCategory>))]
        public ActionResult<DealCategory> GetCategoryByID([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var category = _unitOfWork.DealCategories.SingleOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult CreateCategory([DataSourceRequest]DataSourceRequest request, [FromForm]DealCategory category)
        {

            //return new ObjectResult(new DataSourceResult { Data = new[] { category }, Total = 1 });

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (category != null && ModelState.IsValid)
            {
                try
                {
                    var categoryToAdd = new DealCategory
                    {
                        Name = category.Name,
                        ShortName = category.ShortName,
                        SortSeq = category.SortSeq,
                        PublicCategory = category.PublicCategory,
                        Status = c.Status.Active.ToString(),
                        UpdatedTS = DateTime.UtcNow,
                        UpdatedBy = User.Identity.Name,
                        CreatedTS = DateTime.UtcNow,
                        CreatedBy = User.Identity.Name

                    };


                    _unitOfWork.DealCategories.Add(categoryToAdd);
                    _unitOfWork.Complete();
                    return Json(new[] { categoryToAdd }.ToDataSourceResult(request, ModelState));
                }
                catch (DbUpdateConcurrencyException)
                {

                    {
                        return BadRequest();
                    }
                }

            }
            return CreatedAtAction(nameof(GetCategoryByID), new { id = category.Id }, category);
        }

        [HttpPut("{id}")]
        [Route("[action]")]
        public async Task<IActionResult> UpdateCategory([DataSourceRequest]DataSourceRequest request, [FromForm]DealCategory category)
        {
            //return new StatusCodeResult(200);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (category == null || category.Id == 0)
            {
                return BadRequest();
            }

            if (category != null && ModelState.IsValid)
            {
                var categoryToUpdate = _unitOfWork.DealCategories.SingleOrDefault(c => c.Id == category.Id);
                categoryToUpdate.Name = category.Name;
                categoryToUpdate.ShortName = category.ShortName;
                categoryToUpdate.SortSeq = category.SortSeq;
                categoryToUpdate.PublicCategory = category.PublicCategory;
                categoryToUpdate.UpdatedTS = DateTime.UtcNow;
                categoryToUpdate.UpdatedBy = User.Identity.Name.Substring(13);
                try
                {
                    _unitOfWork.DealCategories.Update(categoryToUpdate);
                    _unitOfWork.Complete();
                    return Ok(category);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await CategoryExists(category.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
            }
            return new JsonResult(ModelState.IsValid ? true : ModelState.ToDataSourceResult());
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(long id)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (ModelState.IsValid)
            {
                var categoryToDelete = _unitOfWork.DealCategories.SingleOrDefault(c => c.Id == id);

                if (categoryToDelete == null) return BadRequest();

                categoryToDelete.Name = categoryToDelete.Name + " (Suspended)";
                categoryToDelete.Status = c.Status.Deleted.ToString();
                categoryToDelete.UpdatedTS = DateTime.UtcNow;
                categoryToDelete.UpdatedBy = User.Identity.Name.Substring(13);

                _unitOfWork.DealCategories.Update(categoryToDelete);
                _unitOfWork.Complete();
            }
            else
            {
                return BadRequest();
            }

            return Ok();
        }

    }
}
