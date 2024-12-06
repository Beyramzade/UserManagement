using AutoMapper;
using MediatR;
using Usermanagement.Domain;
using Usermanagement.Infrastructure.Interfaces;

namespace Usermanagement.Application.Register
{
    public record RegisterationCommand(string Email, string Password) : IRequest<Result<object>>;

    public class RegisterationCommandHandler : IRequestHandler<RegisterationCommand, Result<object>>
    {
        private readonly IIdentityRepository _identityRepository;
        private readonly IMapper _mapper;
        public RegisterationCommandHandler()
        {

        }

        public async Task<Result<object>> Handle(RegisterationCommand command, CancellationToken cancellationToken)
        {
            //Here we just save a registration code and check no user registration for this email
            var userInfo = _identityRepository.GetUserByEmail();
            if (userInfo != null)
            {
                await _identityRepository.UpdateActivationCodes(command.Email);
                await _identityRepository.AddActivationCode();

                var sendActivationCodeStatus = await SendActivationCode();
                if (sendActivationCodeStatus)
                    return Result<object>.Success(true);
                else
                    return Result<object>.Failure("-1", "Error");
            }
            else
                return Result<object>.Failure("-1", "Error");
        }

        private async Task<object> SendActivationCode()
        {

        }


    }
}
