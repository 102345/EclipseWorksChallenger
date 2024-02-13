using EclipseWorks.Challenger.Application.Services.Interfaces;
using EclipseWorks.Challenger.Domain.Entities;
using EclipseWorks.Challenger.InfraStructure.Interfaces;
using EclipseWorks.Challenger.InfraStructure.UnitOfWork;

namespace EclipseWorks.Challenger.Application.Services
{
    public class CommentService : ICommentService
    {
        public IReaderStringConnectionDb _readerStringConnectionDb { get; }
        public CommentService(IReaderStringConnectionDb readerStringConnectionDb)
        {
            _readerStringConnectionDb = readerStringConnectionDb ?? throw new ArgumentNullException(nameof(readerStringConnectionDb));
        }
        public async Task CreateCommentAsync(Comment comment)
        {
            using (var unitOfWork = new UnitOfWork(_readerStringConnectionDb.GetStringConnectionName()))
            {
                var idComment = await unitOfWork.Comments.Add(comment);

                var taskProject = await unitOfWork.TaskProjects.GetById(comment.IdTask);

                var historyTask = new HistoryTaskProject()
                {   
                    IdTask = comment.IdTask,
                    IdComment = idComment,
                    Status = taskProject.Status,
                    DescriptionComment = comment.Description,
                    DescriptionTask = taskProject.Description,
                    IdOwner = taskProject.IdOwner,
                    IdProject = taskProject.IdProject,
                    CreatedAt = DateTime.Now
                };

                await unitOfWork.HistoryTaskProjects.Add(historyTask);

                unitOfWork.Commit();
            }
        }
    }
}
