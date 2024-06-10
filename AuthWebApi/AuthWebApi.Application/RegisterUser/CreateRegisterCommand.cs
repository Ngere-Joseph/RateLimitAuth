using AuthWebApi.AuthWebApi.Core;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AuthWebApi.AuthWebApi.Application.RegisterUser
{
    public class CreateRegisterCommand : IRequest<IdentityResult>
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class RegisterCommandHandler : IRequestHandler<CreateRegisterCommand, IdentityResult>
    {
        private readonly UserManager<AppUser> _userManager;
        public RegisterCommandHandler(UserManager<AppUser> userManager)
            => _userManager = userManager;

        public async Task<IdentityResult> Handle(CreateRegisterCommand request, CancellationToken cancellationToken)
        {
            var user = new AppUser { UserName = request.Username, Email = request.Email };
            return await _userManager.CreateAsync(user, request.Password);
        }
    }
}
