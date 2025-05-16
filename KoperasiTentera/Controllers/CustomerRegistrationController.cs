using KoperasiTentera.Models;
using KoperasiTentera.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KoperasiTentera.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerRegistrationController : ControllerBase
    {
        private readonly IRegistrationService _registrationService;
        private readonly IVerificationService _verificationService;
        public CustomerRegistrationController(IRegistrationService registrationService, IVerificationService verificationService)
        {
            // Constructor logic here
            _registrationService = registrationService;
            _verificationService = verificationService;
        }
        // GET: api/<CustomerRegistration>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpPost]
        public IActionResult RegisterCustomer([FromBody] CustomerAccount model)
        {
            if (ModelState.IsValid)
            {
                // Call the service to register the customer
                // var result = await _registrationService.RegisterCustomerAsync(model);
                _verificationService.GenerateAndSendMobileVerificationCode(model.MobileNumber);
                return Ok("Customer registered successfully");
            }
            return BadRequest(ModelState);
        }

        [HttpPost("SendTemPINToMobile")]
        public IActionResult SendTemPINToMobile([FromBody] CustomerAccount model)
        {
            if (ModelState.IsValid)
            {
                // Call the service to register the customer
                // var result = await _registrationService.RegisterCustomerAsync(model);
                _verificationService.GenerateAndSendMobileVerificationCode(model.MobileNumber);
                return Ok("Customer registered successfully");
            }
            return BadRequest(ModelState);
        }

        [HttpPost("SendTemPINToEmail")]
        public IActionResult SendTemPINToEmail([FromBody] CustomerAccount model)
        {
            if (ModelState.IsValid)
            {
                // Call the service to register the customer
                // var result = await _registrationService.RegisterCustomerAsync(model);
                _verificationService.GenerateAndSendEmailVerificationCode(model.EmailAddress);
                return Ok("Customer registered successfully");
            }
            return BadRequest(ModelState);
        }

        [HttpPost("SetPin")]
        public IActionResult SetPin([FromBody] CustomerAccount model)
        {
            if (ModelState.IsValid)
            {
                // Call the service to register the customer
                // var result = await _registrationService.RegisterCustomerAsync(model);
                return Ok("Customer registered successfully");
            }
            return BadRequest(ModelState);
        }


        [HttpPost("EnableFinger")]
        public IActionResult EnableFinger([FromBody] CustomerAccount model)
        {
            if (ModelState.IsValid)
            {
                // Call the service to register the customer
                // var result = await _registrationService.RegisterCustomerAsync(model);
                return Ok("Customer registered successfully");
            }
            return BadRequest(ModelState);
        }
    }
}
