namespace AuthWebApi.AuthWebApi.Application.RoleUser
{
    using global::AuthWebApi.AuthWebApi.Core;
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    using System.Threading;
    using System.Threading.Tasks;

    public class AssignRoleCommand : IRequest<IdentityResult>
    {
        public string Username { get; set; }
        public string Role { get; set; }
    }

    public class AssignRoleCommandHandler : IRequestHandler<AssignRoleCommand, IdentityResult>
    {
        private readonly UserManager<AppUser> _userManager;

        public AssignRoleCommandHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> Handle(AssignRoleCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.Username);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = $"User {request.Username} not found" });
            }

            return await _userManager.AddToRoleAsync(user, request.Role);
        }
    }

}
