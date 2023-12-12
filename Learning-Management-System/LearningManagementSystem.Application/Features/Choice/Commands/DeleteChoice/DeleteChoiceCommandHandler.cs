using MediatR;
using LearningManagementSystem.Application.Contracts.Interfaces;
using LearningManagementSystem.Application.Persistence.Courses;

namespace LearningManagementSystem.Application.Features.Choice.Commands.DeleteChoice
{
    public class DeleteChoiceCommandHandler : IRequestHandler<DeleteChoiceCommand, DeleteChoiceCommandResponse>
    {
        private readonly IChoiceRepository choiceRepository;
        private readonly IQuestionRepository questionRepository;
        private readonly ICourseRepository courseRepository;
        private readonly ICurrentUserService userService;

        public DeleteChoiceCommandHandler(IChoiceRepository choiceRepository, IQuestionRepository questionRepository, ICourseRepository courseRepository, ICurrentUserService userService)
        {
            this.choiceRepository = choiceRepository;
            this.questionRepository = questionRepository;
            this.courseRepository = courseRepository;
            this.userService = userService;
        }

        public async Task<DeleteChoiceCommandResponse> Handle(DeleteChoiceCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteChoiceCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new DeleteChoiceCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var choice = await choiceRepository.FindByIdAsync(request.ChoiceId);

            if (!choice.IsSuccess)
            {
                return new DeleteChoiceCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { choice.Error }
                };
            }

            if (!userService.IsUserAdmin())
            {
                var question = await questionRepository.FindByIdAsync(choice.Value.QuestionId);

                if (!question.IsSuccess)
                {
                    return new DeleteChoiceCommandResponse
                    {
                        Success = false,
                        ValidationsErrors = new List<string> { question.Error }
                    };
                }

                var course = await courseRepository.FindByIdAsync(question.Value.Chapter.CourseId);

                if (!course.IsSuccess)
                {
                    return new DeleteChoiceCommandResponse
                    {
                        Success = false,
                        ValidationsErrors = new List<string> { course.Error }
                    };
                }

                var userId = Guid.Parse(userService.UserId);

                if (course.Value.ProfessorId != userId)
                {
                    return new DeleteChoiceCommandResponse
                    {
                        Success = false,
                        ValidationsErrors = ["User doesn't own the course"]
                    };
                }
            }

            await choiceRepository.DeleteAsync(request.ChoiceId);

            return new DeleteChoiceCommandResponse
            {
                Success = true
            };
        }
    }
}
