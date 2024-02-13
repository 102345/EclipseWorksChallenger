using EclipseWorks.Challenger.Application.Services.Interfaces;
using EclipseWorks.Challenger.Domain.Entities;
using EclipseWorks.Challenger.InfraStructure.Interfaces;
using EclipseWorks.Challenger.InfraStructure.UnitOfWork;

namespace EclipseWorks.Challenger.Application.Services
{
    public class TaskProjectService : ITaskProjectService
    {

        public IReaderStringConnectionDb _readerStringConnectionDb { get; }
        public TaskProjectService(IReaderStringConnectionDb readerStringConnectionDb)
        {
            _readerStringConnectionDb = readerStringConnectionDb ?? throw new ArgumentNullException(nameof(readerStringConnectionDb));
        }
        public async Task CreateTaskAsync(TaskProject taskProject)
        {
            using (var unitOfWork = new UnitOfWork(_readerStringConnectionDb.GetStringConnectionName()))
            {
                var idTask = await unitOfWork.TaskProjects.Add(taskProject);

                var historyTask = new HistoryTaskProject()
                                    {   
                                        IdTask = idTask,
                                        Status = taskProject.Status,
                                        DescriptionTask = taskProject.Description,
                                        IdOwner = taskProject.IdOwner,
                                        IdProject = taskProject.IdProject,
                                        CreatedAt = DateTime.Now
                                    };

                await unitOfWork.HistoryTaskProjects.Add(historyTask);

                unitOfWork.Commit();
            }
        }

        public async Task DeleteTaskAsync(int id)
        {
            using (var unitOfWork = new UnitOfWork(_readerStringConnectionDb.GetStringConnectionName()))
            {
                var taskProject = await unitOfWork.TaskProjects.GetById(id);

                var historyTask = new HistoryTaskProject()
                {
                    IdTask = id,
                    Status = taskProject.Status,
                    DescriptionTask = taskProject.Description,
                    IdOwner = taskProject.IdOwner,
                    IdProject = taskProject.IdProject,
                    DeletedAt = DateTime.Now
                };

                await unitOfWork.HistoryTaskProjects.Add(historyTask);

                await unitOfWork.Comments.DeletePerTask(id);

                await unitOfWork.TaskProjects.Delete(id);

                unitOfWork.Commit();
            }
        }

        public async Task<IEnumerable<TaskProject>> GetAllTasks(int idProject)
        {
            using (var unitOfWork = new UnitOfWork(_readerStringConnectionDb.GetStringConnectionName()))
            {
                return await unitOfWork.TaskProjects.GetByProject(idProject);
            }
        }

        public async Task UpdateTaskAsync(TaskProject taskProject)
        {
            using (var unitOfWork = new UnitOfWork(_readerStringConnectionDb.GetStringConnectionName()))
            {
                await unitOfWork.TaskProjects.Update(taskProject);

                var historyTask = new HistoryTaskProject()
                {
                    IdTask = taskProject.IdTask,
                    Status = taskProject.Status,
                    DescriptionTask = taskProject.Description,
                    IdOwner = taskProject.IdOwner,
                    IdProject = taskProject.IdProject,
                    UpdatedAt = DateTime.Now
                };

                await unitOfWork.HistoryTaskProjects.Add(historyTask);

                unitOfWork.Commit();
            }
        }
    }
}
