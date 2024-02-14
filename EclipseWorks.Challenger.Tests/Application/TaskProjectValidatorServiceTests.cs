using AutoFixture.Idioms;
using EclipseWorks.Challenger.Application.Services;
using EclipseWorks.Challenger.Domain.Entities;
using EclipseWorks.Challenger.Domain.Enums;
using EclipseWorks.Challenger.Tests.AutoFixture;
using FluentAssertions;
using NSubstitute;

namespace EclipseWorks.Challenger.Tests.Application
{
    public class TaskProjectValidatorServiceTests
    {
        [Theory, AutoNSubstituteData]
        public void Sut_Should_GuardItsClause(GuardClauseAssertion assertion)
        {
            assertion.Verify(typeof(TaskProjectValidatorService).GetConstructors());
        }

        private List<TaskProject> GetMockTaskProjectsFalse()
        {

            var tasks = new List<TaskProject>();

            tasks.Add(new TaskProject() { Description = "teste", 
                                         Status = 2, 
                                         IdOwner = 1, 
                                         IdPriority = 0, 
                                         DueDate = DateTime.Now, 
                                         IdProject = 1, 
                                         IdTask = 1, 
                                         Title = "teste" });

            tasks.Add(new TaskProject()
                                        {
                                            Description = "teste2",
                                            Status = 2,
                                            IdOwner = 1,
                                            IdPriority = 0,
                                            DueDate = DateTime.Now,
                                            IdProject = 1,
                                            IdTask = 2,
                                            Title = "teste2"
                                        });

            return tasks;
        }


        private List<TaskProject> GetMockTaskProjectsTrue()
        {

            var tasks = new List<TaskProject>();

            tasks.Add(new TaskProject()
            {
                Description = "teste",
                Status = 2,
                IdOwner = 1,
                IdPriority = 0,
                DueDate = DateTime.Now,
                IdProject = 1,
                IdTask = 1,
                Title = "teste"
            });

            tasks.Add(new TaskProject()
            {
                Description = "teste2",
                Status = 0,
                IdOwner = 1,
                IdPriority = 0,
                DueDate = DateTime.Now,
                IdProject = 1,
                IdTask = 2,
                Title = "teste2"
            });



            tasks.Add(new TaskProject()
            {
                Description = "teste3",
                Status = 0,
                IdOwner = 1,
                IdPriority = 0,
                DueDate = DateTime.Now,
                IdProject = 1,
                IdTask = 2,
                Title = "teste3"
            });

            return tasks;
        }


        [Theory, AutoNSubstituteData]
        public async Task HasTaskPending_Should_Return_False(TaskProjectValidatorService sut)
        {

            var idProject = 1;

            sut._unitOfWork.TaskProjects.GetByProject(idProject).Returns(GetMockTaskProjectsFalse());

            var result = await sut.HasTaskPending(idProject);

            result.Should().BeFalse();

        }


        [Theory, AutoNSubstituteData]
        public async Task HasTaskPending_Should_Return_True(TaskProjectValidatorService sut)
        {

            var idProject = 1;

            sut._unitOfWork.TaskProjects.GetByProject(idProject).Returns(GetMockTaskProjectsTrue());

            var result = await sut.HasTaskPending(idProject);

            result.Should().BeTrue();

        }



        [Theory, AutoNSubstituteData]
        public async Task ExceedMaximumTask_Should_Return_True(TaskProjectValidatorService sut)
        {

            var idProject = 1;


            var numberMaxTask = 2;

            sut._unitOfWork.TaskProjects.GetByProject(idProject).Returns(GetMockTaskProjectsTrue());

            var result = await sut.ExceedMaximumTask(idProject,numberMaxTask);

            result.Should().BeTrue();

        }


        [Theory, AutoNSubstituteData]
        public async Task ExceedMaximumTask_Should_Return_False(TaskProjectValidatorService sut)
        {

            var idProject = 1;

            var numberMaxTask = 10;

            sut._unitOfWork.TaskProjects.GetByProject(idProject).Returns(GetMockTaskProjectsTrue());

            var result = await sut.ExceedMaximumTask(idProject, numberMaxTask);

            result.Should().BeFalse();

        }

        [Theory, AutoNSubstituteData]
        public void  NotAllowedPriorityValue_Should_Return_True_When_IdPriority_Low(TaskProjectValidatorService sut)
        {

            var idPriority = (int)EnumPriorityTask.Low;

            var result = sut.NotAllowedPriorityValue(idPriority);

            result.Should().BeTrue();

        }

        [Theory, AutoNSubstituteData]
        public void NotAllowedPriorityValue_Should_Return_True_When_IdPriority_Medium(TaskProjectValidatorService sut)
        {

            var idPriority = (int)EnumPriorityTask.Medium;

            var result = sut.NotAllowedPriorityValue(idPriority);

            result.Should().BeTrue();

        }

        [Theory, AutoNSubstituteData]
        public void NotAllowedPriorityValue_Should_Return_True_When_IdPriority_High(TaskProjectValidatorService sut)
        {

            var idPriority = (int)EnumPriorityTask.High;

            var result = sut.NotAllowedPriorityValue(idPriority);

            result.Should().BeTrue();

        }

        [Theory, AutoNSubstituteData]
        public void NotAllowedPriorityValue_Should_Return_False(TaskProjectValidatorService sut)
        {

            var idPriority = 4;

            var result = sut.NotAllowedPriorityValue(idPriority);

            result.Should().BeFalse();

        }
    }
}
