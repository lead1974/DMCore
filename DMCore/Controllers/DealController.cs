using System;
using System.Threading.Tasks;
using DMCore.Data;
using DMCore.Data.Core;
using DMCore.Data.Core.Domain.Deal;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
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

        private async Task<bool> dealExists(long id)
        {
            return await _unitOfWork.Deals.Exist(id);
        }

        // GET: api/<controller>
        [HttpGet]
        [Route("[action]")]
        [Produces(typeof(DbSet<Deal>))]
        public IActionResult GetAllDeals([DataSourceRequest]DataSourceRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var deals = _unitOfWork.Deals.GetAll();
            if (deals == null)
            {
                return NotFound();
            }
            Request.HttpContext.Response.Headers.Add("X-Total-Count", _unitOfWork.Deals.GetCount().ToString());

            return new JsonResult(deals.ToDataSourceResult(request));
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        [Route("[action]")]
        [Produces(typeof(DbSet<Deal>))]
        public ActionResult<Deal> GetDealByID([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var deal = _unitOfWork.Deals.SingleOrDefault(c => c.Id == id);
            if (deal == null)
            {
                return NotFound();
            }

            return Ok(deal);
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult CreateDeal([DataSourceRequest]DataSourceRequest request, [FromForm]Deal deal)
        {

            //return new ObjectResult(new DataSourceResult { Data = new[] { deal }, Total = 1 });

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (deal != null && ModelState.IsValid)
            {
                try
                {
                    var dealToAdd = new Deal
                    {
                        Title = deal.Title,
                        URL = deal.URL,
                        AffiliateSiteId = deal.AffiliateSiteId,
                        StoreId = deal.StoreId,
                        Approved = deal.Approved,
                        CouponCode = deal.CouponCode,
                        DealCategoryId = deal.DealCategoryId,
                        DealTagId = deal.DealTagId,
                        Dislikes = deal.Dislikes,
                        DMProduct = deal.DMProduct,                        
                        ImageURL = deal.ImageURL,
                        Instructions = deal.Instructions,
                        IsCoupon = deal.IsCoupon,
                        Likes = deal.Likes,
                        Price = deal.Price,
                        StartTS = deal.StartTS,
                        EndTS = deal.EndTS,
                        Status = deal.Status,                   
                        Views = deal.Views,
                        UpdatedTS = DateTime.UtcNow,
                        UpdatedBy = User.Identity.Name,
                        CreatedTS = DateTime.UtcNow,
                        CreatedBy = User.Identity.Name
                    };


                    _unitOfWork.Deals.Add(dealToAdd);
                    _unitOfWork.Complete();
                    return Json(new[] { dealToAdd }.ToDataSourceResult(request, ModelState));
                }
                catch (DbUpdateConcurrencyException)
                {

                    {
                        return BadRequest();
                    }
                }

            }
            return CreatedAtAction(nameof(GetDealByID), new { id = deal.Id }, deal);
        }

        [HttpPut("{id}")]
        [Route("[action]")]
        public async Task<IActionResult> UpdateDeal([DataSourceRequest]DataSourceRequest request, [FromForm]Deal deal)
        {
            //return new StatusCodeResult(200);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (deal == null || deal.Id == 0)
            {
                return BadRequest();
            }

            if (deal != null && ModelState.IsValid)
            {
                var dealToUpdate = _unitOfWork.Deals.SingleOrDefault(c => c.Id == deal.Id);

                dealToUpdate.Title = deal.Title;
                dealToUpdate.URL = deal.URL;
                dealToUpdate.AffiliateSiteId = deal.AffiliateSiteId;
                dealToUpdate.StoreId = deal.StoreId;
                dealToUpdate.Approved = deal.Approved;
                dealToUpdate.CouponCode = deal.CouponCode;
                dealToUpdate.DealCategoryId = deal.DealCategoryId;
                dealToUpdate.DealTagId = deal.DealTagId;
                dealToUpdate.Dislikes = deal.Dislikes;
                dealToUpdate.DMProduct = deal.DMProduct;
                dealToUpdate.ImageURL = deal.ImageURL;
                dealToUpdate.Instructions = deal.Instructions;
                dealToUpdate.IsCoupon = deal.IsCoupon;
                dealToUpdate.Likes = deal.Likes;
                dealToUpdate.Price = deal.Price;
                dealToUpdate.StartTS = deal.StartTS;
                dealToUpdate.EndTS = deal.EndTS;
                dealToUpdate.Status = deal.Status;
                dealToUpdate.Views = deal.Views;
                dealToUpdate.UpdatedTS = DateTime.UtcNow;
                dealToUpdate.UpdatedBy = User.Identity.Name.Substring(13);
                try
                {
                    _unitOfWork.Deals.Update(dealToUpdate);
                    _unitOfWork.Complete();
                    return Ok(deal);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await dealExists(deal.Id))
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
        public IActionResult DeleteDeal(long id)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (ModelState.IsValid)
            {
                var dealToDelete = _unitOfWork.Deals.SingleOrDefault(c => c.Id == id);

                if (dealToDelete == null) return BadRequest();

                dealToDelete.Title = dealToDelete.Title + " (Suspended)";
                dealToDelete.Status = c.Status.Deleted.ToString();
                dealToDelete.UpdatedTS = DateTime.UtcNow;
                dealToDelete.UpdatedBy = User.Identity.Name.Substring(13);

                _unitOfWork.Deals.Update(dealToDelete);
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
