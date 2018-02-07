namespace Nibo.Framework.WebApi.Auth
{
    public interface IAuthOptions
    {
        string PassportUrl { get; }
        string JwtPassword { get; }
        string ClientId { get; }
        string ClientSecret { get; }
        string ReturnUrl { get; }
    }
}