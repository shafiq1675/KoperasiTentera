using Microsoft.Extensions.Caching.Memory;

namespace KoperasiTentera.Services
{
    public interface IVerificationService
    {
        Task<string> GenerateAndSendMobileVerificationCode(string mobileNumber);
        Task<string> GenerateAndSendEmailVerificationCode(string email);
        Task<bool> VerifyMobileCode(string mobileNumber, string code);
        Task<bool> VerifyEmailCode(string email, string code);
    }
    public class VerificationService : IVerificationService
    {
        private readonly IMemoryCache _cache;
        private readonly TimeSpan _codeExpiry = TimeSpan.FromMinutes(5);

        public VerificationService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public async Task<string> GenerateAndSendMobileVerificationCode(string mobileNumber)
        {
            var code = GenerateRandomCode();
            _cache.Set($"MobileVerification_{mobileNumber}", code, _codeExpiry);

            // TODO: Integrate with SMS service (Twilio, AWS SNS, etc.)
            await SendSms(mobileNumber, $"Your verification code is: {code}");

            return code;
        }

        public async Task<string> GenerateAndSendEmailVerificationCode(string email)
        {
            var code = GenerateRandomCode();
            _cache.Set($"EmailVerification_{email}", code, _codeExpiry);

            // TODO: Integrate with Email service (SendGrid, SMTP, etc.)
            await SendEmail(email, "Verify Your Email", $"Your verification code is: {code}");

            return code;
        }

        public Task<bool> VerifyMobileCode(string mobileNumber, string code)
        {
            var storedCode = _cache.Get<string>($"MobileVerification_{mobileNumber}");
            return Task.FromResult(storedCode != null && storedCode == code);
        }

        public Task<bool> VerifyEmailCode(string email, string code)
        {
            var storedCode = _cache.Get<string>($"EmailVerification_{email}");
            return Task.FromResult(storedCode != null && storedCode == code);
        }

        private string GenerateRandomCode() => new Random().Next(100000, 999999).ToString();

        private Task SendSms(string mobileNumber, string message)
        {
            // TODO: Replace with actual SMS provider (Twilio, AWS SNS, etc.)
            Console.WriteLine($"SMS to {mobileNumber}: {message}");
            return Task.CompletedTask;
        }

        private Task SendEmail(string email, string subject, string body)
        {
            // TODO: Replace with actual Email service (SendGrid, SMTP, etc.)
            Console.WriteLine($"Email to {email}: {subject} - {body}");
            return Task.CompletedTask;
        }
    }

}
