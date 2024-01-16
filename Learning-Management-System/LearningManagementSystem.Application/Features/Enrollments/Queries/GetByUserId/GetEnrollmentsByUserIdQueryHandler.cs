using MediatR;
using LearningManagementSystem.Application.Contracts.Interfaces;
using LearningManagementSystem.Application.Persistence.Courses;
using LearningManagementSystem.Application.Features.Courses.Queries;
using LearningManagementSystem.Application.Features.Questions.Queries.GetQuestionById;

namespace LearningManagementSystem.Application.Features.Enrollments.Queries.GetByUserId
{
    public class GetEnrollmentsByUserIdQueryHandler : IRequestHandler<GetEnrollmentsByUserIdQuery, GetEnrollmentsByUserIdResponse>
    {
        private readonly IEnrollmentRepository enrollmentRepository;
        private readonly ICourseRepository courseRepository; // Adăugăm dependența pentru repository-ul de cursuri
        private readonly ICurrentUserService currentUserService;

        public GetEnrollmentsByUserIdQueryHandler(IEnrollmentRepository enrollmentRepository, ICourseRepository courseRepository, ICurrentUserService currentUserService)
        {
            this.enrollmentRepository = enrollmentRepository ?? throw new ArgumentNullException(nameof(enrollmentRepository));
            this.courseRepository = courseRepository ?? throw new ArgumentNullException(nameof(courseRepository));
            this.currentUserService = currentUserService ?? throw new ArgumentNullException(nameof(currentUserService));
        }

        public async Task<GetEnrollmentsByUserIdResponse> Handle(GetEnrollmentsByUserIdQuery request, CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(currentUserService.UserId);

            var response = new GetEnrollmentsByUserIdResponse();

            var result = await enrollmentRepository.GetEnrollmentsByUserIdAsync(userId);

            if (result.IsSuccess)
            {
                response.Enrollments = new List<EnrollmentDto>();

                foreach (var enrollment in result.Value)
                {
                    // Obținem informații despre curs utilizând repository-ul de cursuri
                    var courseResult = await courseRepository.FindByIdAsync(enrollment.CourseId);

                    if (courseResult.IsSuccess)
                    {
                        var courseDto = new CourseDto
                        {
                            Title = courseResult.Value.Title,
                            Description = courseResult.Value.Description,
                            CategoryId = courseResult.Value.CategoryId,
                            Chapters = courseResult.Value.Chapters.Select(c => new Chapters.Queries.ChapterDto
                            {
                                ChapterId = c.ChapterId,
                                Questions = c.Quizz.Select(q => new QuestionDto
                                {
                                    QuestionId = q.QuestionId,
                                }).ToList()
                            }).ToList()
                        };

                        var enrollmentDto = new EnrollmentDto
                        {
                            EnrollmentId = enrollment.EnrollmentId,
                            UserId = enrollment.UserId,
                            CourseId = enrollment.CourseId,
                            Progress = enrollment.Progress,
                            Course = courseDto
                        };

                        response.Enrollments.Add(enrollmentDto);
                    }
                }
            }

            return response;
        }
    }
}
