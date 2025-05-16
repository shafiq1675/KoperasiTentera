namespace KoperasiTentera.Services
{
    public interface IEmailService
    {
        Task<bool> SendEmail(string email, string subject, string message);
    }

    public class EmailService : IEmailService
    {
        private readonly ILogger<EmailService> _logger;

        public EmailService(ILogger<EmailService> logger)
        {
            _logger = logger;
        }

        public Task<bool> SendEmail(string email, string subject, string message)
        {
            // In a real implementation, this would connect to an email server
            // For demo purposes, we just log the message

            _logger.LogInformation($"DEMO EMAIL SERVICE: Sending to {email}");
            _logger.LogInformation($"Subject: {subject}");
            _logger.LogInformation($"Message: {message}");

            // Simulate a small delay
            Thread.Sleep(200);

            // Always return true for demo purposes
            return Task.FromResult(true);
        }
    }
}
