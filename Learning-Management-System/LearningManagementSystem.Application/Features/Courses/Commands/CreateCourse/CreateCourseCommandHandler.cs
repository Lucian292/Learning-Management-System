using LearningManagementSystem.Application.Persistence.Courses;
using LearningManagementSystem.Domain.Entities.Courses;
using MediatR;

namespace LearningManagementSystem.Application.Features.Couerses.Commands.CreateCourse
{
    public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, CreateCourseCommandResponse>
    {
        private readonly ICourseRepository repository;

        public CreateCourseCommandHandler(ICourseRepository repository)
        {
            this.repository = repository;
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

            var course = Course.Create(request.Title, request.Description, request.UserName, request.CategoryId);
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
                    UserName = course.Value.UserName,
                    CategoryId = course.Value.CategoryId,
                }
            };
        }
    }
}
