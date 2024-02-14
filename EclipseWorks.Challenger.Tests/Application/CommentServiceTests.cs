using AutoFixture.Idioms;
using EclipseWorks.Challenger.Application.Services;
using EclipseWorks.Challenger.Domain.Entities;
using EclipseWorks.Challenger.Tests.AutoFixture;
using FluentAssertions;
using NSubstitute;

namespace EclipseWorks.Challenger.Tests.Application
{
    public  class CommentServiceTests
    {
        [Theory, AutoNSubstituteData]
        public void Sut_Should_GuardItsClause(GuardClauseAssertion assertion)
        {
            assertion.Verify(typeof(CommentService).GetConstructors());
        }


        [Theory, AutoNSubstituteData]
        public async Task CreateCommentAsync_Should_Return_Success(CommentService sut, Comment comment, TaskProject taskProject, 
            HistoryTaskProject historyTask)
        {

            int idComment = 1;

            sut._unitOfWork.Comments.Add(comment).Returns(idComment);

            sut._unitOfWork.TaskProjects.GetById(comment.IdTask).Returns(taskProject);

            historyTask.IdComment = idComment;

            sut._unitOfWork.HistoryTaskProjects.Add(historyTask);

            sut._unitOfWork.Commit();

            var result = await sut.CreateCommentAsync(comment);

            result.Should().BeTrue();

        }
    }
}
