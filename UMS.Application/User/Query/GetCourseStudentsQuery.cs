using MediatR;

namespace UMS.Application.User.Query;

public class GetCourseStudentsQuery : IRequest<List<Domain.Models.User>>
{
    public int CourseId;

    public GetCourseStudentsQuery(int courseId)
    {
        CourseId = courseId;
    }
}