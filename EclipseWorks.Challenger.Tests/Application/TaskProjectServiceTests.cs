using AutoFixture.Idioms;
using EclipseWorks.Challenger.Application.Services;
using EclipseWorks.Challenger.Domain.Entities;
using EclipseWorks.Challenger.Tests.AutoFixture;
using FluentAssertions;
using NSubstitute;

namespace EclipseWorks.Challenger.Tests.Application
{
    public class TaskProjectServiceTests
    {
        [Theory, AutoNSubstituteData]
        public void Sut_Should_GuardItsClause(GuardClauseAssertion assertion)
        {
            assertion.Verify(typeof(TaskProjectService).GetConstructors());
        }

        [Theory, AutoNSubstituteData]
        public async Task CreateTaskAsync_Should_Return_Success(TaskProjectService sut, TaskProject taskProject,
           HistoryTaskProject historyTask)
        {

            int idComment = 1;

            sut._unitOfWork.BeginTransaction();

            sut._unitOfWork.TaskProjects.Add(taskProject);

            sut._unitOfWork.HistoryTaskProjects.Add(historyTask);

            sut._unitOfWork.Commit();

            var result = await sut.CreateTaskAsync(taskProject);

            result.Should().BeTrue();

        }

        [Theory, AutoNSubstituteData]
        public async Task UpdateTaskAsync_Should_Return_Success(TaskProjectService sut, TaskProject taskProject,
           HistoryTaskProject historyTask)
        {

            sut._unitOfWork.BeginTransaction();

            sut._unitOfWork.TaskProjects.Update(taskProject);

            sut._unitOfWork.HistoryTaskProjects.Add(historyTask);

            sut._unitOfWork.Commit();

            var result = await sut.UpdateTaskAsync(taskProject);

            result.Should().BeTrue();

        }

        [Theory, AutoNSubstituteData]
        public async Task DeleteTaskAsync_Should_Return_Success(TaskProjectService sut, TaskProject taskProject,
          HistoryTaskProject historyTask)
        {
            int idTask = 1;

            sut._unitOfWork.BeginTransaction();

            sut._unitOfWork.TaskProjects.GetById(idTask).Returns(taskProject);

            sut._unitOfWork.HistoryTaskProjects.Add(historyTask);

            sut._unitOfWork.Comments.DeletePerTask(idTask);

            sut._unitOfWork.TaskProjects.Delete(idTask);

            sut._unitOfWork.Commit();

            var result = await sut.DeleteTaskAsync(idTask);

            result.Should().BeTrue();

        }

        [Theory, AutoNSubstituteData]
        public async Task GetById(TaskProjectService sut, TaskProject taskProject)
        {
            var idTask = 1;

            sut._unitOfWork.BeginTransaction();

            sut._unitOfWork.TaskProjects.GetById(idTask).Returns(taskProject);

            sut._unitOfWork.Commit();

            var result = await sut.GetById(idTask);

            result.Equals(taskProject);

        }



        [Theory, AutoNSubstituteData]
        public async Task GetAllTasks(TaskProjectService sut, List<TaskProject> taskProjects)
        {
            var idProject = 1;

            sut._unitOfWork.BeginTransaction();

            sut._unitOfWork.TaskProjects.GetByProject(idProject).Returns(taskProjects);

            sut._unitOfWork.Commit();

            var result = await sut.GetAllTasks(idProject);

            result.Equals(taskProjects);

        }
    }
}
