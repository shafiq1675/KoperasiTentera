using KoperasiTentera.Models;
using KoperasiTentera.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KoperasiTentera.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IVerificationService _verificationService;

        public AuthController(IUserService userService, IVerificationService verificationService)
        {
            _userService = userService;
            _verificationService = verificationService;
        }

        [HttpPost("check-registration")]
        public async Task<IActionResult> CheckRegistration([FromBody] string icnNumber)
        {
            var isRegistered = await _userService.IsUserRegistered(icnNumber);
            return Ok(new { isRegistered });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            try
            {
                var user = await _userService.RegisterUser(request);

                // Send mobile verification code
                await _verificationService.GenerateAndSendMobileVerificationCode(user.MobileNumber);

                return Ok(new
                {
                    success = true,
                    message = "Registration successful. Mobile verification code sent.",
                    nextStep = "verify-mobile"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpPost("send-mobile-verification")]
        public async Task<IActionResult> SendMobileVerification([FromBody] string icnNumber)
        {
            var user = await _userService.GetUserByICN(icnNumber);
            if (user == null)
                return NotFound(new { success = false, message = "User not found" });

            await _verificationService.GenerateAndSendMobileVerificationCode(user.MobileNumber);

            return Ok(new
            {
                success = true,
                message = "Mobile verification code sent",
                nextStep = "verify-mobile"
            });
        }

        [HttpPost("verify-mobile")]
        public async Task<IActionResult> VerifyMobile([FromBody] VerificationRequest request)
        {
            var isVerified = await _userService.VerifyUser(request.ICNNumber, request.Code, "mobile");

            if (!isVerified)
                return BadRequest(new { success = false, message = "Invalid verification code" });

            return Ok(new
            {
                success = true,
                message = "Mobile verified successfully",
                nextStep = "send-email-verification"
            });
        }

        [HttpPost("send-email-verification")]
        public async Task<IActionResult> SendEmailVerification([FromBody] string icnNumber)
        {
            var user = await _userService.GetUserByICN(icnNumber);
            if (user == null)
                return NotFound(new { success = false, message = "User not found" });

            await _verificationService.GenerateAndSendEmailVerificationCode(user.Email);

            return Ok(new
            {
                success = true,
                message = "Email verification code sent",
                nextStep = "verify-email"
            });
        }

        [HttpPost("verify-email")]
        public async Task<IActionResult> VerifyEmail([FromBody] VerificationRequest request)
        {
            var isVerified = await _userService.VerifyUser(request.ICNNumber, request.Code, "email");

            if (!isVerified)
                return BadRequest(new { success = false, message = "Invalid verification code" });

            return Ok(new
            {
                success = true,
                message = "Email verified successfully",
                nextStep = "accept-policy"
            });
        }

        [HttpPost("accept-policy")]
        public async Task<IActionResult> AcceptPolicy([FromBody] PolicyAcceptanceRequest request)
        {
            if (!request.Accept)
                return BadRequest(new { success = false, message = "Policy must be accepted to continue" });

            var success = await _userService.AcceptPolicy(request.ICNNumber);

            if (!success)
                return BadRequest(new { success = false, message = "Failed to accept policy" });

            return Ok(new
            {
                success = true,
                message = "Policy accepted successfully",
                nextStep = "set-pin"
            });
        }

        [HttpPost("set-pin")]
        public async Task<IActionResult> SetPin([FromBody] PinSetupRequest request)
        {
            if (request.Pin.Length != 6)
                return BadRequest(new { success = false, message = "PIN must be 6 digits" });

            var success = await _userService.SetPin(request.ICNNumber, request.Pin);

            if (!success)
                return BadRequest(new { success = false, message = "Failed to set PIN" });

            return Ok(new
            {
                success = true,
                message = "PIN set successfully",
                nextStep = "complete"
            });
        }
    }
}
