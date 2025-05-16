using System;
using KoperasiTentera.Models;

namespace KoperasiTentera.Services
{
    public interface IUserService
    {
        Task<bool> IsUserRegistered(string icnNumber);
        Task<User> RegisterUser(RegisterRequest request);
        Task<bool> VerifyUser(string icnNumber, string code, string verificationType);
        Task<bool> AcceptPolicy(string icnNumber);
        Task<bool> SetPin(string icnNumber, string pin);
        Task<User> GetUserByICN(string icnNumber);
    }
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly IVerificationService _verificationService;

        public UserService(AppDbContext context, IVerificationService verificationService)
        {
            _context = context;
            _verificationService = verificationService;
        }

        public async Task<bool> IsUserRegistered(string icnNumber)
        {
            return await _context.Users.AnyAsync(u => u.ICNNumber == icnNumber);
        }

        public async Task<User> RegisterUser(RegisterRequest request)
        {
            if (await IsUserRegistered(request.ICNNumber))
                throw new Exception("User already registered");

            var user = new User
            {
                ICNNumber = request.ICNNumber,
                Name = request.Name,
                MobileNumber = request.MobileNumber,
                Email = request.Email,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<bool> VerifyUser(string icnNumber, string code, string verificationType)
        {
            var user = await GetUserByICN(icnNumber);
            if (user == null) return false;

            var isVerified = await _verificationService.VerifyCode(
                verificationType == "mobile" ? user.MobileNumber : user.Email,
                code,
                verificationType
            );

            if (isVerified)
            {
                if (verificationType == "mobile")
                    user.IsMobileVerified = true;
                else
                    user.IsEmailVerified = true;

                user.UpdatedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }

            return isVerified;
        }

        public async Task<bool> AcceptPolicy(string icnNumber)
        {
            var user = await GetUserByICN(icnNumber);
            if (user == null) return false;

            user.HasAcceptedPolicy = true;
            user.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> SetPin(string icnNumber, string pin)
        {
            var user = await GetUserByICN(icnNumber);
            if (user == null) return false;

            user.PinNumber = pin;
            user.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<User> GetUserByICN(string icnNumber)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.ICNNumber == icnNumber);
        }
    }
}
