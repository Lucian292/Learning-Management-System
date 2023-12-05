using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningManagementSystem.Application.Features.Courses.Queries.GetByProfessorId
{
    public class GetCoursesByProfessorIdQuery : IRequest<GetCoursesByProfessorIdQueryResponse>
    {
    }
}
