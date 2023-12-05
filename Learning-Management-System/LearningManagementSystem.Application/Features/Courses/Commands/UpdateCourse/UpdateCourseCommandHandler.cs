using LearningManagementSystem.Application.Features.Courses.Commands.UpdateCourse;
using LearningManagementSystem.Application.Persistence.Courses;
using MediatR;

namespace LearningManagementSystem.Application.Features.Courses.Commands.UpdateCourseCommand
{
    public class UpdateCourseCommandHandler : IRequestHandler<UpdateCourseCommand, UpdateCourseCommandResponse>
    {
        private readonly ICourseRepository _courseRepository;
        public UpdateCourseCommandHandler(ICourseRepository courseRepository)
        {
            this._courseRepository = courseRepository;
        }

        public async Task<UpdateCourseCommandResponse> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
        {
            var course = await _courseRepository.FindByIdAsync(request.CourseId);

            if (!course.IsSuccess)
            {
                return new UpdateCourseCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { course.Error }
                };
            }

            var updateCourseDto = request.UpdateCourseDto;

            var result = course.Value.Update(updateCourseDto.Title, updateCourseDto.Description);

            if (!result.IsSuccess)
            {
                return new UpdateCourseCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { result.Error }
                };
            }

            await _courseRepository.UpdateAsync(course.Value);

            return new UpdateCourseCommandResponse
            {
                Success = true,
                UpdateCourse = new UpdateCourseDto
                {
                    Title = course.Value.Title,
                    Description = course.Value.Description
                }
            };
        }
    }
}
