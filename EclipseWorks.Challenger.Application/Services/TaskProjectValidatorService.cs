using EclipseWorks.Challenger.Application.Services.Interfaces;
using EclipseWorks.Challenger.Domain.Enums;
using EclipseWorks.Challenger.InfraStructure.Interfaces;

namespace EclipseWorks.Challenger.Application.Services
{
    public class TaskProjectValidatorService : ITaskProjectValidatorService
    {
        public IUnitOfWork _unitOfWork { get; }
        public TaskProjectValidatorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

        }
        public async Task<bool> HasTaskPending(int idProject)
        {
            _unitOfWork.BeginTransaction();

            var tasks = await _unitOfWork.TaskProjects.GetByProject(idProject);

            var task = tasks.FirstOrDefault(f => f.Status == (int)EnumStatusTask.Pendency);

            _unitOfWork.Commit();

            return task != null ? true : false;

        }

        public async Task<bool> ExceedMaximumTask(int idProject, int numberMaxTask)
        {   
            _unitOfWork.BeginTransaction();

            var tasks = await _unitOfWork.TaskProjects.GetByProject(idProject);

            _unitOfWork.Commit();

            var taskCount = tasks.Count();

            return taskCount > numberMaxTask ? true : false;

        }

        public bool NotAllowedPriorityValue(int idPriority)
        {
            return idPriority == (int)EnumPriorityTask.Low || idPriority == (int)EnumPriorityTask.Medium
                                        || idPriority == (int)EnumPriorityTask.High;
        }
    }
}
