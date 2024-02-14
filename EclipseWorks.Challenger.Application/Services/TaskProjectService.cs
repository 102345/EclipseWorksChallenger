using EclipseWorks.Challenger.Application.Services.Interfaces;
using EclipseWorks.Challenger.Domain.Entities;
using EclipseWorks.Challenger.InfraStructure.Interfaces;
using EclipseWorks.Challenger.InfraStructure.UnitOfWork;

namespace EclipseWorks.Challenger.Application.Services
{
    public class TaskProjectService : ITaskProjectService
    {

        public IUnitOfWork _unitOfWork { get; }
        public TaskProjectService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

        }
        public async Task CreateTaskAsync(TaskProject taskProject)
        {

            var idTask = await _unitOfWork.TaskProjects.Add(taskProject);

            var historyTask = new HistoryTaskProject()
            {
                IdTask = idTask,
                Status = taskProject.Status,
                DescriptionTask = taskProject.Description,
                IdOwner = taskProject.IdOwner,
                IdProject = taskProject.IdProject,
                CreatedAt = DateTime.Now
            };

            await _unitOfWork.HistoryTaskProjects.Add(historyTask);

            _unitOfWork.Commit();

        }

        public async Task DeleteTaskAsync(int id)
        {

            var taskProject = await _unitOfWork.TaskProjects.GetById(id);

            var historyTask = new HistoryTaskProject()
            {
                IdTask = id,
                Status = taskProject.Status,
                DescriptionTask = taskProject.Description,
                IdOwner = taskProject.IdOwner,
                IdProject = taskProject.IdProject,
                DeletedAt = DateTime.Now
            };

            await _unitOfWork.HistoryTaskProjects.Add(historyTask);

            await _unitOfWork.Comments.DeletePerTask(id);

            await _unitOfWork.TaskProjects.Delete(id);

            _unitOfWork.Commit();

        }

        public async Task<IEnumerable<TaskProject>> GetAllTasks(int idProject)
        {

            return await _unitOfWork.TaskProjects.GetByProject(idProject);

        }

        public async Task UpdateTaskAsync(TaskProject taskProject)
        {

            await _unitOfWork.TaskProjects.Update(taskProject);

            var historyTask = new HistoryTaskProject()
            {
                IdTask = taskProject.IdTask,
                Status = taskProject.Status,
                DescriptionTask = taskProject.Description,
                IdOwner = taskProject.IdOwner,
                IdProject = taskProject.IdProject,
                UpdatedAt = DateTime.Now
            };

            await _unitOfWork.HistoryTaskProjects.Add(historyTask);

            _unitOfWork.Commit();

        }

        public async Task<TaskProject> GetById(int idTask)
        {

            return await _unitOfWork.TaskProjects.GetById(idTask);

        }
    }
}
