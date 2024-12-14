using AutoMapper;
using MailKit.Net.Smtp;
using MailKit.Security;
using MediatR;
using Microsoft.Extensions.Configuration;
using MimeKit;
using Usermanagement.Domain;
using Usermanagement.Domain.User;
using Usermanagement.Infrastructure.Interfaces;

namespace Usermanagement.Application.Register
{
    public record RegisterationCommand(string Email, string Password, string MobileNumber, int DeliveryType) : IRequest<Result<object>>;

    public class RegisterationCommandHandler : IRequestHandler<RegisterationCommand, Result<object>>
    {
        private readonly IIdentityRepository _identityRepository;
        private readonly IMapper _mapper;
        private readonly string _email;
        private readonly string _password;
        private readonly string _userName;
        private readonly string _smtServerAddress;
        private readonly string _subject;
        private readonly bool _isTest;


        public RegisterationCommandHandler(IIdentityRepository identityRepository, IMapper mapper, IConfiguration configuration)
        {
            _identityRepository = identityRepository;
            _mapper = mapper;
            _email = configuration["Email"];
            _password = configuration["Password"];
            _userName = configuration["UserName"];
            _smtServerAddress = configuration["SmtServerAddress"];
            _isTest = bool.Parse(configuration["IsTest"]);
        }

        public async Task<Result<object>> Handle(RegisterationCommand command, CancellationToken cancellationToken)
        {
            //Here we just save a registration code and check no user registration for this email
            var userInfo = _identityRepository.GetUserByEmail(command.Email);
            if (userInfo == null)
            {
                var activationCode = new ActivationCode(command.Email);
                activationCode.GenerateCode(_isTest);

                await _identityRepository.UpdateActivationCodes(command.Email);
                await _identityRepository.AddActivationCode(activationCode);

                var sendActivationCodeRequest = new SendActivationCodeModel(command.Email, _userName, _subject, activationCode.Code
                    , command.MobileNumber, command.DeliveryType);

                var sendActivationCodeStatus = await SendActivationCode(sendActivationCodeRequest);
                if (sendActivationCodeStatus)
                    return Result<object>.Success(true);
                else
                    return Result<object>.Failure("-1", "Error");
            }
            else
                return Result<object>.Failure("-1", "Error");
        }

        public async Task<bool> SendActivationCode(SendActivationCodeModel model)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_userName, _email));
            message.To.Add(new MailboxAddress(model.UserName, model.UserEmail));
            message.Subject = model.Subject;
            message.Body = new TextPart("plain") { Text = model.MessageBody };

            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect(_smtServerAddress, 587, SecureSocketOptions.StartTls);
                    client.Authenticate(_email, _password);

                    client.Send(message);
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
                finally
                {
                    client.Disconnect(true);
                }
            }
        }

        public record SendActivationCodeModel(string UserEmail, string UserName, string Subject, string MessageBody, string MobileNumber, int DeliveryType);
    }
}
