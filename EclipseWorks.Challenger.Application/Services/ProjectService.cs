using EclipseWorks.Challenger.Application.Services.Interfaces;
using EclipseWorks.Challenger.Domain.Entities;
using EclipseWorks.Challenger.InfraStructure.Interfaces;
using EclipseWorks.Challenger.InfraStructure.UnitOfWork;

namespace EclipseWorks.Challenger.Application.Services
{
    public class ProjectService : IProjectService
    {
        public IReaderStringConnectionDb _readerStringConnectionDb { get; }
        public ProjectService(IReaderStringConnectionDb readerStringConnectionDb)
        {
            _readerStringConnectionDb = readerStringConnectionDb ?? throw new ArgumentNullException(nameof(readerStringConnectionDb));
        }
        public async Task CreateAsync(Project project)
        {
            using (var unitOfWork = new UnitOfWork(_readerStringConnectionDb.GetStringConnectionName()))
            {
                await unitOfWork.Projects.Add(project);

                unitOfWork.Commit();
            }
        }

        public async Task<IEnumerable<Project>> GetAllAsync(int idOwner)
        {
            using (var unitOfWork = new UnitOfWork(_readerStringConnectionDb.GetStringConnectionName()))
            {
                return await unitOfWork.Projects.GetAllAsync(idOwner);

            }
        }

        public async  Task DeleteAsync(int idProject)
        {
            using (var unitOfWork = new UnitOfWork(_readerStringConnectionDb.GetStringConnectionName()))
            {

                var project = await unitOfWork.Projects.GetById(idProject);

                var historyProject = new HistoryTaskProject()
                {
                    IdOwner = project.IdOwner,
                    IdProject = project.IdProject,
                    DeletedAt = DateTime.Now
                };

                await unitOfWork.HistoryTaskProjects.Add(historyProject);

                var taskProjects = await unitOfWork.TaskProjects.GetByProject(idProject);

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

                    await unitOfWork.HistoryTaskProjects.Add(historyTask);

                    var comments = await unitOfWork.Comments.GetCommentsByTask(taskProject.IdTask);

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

                        await unitOfWork.HistoryTaskProjects.Add(historyComment);
                    }

                    await unitOfWork.Comments.DeletePerTask(taskProject.IdTask);

                    await unitOfWork.TaskProjects.Delete(taskProject.IdTask);

                }

                await unitOfWork.Projects.Delete(idProject);

                unitOfWork.Commit();
            }
        }

        public async  Task<Project> GetById(int id)
        {
            using (var unitOfWork = new UnitOfWork(_readerStringConnectionDb.GetStringConnectionName()))
            {
                return await unitOfWork.Projects.GetById(id);

            }
        }
    }
}
