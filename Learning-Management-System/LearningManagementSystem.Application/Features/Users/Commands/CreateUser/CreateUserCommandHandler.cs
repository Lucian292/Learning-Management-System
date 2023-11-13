using LearningManagementSystem.Application.Persistence.Users;
using MediatR;
using LearningManagementSystem.Domain.Entities.Users;

namespace LearningManagementSystem.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CreateUserCommandResponse>
    {
        private readonly IUserRepository repository;

        public CreateUserCommandHandler(IUserRepository repository)
        {
            this.repository = repository;
        }

        public async Task<CreateUserCommandResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateUserCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new CreateUserCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var user = User.Create(request.FirstName, request.LastName, request.Email, request.Password, request.Role);
            if (!user.IsSuccess)
            {
                return new CreateUserCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { user.Error }
                };
            }

            await repository.AddAsync(user.Value);

            return new CreateUserCommandResponse
            {
                Success = true,
                User = new CreateUserDto
                {
                    UserId = user.Value.UserId,
                    FirstName = user.Value.FirstName,
                    LastName = user.Value.LastName,
                    Email = user.Value.Email,
                    Password = user.Value.Password,
                    Role = user.Value.Role
                }
            };
        }
    }
}

