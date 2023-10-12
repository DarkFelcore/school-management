using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.Schools.Create;
using SchoolManagement.Application.Schools.Delete;
using SchoolManagement.Application.Schools.Get;
using SchoolManagement.Application.Schools.Update;
using SchoolManagement.Contracts.Schools;

namespace SchoolManagement.Api.Controllers
{
    public class SchoolsController : ApiController
    {
        private readonly IMapper _mapper;
        private readonly ISender _mediator;

        public SchoolsController(IMapper mapper, ISender mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSchoolsAsync()
        {
            var query = new GetAllSchoolsQuery();
            var result = await _mediator.Send(query);

            var mappedSchools = result.Value.Select(school => _mapper.Map<SchoolResponse>(school)).ToList();

            return result.Match(
                result => Ok(mappedSchools),
                Problem
            );
        }

        
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetSchoolByIdAsync(string id)
        {
            var query = new GetSchoolQuery(id);
            var result = await _mediator.Send(query);

            return result.Match(
                result => Ok(_mapper.Map<SchoolResponse>(result)),
                Problem
            );
        }

        [HttpPost]
        public async Task<IActionResult> CreateSchoolAsync(CreateSchoolRequest request)
        {
            var command = _mapper.Map<CreateSchoolCommand>(request);
            var result = await _mediator.Send(command);

            return result.Match(
                result => NoContent(),
                Problem
            );
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateSchoolAsync(UpdateSchoolRequest request, string id)
        {
            var command = _mapper.Map<UpdateSchoolCommand>((request, id));
            var result = await _mediator.Send(command);

            return result.Match(
                result => Ok(_mapper.Map<SchoolResponse>(result)),
                Problem
            );
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteSchoolAsync(string id)
        {
            var command =  new DeleteSchoolCommand(id);
            var result = await _mediator.Send(command);

            return result.Match(
                result => NoContent(),
                Problem
            );
        }
    }
}