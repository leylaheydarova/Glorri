namespace Glorri.API.Services.Interfaces
{
    public interface IOtpService
    {
        int GenerateOtpCode(string username);
        bool VerifyOtp(string username, int otp);
    }
}
