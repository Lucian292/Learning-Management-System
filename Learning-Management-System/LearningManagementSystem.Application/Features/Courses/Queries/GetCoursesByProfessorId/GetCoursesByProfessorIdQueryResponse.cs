using LearningManagementSystem.Application.Responses;

namespace LearningManagementSystem.Application.Features.Courses.Queries.GetByProfessorId
{
    public class GetCoursesByProfessorIdQueryResponse : BaseResponse
    {
        public GetCoursesByProfessorIdQueryResponse() : base()
        {  
        }

        public List<CourseDto> Courses { get; set; } = [];
    }
}
