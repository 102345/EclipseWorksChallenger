using EclipseWorks.Challenger.Application.Services.Interfaces;
using EclipseWorks.Challenger.Domain.Entities;
using EclipseWorks.Challenger.InfraStructure.Interfaces;

namespace EclipseWorks.Challenger.Application.Services
{
    public class TaskProjectService : ITaskProjectService
    {

        public IUnitOfWork _unitOfWork { get; }
        public TaskProjectService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

        }
        public async Task<bool> CreateTaskAsync(TaskProject taskProject)
        {
            try
            {
                _unitOfWork.BeginTransaction();

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

                return true;

            }
            catch(Exception ex)
            {
                string message = ex.Message;    
                _unitOfWork?.Rollback();
                return false;
            }

           

        }

        public async Task<bool> DeleteTaskAsync(int id)
        {
            try
            {   
                _unitOfWork.BeginTransaction();

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

                return true;

            }
            catch(Exception ex)
            {
                string msg = ex.Message;
                _unitOfWork?.Rollback();
                return false;
            }
          

        }

        public async Task<IEnumerable<TaskProject>> GetAllTasks(int idProject)
        {
            _unitOfWork.BeginTransaction();

            var taskProject = await _unitOfWork.TaskProjects.GetByProject(idProject);

            _unitOfWork.Commit();

            return taskProject;

        }

        public async Task<bool> UpdateTaskAsync(TaskProject taskProject)
        {

            try
            {
                _unitOfWork.BeginTransaction();

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

                return true;
            }
            catch (Exception)
            {
                _unitOfWork?.Rollback();
                return false;
            }

        }

        public async Task<TaskProject> GetById(int idTask)
        {
            _unitOfWork.BeginTransaction();

            var taskProject = await _unitOfWork.TaskProjects.GetById(idTask);

            _unitOfWork.Commit();

            return taskProject;

        }
    }
}
