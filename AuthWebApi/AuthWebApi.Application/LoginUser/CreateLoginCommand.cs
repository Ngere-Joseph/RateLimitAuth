using AuthWebApi.AuthWebApi.Core;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AuthWebApi.AuthWebApi.Application.LoginUser
{
    public class CreateLoginCommand : IRequest<SignInResult>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class LoginCommandHandler : IRequestHandler<CreateLoginCommand, SignInResult>
    {
        private readonly SignInManager<AppUser> _signInManager;

        public LoginCommandHandler(SignInManager<AppUser> signInManager)
            => _signInManager = signInManager;

        public async Task<SignInResult> Handle(CreateLoginCommand request, CancellationToken cancellationToken)
        {
            return await _signInManager.PasswordSignInAsync(request.Username, request.Password, false, false);
        }
    }
}