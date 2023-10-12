using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.Lessons.Create;
using SchoolManagement.Application.Lessons.Delete;
using SchoolManagement.Application.Lessons.Get;
using SchoolManagement.Contracts.Lessons;

namespace SchoolManagement.Api.Controllers
{
    public class LessonsController : ApiController
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public LessonsController(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLessonsAsync()
        {
            var query = new GetAllLessonsQuery();
            var result = await _sender.Send(query);

            var mappedLessons = result.Value.Select(lesson => _mapper.Map<LessonResponse>(lesson)).ToList();

            return result.Match(
                result => Ok(mappedLessons),
                Problem
            );
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetLessonByIdAsync(string id)
        {
            var query = new GetLessonQuery(id);
            var result = await _sender.Send(query);

            return result.Match(
                result => Ok(_mapper.Map<LessonResponse>(result)),
                Problem
            );
        }

        [HttpPost]
        public async Task<IActionResult> CreateLessonAsync(CreateLessonRequest request)
        {
            var command = _mapper.Map<CreateLessonCommand>(request);
            var result = await _sender.Send(command);

            return result.Match(
                result => NoContent(),
                Problem
            );
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteLessonAsync(string id)
        {
            var command = new DeleteLessonCommand(id);
            var result = await _sender.Send(command);

            return result.Match(
                result => NoContent(),
                Problem
            );
        }
    }
}