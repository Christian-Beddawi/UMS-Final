using MediatR;

namespace UMS.Application.User.Query;

public class GetCourseStudentsQuery : IRequest<List<Domain.Models.User>>
{
    public string CourseName;

    public GetCourseStudentsQuery(string courseName)
    {
        CourseName = courseName;
    }
}