using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.Students.Create;
using SchoolManagement.Application.Students.Delete;
using SchoolManagement.Application.Students.Get;
using SchoolManagement.Application.Students.Update;
using SchoolManagement.Contracts.Students;

namespace SchoolManagement.Api.Controllers
{
    public class StudentsController : ApiController
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;
        public StudentsController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            var query = new GetAllStudentsQuery();
            var result = await _mediator.Send(query);
            
            var mappedStudents = result.Value.Select(student => _mapper.Map<StudentResponse>(student)).ToList();

            return result.Match(
                result => Ok(mappedStudents),
                Problem
            );
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetStudentByIdAsync(string id)
        {
            var query = new GetStudentQuery(id);
            var result = await _mediator.Send(query);

            return result.Match(
                result => Ok(_mapper.Map<StudentResponse>(result)),
                Problem
            );
        }

        [HttpPost]
        public async Task<IActionResult> CreateStudent(CreateStudentRequest request)
        {
            var command = _mapper.Map<CreateStudentCommand>(request);
            var result = await _mediator.Send(command);

            return result.Match(
                result => NoContent(),
                Problem
            );
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateStudent(UpdateStudentRequest request, string id)
        {
            var command = _mapper.Map<UpdateStudentCommand>((request, id));
            var result = await _mediator.Send(command);

            return result.Match(
                result => Ok(_mapper.Map<StudentResponse>(result)),
                Problem
            );
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteStudent(string id)
        {
            var command = new DeleteStudentCommand(id);
            var result = await _mediator.Send(command);

            return result.Match(
                result => NoContent(),
                Problem
            );
        }

    }
}
