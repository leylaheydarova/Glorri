using Glorri.API.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace Glorri.API.Services.Implements
{
    public class OtpService : IOtpService
    {
        readonly IMemoryCache _cache;

        public OtpService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public int GenerateOtpCode(string username)
        {
            var random = new Random();
            var otp = random.Next(1000, 9999);

            _cache.Set(username, otp, TimeSpan.FromMinutes(5));

            return otp;
        }

        public bool VerifyOtp(string username, int otp)
        {
            var result = _cache.TryGetValue(username, out int cachedOtp);
            if (cachedOtp == otp)
            {
                _cache.Remove(username);
                return true;
            }
            return false;
        }
    }
}
