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
        [Route("viewlist")]
        public IActionResult Get()
        {
            var response = iPaymentDestinationService.GetDestinations();
            return Ok(response);
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
                BankCode = request.BankCode,
                BankName = request.BankName,
                BankLogo = request.BankLogo,
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

            destination.BankCode = request.BankCode;
            destination.BankName = request.BankName;
            destination.BankLogo = request.BankLogo;
            destination.IsActive = true;

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
