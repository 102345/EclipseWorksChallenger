using AutoFixture.Idioms;
using EclipseWorks.Challenger.Application.Services;
using EclipseWorks.Challenger.Domain.Entities;
using EclipseWorks.Challenger.Tests.AutoFixture;
using FluentAssertions;
using NSubstitute;

namespace EclipseWorks.Challenger.Tests.Application
{
    public class ProjectServiceTests
    {
        [Theory, AutoNSubstituteData]
        public void Sut_Should_GuardItsClause(GuardClauseAssertion assertion)
        {
            assertion.Verify(typeof(ProjectService).GetConstructors());
        }

        [Theory, AutoNSubstituteData]
        public async Task CreateAsync_Should_Return_Success(ProjectService sut, Project project)
        {   
            sut._unitOfWork.BeginTransaction();
 
            sut._unitOfWork.Projects.Add(project);

            sut._unitOfWork.Commit();

            var result = await sut.CreateAsync(project);

            result.Should().BeTrue();

        }

        [Theory, AutoNSubstituteData]
        public async Task GetAllAsync_Should_Return_Projects(ProjectService sut, List<Project> projects)
        {
            int idOwner = 1;

            sut._unitOfWork.BeginTransaction();

            sut._unitOfWork.Projects.GetAllAsync(idOwner).Returns(projects);

            sut._unitOfWork.Commit();

            var result = await sut.GetAllAsync(idOwner);

            result.Equals(projects);
        }


        [Theory, AutoNSubstituteData]
        public async Task GetById_Should_Return_Project(ProjectService sut, Project project)
        {
            int idProject = 1;

            sut._unitOfWork.BeginTransaction();

            sut._unitOfWork.Projects.GetById(idProject).Returns(project);

            sut._unitOfWork.Commit();

            var result = await sut.GetById(idProject);

            result.Equals(project);
        }

        [Theory, AutoNSubstituteData]
        public async Task DeleteAsync_Should_Return_Success(ProjectService sut, Project project, HistoryTaskProject historyProject,
            HistoryTaskProject historyTask, HistoryTaskProject historyComment, List<TaskProject> taskProjects , List<Comment> comments)
        {   
            int idProject = 1;
            int idTask = 1;

            sut._unitOfWork.BeginTransaction();

            sut._unitOfWork.Projects.GetById(idProject).Returns(project);

            sut._unitOfWork.HistoryTaskProjects.Add(historyProject);

            sut._unitOfWork.Projects.Add(project);

            sut._unitOfWork.TaskProjects.GetByProject(idProject).Returns(taskProjects);

            sut._unitOfWork.HistoryTaskProjects.Add(historyTask);

            sut._unitOfWork.Comments.GetCommentsByTask(idTask).Returns(comments);

            sut._unitOfWork.HistoryTaskProjects.Add(historyComment);

            sut._unitOfWork.Comments.DeletePerTask(idTask);

            sut._unitOfWork.TaskProjects.Delete(idTask);

            sut._unitOfWork.Projects.Delete(idProject);

            sut._unitOfWork.Commit();

            var result = await sut.DeleteAsync(idProject);

            result.Should().BeTrue();

        }
    }
}
