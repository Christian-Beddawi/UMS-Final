using MediatR;
using UMS.Persistence;

namespace UMS.Application.User.Query;

public class GetCourseStudentsHandler : IRequestHandler<GetCourseStudentsQuery,List<Domain.Models.User>>
{
    private readonly umsContext _context;
    
    public GetCourseStudentsHandler(umsContext context)
    {
        _context = context;
    }
    public async Task<List<Domain.Models.User>> Handle(GetCourseStudentsQuery request, CancellationToken cancellationToken)
    {
        return _context.ClassEnrollments.Where(u => u.Class.Course.Name == request.CourseName)
            .Select(u=>u.Student).ToList();
    }
}