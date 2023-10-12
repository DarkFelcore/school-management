using ErrorOr;
using MediatR;
using SchoolManagement.Application.Common.Interfaces;
using SchoolManagement.Domain.Common.Errors;
using SchoolManagement.Domain.Common.JoinedEntities;
using SchoolManagement.Domain.Students;

namespace SchoolManagement.Application.Students.Update
{
    public class UpdateStudentCommandHandler : IRequestHandler<UpdateStudentCommand, ErrorOr<Student>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateStudentCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<Student>> Handle(UpdateStudentCommand command, CancellationToken cancellationToken)
        {
            var student = await _unitOfWork.StudentRepository.GetByIdAsync(Guid.Parse(command.StudentId));

            if (student is null)
            {
                return Errors.Student.NotFound;
            }

            // Update the student object
            if (command.FirstName is not null)
            {
                student.FirstName = command.FirstName;
            }
            if (command.LastName is not null)
            {
                student.LastName = command.LastName;
            }

            var school = await _unitOfWork.SchoolRepository.GetSchoolByNameAsync(command.SchoolName);
            if (school is null)
            {
                return Errors.School.NotFound;
            }
            else
            {
                student.SchoolId = school.SchoolId;
                student.School = school;
            }

            if (command.Lessons is not null)
            {
                var updatedStudentLessons = new List<StudentLessons>();
                foreach (string lessonName in command.Lessons)
                {
                    var lesson = await _unitOfWork.LessonRepository.GetLessonByNameAsync(lessonName);

                    if (lesson is null)
                    {
                        return Errors.Lesson.NotFound;
                    }

                    updatedStudentLessons.Add(new StudentLessons
                    {
                        Student = student,
                        Lesson = lesson,
                        StudentId = student.StudentId,
                        LessonId = lesson.LessonId
                    });
                }

                student.StudentLessons = updatedStudentLessons;
            }

            // Persist the updated student object
            await _unitOfWork.StudentRepository.UpdateAsync(student);
            await _unitOfWork.CompleteAsync();

            // Return the object
            return student;
        }
    }
}