using BusinessObjects;
using BusinessObjects.Models;
using DAOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Repositories;
using Services;
using System.Net;

namespace API.Controllers
{
    [Route("api/merchants")]
    [ApiController]
    public class MerchantController : ControllerBase
    {
        private readonly IMerchantService iMerchantService;
        public MerchantController()
        {
            iMerchantService = new MerchantService();
        }

        [HttpGet]
        [Route("view_merchants")]
        public IActionResult Get()
        {
            var response = iMerchantService.GetMerchants();
            return Ok(response);
        }

        [HttpGet]
        [Route("with_paging")]
        public IActionResult GetPaging([FromQuery] BasePagingQuery query)
        {
            var response = new BaseResultWithData<BasePagingData<MerchantDTO>>();
            return Ok(response);
        }

        [HttpGet]
        [Route("{id}")]

        [ProducesResponseType(typeof(BaseResultWithData<MerchantDTO>), 200)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult GetOne([FromRoute] string id)
        {
            var response = new BaseResultWithData<MerchantDTO>();
            return Ok();
        }

        [HttpPost]
        [Route("create_merchant")]
        public IActionResult Create([FromBody] MerchantCreate request)
        {
            var response = new Merchant()
            {
                Id = Guid.NewGuid().ToString(),
                MerchantName = request.MerchantName,
                MerchantWebLink = request.MerchantWebLink,
                MerchantIpnUrl = request.MerchantIpnUrl,
                MerchantReturnUrl = request.MerchantReturnUrl,
                SecretKey = "jhsbdv",
                IsActive = false
            };

            iMerchantService.AddMerchant(response);
            return Ok(response);
        }

        [HttpPut]
        [Route("update/{id}")]
        public IActionResult Update(string id, [FromBody] MerchantUpdate request)
        {
            var merchant = iMerchantService.GetMerchants().FirstOrDefault(s => s.Id == id);
            if (merchant == null)
            {
                return NotFound();
            }

            merchant.MerchantName = request.MerchantName;
            merchant.MerchantWebLink = request.MerchantWebLink;
            merchant.MerchantIpnUrl = request.MerchantIpnUrl;
            merchant.MerchantReturnUrl = request.MerchantReturnUrl;
            merchant.SecretKey = request.SecretKey;
            merchant.IsActive = true;

           
            return Ok(iMerchantService.UpdateMerchant(merchant));
        }

        [HttpPut]
        [Route("set_active/{id}")]
        public IActionResult SetActive(string id, bool isActive)
        {
            var merchant = iMerchantService.GetMerchants().FirstOrDefault(s => s.Id == id);
            if (merchant == null)
            {
                return NotFound();
            }
            merchant.IsActive = isActive;

            return Ok(iMerchantService.UpdateMerchant(merchant));
        }

    }
}
