using Microsoft.AspNetCore.Http;
using Notepad.Application.Common.Repositories;
using System.Security.Claims;

namespace Notepad.Application.Common.Authentication
{
    public interface IIdentityProvider
    {
        Guid UserId { get; }
    }
    public class IdentityProvider : IIdentityProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserRepository _userRepository;
        public IdentityProvider(IHttpContextAccessor httpContextAccessor, 
            UserRepository userRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
        }

        public Guid UserId
        {
            get
            {
                var strId = _httpContextAccessor?.HttpContext?.User.FindFirstValue("Id"); 

                var id = Guid.Parse(strId);
                return id;
            }
        }
        

        /// Maybe if i need get user and modify him
        //public async Task<User> GetUser()
        //{
        //    if (UserId == Guid.Empty)
        //        return null;
        //    var user = await _userRepository.GetUserByIdAsync(UserId);
        //    if(user != null)
        //        return user;

        //    return null;
        //   // throw new UnauthorizedAccessException();  /// TODO : Make normal error throwing
        //}
    }
}
