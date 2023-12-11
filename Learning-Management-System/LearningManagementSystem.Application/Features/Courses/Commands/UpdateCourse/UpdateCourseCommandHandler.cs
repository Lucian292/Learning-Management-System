using LearningManagementSystem.Application.Contracts.Interfaces;
using LearningManagementSystem.Application.Features.Courses.Commands.DeleteCourse;
using LearningManagementSystem.Application.Features.Courses.Commands.UpdateCourse;
using LearningManagementSystem.Application.Persistence.Courses;
using MediatR;

namespace LearningManagementSystem.Application.Features.Courses.Commands.UpdateCourseCommand
{
    public class UpdateCourseCommandHandler : IRequestHandler<UpdateCourseCommand, UpdateCourseCommandResponse>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ICurrentUserService userService;

        public UpdateCourseCommandHandler(ICourseRepository courseRepository, ICurrentUserService userService)
        {
            this._courseRepository = courseRepository;
            this.userService = userService;
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

            var userId = Guid.Parse(userService.UserId);

            if (course.Value.ProfessorId != userId && !userService.IsUserAdmin())
            {
                return new UpdateCourseCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { "User doesn't own this course" }
                };
            }

            var updateCourseDto = request.UpdateCourseDto;

            course.Value.Update(updateCourseDto.Title, updateCourseDto.Description);

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
