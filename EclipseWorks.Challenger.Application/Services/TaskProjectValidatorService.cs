using EclipseWorks.Challenger.Application.Services.Interfaces;
using EclipseWorks.Challenger.Domain.Enums;
using EclipseWorks.Challenger.InfraStructure.Interfaces;
using EclipseWorks.Challenger.InfraStructure.UnitOfWork;

namespace EclipseWorks.Challenger.Application.Services
{
    public class TaskProjectValidatorService : ITaskProjectValidatorService
    {
        public IReaderStringConnectionDb _readerStringConnectionDb { get; }
        public TaskProjectValidatorService(IReaderStringConnectionDb readerStringConnectionDb)
        {
            _readerStringConnectionDb = readerStringConnectionDb ?? throw new ArgumentNullException(nameof(readerStringConnectionDb));
        }
        public async Task<bool> HasTaskPending(int idProject)
        {

            using (var unitOfWork = new UnitOfWork(_readerStringConnectionDb.GetStringConnectionName()))
            {
                var tasks = await unitOfWork.TaskProjects.GetByProject(idProject);

                var task = tasks.FirstOrDefault(f => f.Status == (int)EnumStatusTask.Pendency);

                return task != null ? true : false;
            }
        }

        public async Task<bool> ExceedMaximumTask(int idProject, int numberMaxTask)
        {
            using (var unitOfWork = new UnitOfWork(_readerStringConnectionDb.GetStringConnectionName()))
            {
                var tasks = await unitOfWork.TaskProjects.GetByProject(idProject);

                var taskCount = tasks.Count();

                return taskCount > numberMaxTask ? true : false;
            }
        }

        public bool NotAllowedPriorityValue(int idPriority)
        {
            return idPriority == (int)EnumPriorityTask.Low || idPriority == (int)EnumPriorityTask.Medium 
                                        || idPriority == (int)EnumPriorityTask.High;
        }
    }
}
