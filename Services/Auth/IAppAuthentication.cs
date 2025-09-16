

namespace Projet101.Services.Auth
{
    public interface IAppAuthentication
    {
        public Task<object?> LoginAsync(UserDto request);
       
    }
}
