using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UMS.Application.ClassEnrollment.Commands;
using UMS.Application.Entities.TeacherPerCoursePerSession.Commands;
using UMS.Application.User.Query;
using UMS.WebAPI.DTO;

namespace UMS.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
   private readonly IMediator _mediator;
       private readonly IMapper _mapper;
       
       public UsersController(IMediator mediator, IMapper mapper)
       {
           _mediator = mediator;
           _mapper = mapper;
       }
       
       // GET
       [HttpGet("GetCourses")]
       public async Task<IActionResult> GetCourseStudents([FromHeader] int courseId)
       {
           return Ok(await _mediator.Send(new GetCourseStudentsQuery(courseId)));
       }
       
       // Teacher to Course Registration
       [HttpPost("RegisterToCourse")]
       public async Task<IActionResult> InsertTeacherPerCourse([FromBody] RegisterToCourse regToCourse)
       {
           return Ok(await _mediator.Send(new RegisterTeacherToCourseCommand(regToCourse)));
       }

       //POST
       [HttpPost("EnrollCourse")]
       public async Task<IActionResult> EnrollCourse([FromBody] EnrollClass enrollClass)
       {
           return Ok(await _mediator.Send(new EnrollClassCommand(enrollClass)));
       }
}