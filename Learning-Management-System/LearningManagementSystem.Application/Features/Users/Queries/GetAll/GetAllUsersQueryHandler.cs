using LearningManagementSystem.Application.Persistence.Users;
using MediatR;

namespace LearningManagementSystem.Application.Features.Users.Queries.GetAll
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, GetAllUsersResponse>
    {
        private readonly IUserRepository repository;
        public GetAllUsersQueryHandler(IUserRepository userRepository)
        {
            this.repository = userRepository;
        }
        public async Task<GetAllUsersResponse> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            GetAllUsersResponse response = new();
            var result = await repository.GetAllAsync();
            if (result.IsSuccess)
            {
                response.Users = result.Value.Select(u => new UserDto
                {
                    UserId = u.UserId,
                    FirstName = u.FirstName, // Schimbat aici
                    LastName = u.LastName,   // Schimbat aici
                    Email = u.Email,         // Schimbat aici
                    Password = u.Password,   // Schimbat aici
                    Role = u.Role // Convertit enum la string
                }).ToList();
            }
            return response;
        }
    }
}
