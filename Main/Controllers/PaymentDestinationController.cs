using BusinessObjects;
using BusinessObjects.Models;
using DAOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using System.Net;

namespace API.Controllers
{
    [Route("api/paymentdestination")]
    [ApiController]
    public class PaymentDestinationController : ControllerBase
    {
        private readonly IPaymentDestinationService iPaymentDestinationService;
        public PaymentDestinationController()
        {
            iPaymentDestinationService = new PaymentDestinationService();
        }

        [HttpGet]
     
        public IActionResult Get()
        {
            var response = iPaymentDestinationService.GetDestinations();
            return Ok(response);
        }

        [HttpGet]
        [Route("with-paging")]
        public IActionResult GetPaging([FromQuery] BasePagingQuery query)
        {
            return Ok();
        }

        [HttpGet]
        [Route("get_destination/{id}")]
        public IActionResult GetOne([FromRoute] string id)
        {
            var response = iPaymentDestinationService.GetDestinations().FirstOrDefault(d => d.Id == id);
            return Ok(response);
        }

        [HttpPost]
        [Route("create_destination")]
        public IActionResult Create([FromBody] CreatePaymentDestination request)
        {
            var response = new PaymentDestination() 
            {
                Id = Guid.NewGuid().ToString(),
                DesName = request.DesName,
                DesShortName = request.DesShortName,
                DesParentId = request.DesParentId,
                DesLogo = request.DesLogo,
                SortIndex = request.SortIndex,
                IsActive = true
            };

            return Ok(iPaymentDestinationService.AddDestination(response));
        }

        [HttpPut]
        [Route("update/{id}")]
        public IActionResult Update(string id, [FromBody] CreatePaymentDestination request)
        {
            var destination = iPaymentDestinationService.GetDestinations().FirstOrDefault(s => s.Id == id);
            if (destination == null)
            {
                return NotFound();
            }

            destination.DesName = request.DesName;
            destination.DesShortName = request.DesShortName;
            destination.DesParentId = request.DesParentId;
            destination.DesLogo = request.DesLogo;
            destination.SortIndex = request.SortIndex;
            destination.IsActive = true;


            return Ok(iPaymentDestinationService.UpdateDestination(destination));
        }

        [HttpPut]
        [Route("set_active/{id}")]
        public IActionResult SetActive(string id, bool isActive)
        {
            var destination = iPaymentDestinationService.GetDestinations().FirstOrDefault(s => s.Id == id);
            if (destination == null)
            {
                return NotFound();
            }
            destination.IsActive = isActive;

            return Ok(iPaymentDestinationService.UpdateDestination(destination));
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult Delete(string id)
        {
            var destination = iPaymentDestinationService.GetDestinations().FirstOrDefault(s => s.Id == id);
            if (destination == null)
            {
                return NotFound();
            }
            destination.IsActive = false;
            return Ok(iPaymentDestinationService.UpdateDestination(destination));
        }
    }
}
