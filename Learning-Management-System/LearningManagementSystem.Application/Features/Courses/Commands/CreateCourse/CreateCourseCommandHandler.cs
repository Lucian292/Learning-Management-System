using LearningManagementSystem.Application.Contracts.Interfaces;
using LearningManagementSystem.Application.Persistence.Courses;
using LearningManagementSystem.Domain.Entities.Courses;
using MediatR;

namespace LearningManagementSystem.Application.Features.Couerses.Commands.CreateCourse
{
    public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, CreateCourseCommandResponse>
    {
        private readonly ICourseRepository repository;
        private readonly ICurrentUserService userService;

        public CreateCourseCommandHandler(ICourseRepository repository, ICurrentUserService userService)
        {
            this.repository = repository;
            this.userService = userService;
        }

        public async Task<CreateCourseCommandResponse> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateCourseCommandValidator();
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validatorResult.IsValid)
            {
                return new CreateCourseCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var userId = Guid.Parse(userService.UserId);

            var course = Course.Create(request.Title, request.Description, userId, request.CategoryId);
            
            if (!course.IsSuccess)
            {
                return new CreateCourseCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { course.Error }
                };
            }

            await repository.AddAsync(course.Value);

            return new CreateCourseCommandResponse
            {
                Success = true,
                Course = new CreateCourseDto
                {
                    CourseId = course.Value.CourseId,
                    Title = course.Value.Title,
                    Description = course.Value.Description,
                    UserId = course.Value.ProfessorId,
                    CategoryId = course.Value.CategoryId,
                }
            };
        }
    }
}
