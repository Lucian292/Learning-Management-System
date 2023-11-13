using LearningManagementSystem.Application.Features.Enrollments.Queries.GetAll;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningManagementSystem.Application.Features.Enrollments.Queries.GetAll
{
    public class GetAllEnrollmentsQuery : IRequest<GetAllEnrollmentsResponse>
    {
    }
}