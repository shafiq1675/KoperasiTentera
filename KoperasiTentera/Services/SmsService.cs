namespace KoperasiTentera.Services
{
    public interface ISmsService
    {
        Task<bool> SendSms(string mobileNumber, string message);
    }
    public class SmsService : ISmsService
    {
        private readonly ILogger<SmsService> _logger;

        public SmsService(ILogger<SmsService> logger)
        {
            _logger = logger;
        }

        public Task<bool> SendSms(string mobileNumber, string message)
        {
            // In a real implementation, this would connect to an SMS gateway
            // For demo purposes, we just log the message

            _logger.LogInformation($"DEMO SMS SERVICE: Sending to {mobileNumber}");
            _logger.LogInformation($"Message: {message}");

            // Simulate a small delay
            Thread.Sleep(200);

            // Always return true for demo purposes
            return Task.FromResult(true);
        }
    }
}
