using MediatR;
using UMS.Application.Common;
using UMS.Infrastructure.Abstraction.EmailServiceInterface;
using UMS.Persistence;

namespace UMS.Application.ClassEnrollment.Commands;

public class EnrollClassHandler : IRequestHandler<EnrollClassCommand, string>
{
    private readonly umsContext _context;
    private readonly ICommonServices _common;
    private readonly IEEmailService _emailSender;

    public EnrollClassHandler(ICommonServices common, umsContext context, IEEmailService emailSender)
    {
        _context = context;
        _common = common;
        _emailSender = emailSender;
    }
    public async Task<string> Handle(EnrollClassCommand request, CancellationToken cancellationToken)
    {
        DateTime currentDate = DateTime.Now;
        var range = _common.GetCourseDateRange(request.EnrollmentInfo.ClassName);
        DateOnly? startDate = range?.LowerBound;
        DateOnly? endDate = range?.UpperBound;
        if (startDate > DateOnly.FromDateTime(DateTime.Now) || DateOnly.FromDateTime(DateTime.Now) > endDate)
        {
            return "Not allowed to enroll at this time !";
        }

        if (!_common.CheckCourseCapacity(request.EnrollmentInfo.ClassName))
            return "The class is full!";
        var classId = _common.GetClassId(request.EnrollmentInfo);
        var res = await _context.AddAsync(new Domain.Models.ClassEnrollment()
        {
            StudentId = request.EnrollmentInfo.StudentId,
            ClassId = classId
        });
        _context.SaveChanges();
        var emailAddress = _common.GetUserEmail(request.EnrollmentInfo.StudentId);
        string emailBody = "Dear student,\tKindly note note that you have been successfully enrolled to the "
            + request.EnrollmentInfo.ClassName + " course.\tBest";
        await _emailSender.SendEmailAsync(emailAddress, "Course Enrollment", emailBody);
        return "Enrolled successfully!";
    }
    
}