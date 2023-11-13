using LearningManagementSystem.Application.Persistence.Users;
using MediatR;

namespace LearningManagementSystem.Application.Features.Users.Queries.GetById
{
    public class GetByIdUserQueryHandler : IRequestHandler<GetByIdUserQuery, UserDto>
    {
        private readonly IUserRepository repository;

        public GetByIdUserQueryHandler(IUserRepository repository)
        {
            this.repository = repository;
        }

        public async Task<UserDto> Handle(GetByIdUserQuery request, CancellationToken cancellationToken)
        {
            var userResult = await repository.FindByIdAsync(request.Id);

            if (userResult.IsSuccess)
            {
                var user = userResult.Value;

                return new UserDto
                {
                    UserId = user.UserId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Password = user.Password,
                    Role = user.Role // Convertit enum la string
                };
            }
            else
            {
                return new UserDto();
            }
        }
    }
}
