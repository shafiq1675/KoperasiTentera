using Microsoft.Extensions.Caching.Memory;

namespace KoperasiTentera.Services
{
    public interface IVerificationService
    {
        Task<string> GenerateAndSendMobileVerificationCode(string mobileNumber);
        Task<string> GenerateAndSendEmailVerificationCode(string email);
        Task<bool> VerifyCode(string identifier, string code, string verificationType);
    }

    public class VerificationService : IVerificationService
    {
        private readonly IMemoryCache _cache;
        private readonly ISmsService _smsService;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _config;

        public VerificationService(
            IMemoryCache cache,
            ISmsService smsService,
            IEmailService emailService,
            IConfiguration config)
        {
            _cache = cache;
            _smsService = smsService;
            _emailService = emailService;
            _config = config;
        }

        public async Task<string> GenerateAndSendMobileVerificationCode(string mobileNumber)
        {
            var code = GenerateRandomCode();
            var cacheKey = $"mobile-verification-{mobileNumber}";

            _cache.Set(cacheKey, code, TimeSpan.FromMinutes(5));

            await _smsService.SendSms(mobileNumber, $"Your verification code is: {code}");

            return code;
        }

        public async Task<string> GenerateAndSendEmailVerificationCode(string email)
        {
            var code = GenerateRandomCode();
            var cacheKey = $"email-verification-{email}";

            _cache.Set(cacheKey, code, TimeSpan.FromMinutes(5));

            await _emailService.SendEmail(email, "Verification Code", $"Your verification code is: {code}");

            return code;
        }

        public async Task<bool> VerifyCode(string identifier, string code, string verificationType)
        {
            var cacheKey = $"{verificationType}-verification-{identifier}";

            if (!_cache.TryGetValue(cacheKey, out string storedCode))
                return false;

            if (storedCode != code)
                return false;

            // Remove from cache after successful verification
            _cache.Remove(cacheKey);

            return true;
        }

        private string GenerateRandomCode()
        {
            var random = new Random();
            return random.Next(100000, 999999).ToString();
        }
    }
}
