using LearningManagementSystem.Application.Contracts.Interfaces;
using LearningManagementSystem.Application.Persistence.Courses;
using MediatR;

namespace LearningManagementSystem.Application.Features.Courses.Commands.DeleteCourse
{
    public class DeleteCourseCommandHandler : IRequestHandler<DeleteCourseCommand, DeleteCourseCommandResponse>
    {
        private readonly ICourseRepository repository;
        private readonly ICurrentUserService userService;

        public DeleteCourseCommandHandler(ICourseRepository repository, ICurrentUserService userService)
        {
            this.repository = repository;
            this.userService = userService;
        }

        public async Task<DeleteCourseCommandResponse> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteCourseCommandValidator();
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validatorResult.IsValid)
            {
                return new DeleteCourseCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var course = await repository.FindByIdAsync(request.CourseId);

            var userId = Guid.Parse(userService.UserId);

            if (course.Value.ProfessorId != userId)
            {
                return new DeleteCourseCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { "User doesn't own this course" }
                };
            }

            if (!course.IsSuccess)
            {
                return new DeleteCourseCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { course.Error }
                };
            }

            await repository.DeleteAsync(request.CourseId);

            return new DeleteCourseCommandResponse
            {
                Success = true
            };
        }
    }
}
