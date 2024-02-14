using EclipseWorks.Challenger.Application.Services.Interfaces;
using EclipseWorks.Challenger.Domain.Entities;
using EclipseWorks.Challenger.InfraStructure.Interfaces;

namespace EclipseWorks.Challenger.Application.Services
{
    public class ProjectService : IProjectService
    {
        public IUnitOfWork _unitOfWork { get; }
        public ProjectService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

        }
        public async Task CreateAsync(Project project)
        {

            await _unitOfWork.Projects.Add(project);

            _unitOfWork.Commit();

        }

        public async Task<IEnumerable<Project>> GetAllAsync(int idOwner)
        {

            return await _unitOfWork.Projects.GetAllAsync(idOwner);

        }

        public async Task DeleteAsync(int idProject)
        {


            var project = await _unitOfWork.Projects.GetById(idProject);

            var historyProject = new HistoryTaskProject()
            {
                IdOwner = project.IdOwner,
                IdProject = project.IdProject,
                DeletedAt = DateTime.Now
            };

            await _unitOfWork.HistoryTaskProjects.Add(historyProject);

            var taskProjects = await _unitOfWork.TaskProjects.GetByProject(idProject);

            foreach (var taskProject in taskProjects)
            {
                var historyTask = new HistoryTaskProject()
                {
                    IdTask = taskProject.IdTask,
                    Status = taskProject.Status,
                    DescriptionTask = taskProject.Description,
                    IdOwner = taskProject.IdOwner,
                    IdProject = taskProject.IdProject,
                    DeletedAt = DateTime.Now
                };

                await _unitOfWork.HistoryTaskProjects.Add(historyTask);

                var comments = await _unitOfWork.Comments.GetCommentsByTask(taskProject.IdTask);

                foreach (var comment in comments)
                {
                    var historyComment = new HistoryTaskProject()
                    {
                        IdTask = taskProject.IdTask,
                        IdComment = comment.IdComment,
                        Status = taskProject.Status,
                        DescriptionTask = taskProject.Description,
                        DescriptionComment = comment.Description,
                        IdOwner = taskProject.IdOwner,
                        IdProject = taskProject.IdProject,
                        DeletedAt = DateTime.Now
                    };

                    await _unitOfWork.HistoryTaskProjects.Add(historyComment);
                }

                await _unitOfWork.Comments.DeletePerTask(taskProject.IdTask);

                await _unitOfWork.TaskProjects.Delete(taskProject.IdTask);

            }

            await _unitOfWork.Projects.Delete(idProject);

            _unitOfWork.Commit();

        }

        public async Task<Project> GetById(int id)
        {
            return await _unitOfWork.Projects.GetById(id);
        }
    }
}
