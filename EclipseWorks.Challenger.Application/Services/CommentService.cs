using EclipseWorks.Challenger.Application.Services.Interfaces;
using EclipseWorks.Challenger.Domain.Entities;
using EclipseWorks.Challenger.InfraStructure.Interfaces;

namespace EclipseWorks.Challenger.Application.Services
{
    public class CommentService : ICommentService
    {
        public IUnitOfWork _unitOfWork { get; }
        public CommentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

        }
        public async Task CreateCommentAsync(Comment comment)
        {

            var idComment = await _unitOfWork.Comments.Add(comment);

            var taskProject = await _unitOfWork.TaskProjects.GetById(comment.IdTask);

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

            await _unitOfWork.HistoryTaskProjects.Add(historyTask);

            _unitOfWork.Commit();

        }
    }
}
