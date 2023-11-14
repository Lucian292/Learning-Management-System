using LearningManagementSystem.Application.Persistence.Courses;
using MediatR;

namespace LearningManagementSystem.Application.Features.Courses.Commands.DeleteCourse
{
    public class DeleteCourseCommandHandler : IRequestHandler<DeleteCourseCommand, DeleteCourseCommandResponse>
    {
        private readonly ICourseRepository repository;

        public DeleteCourseCommandHandler(ICourseRepository repository)
        {
            this.repository = repository;
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

            var category = await repository.FindByIdAsync(request.CourseId);
            if (!category.IsSuccess)
            {
                return new DeleteCourseCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { category.Error }
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
