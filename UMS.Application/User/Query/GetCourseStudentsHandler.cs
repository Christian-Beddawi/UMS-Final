using MediatR;
using UMS.Domain.Models;
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
        var students = (from ep in _context.Courses
            join e in _context.TeacherPerCourses on ep.Id equals e.CourseId
            join s in _context.TeacherPerCoursePerSessionTimes on e.Id equals s.TeacherPerCourseId
            join t in _context.ClassEnrollments on s.Id equals t.ClassId
            join u in _context.Users on t.StudentId equals u.Id
            where ep.Id == request.CourseId
            select u).ToList();
        return students;
    }
}