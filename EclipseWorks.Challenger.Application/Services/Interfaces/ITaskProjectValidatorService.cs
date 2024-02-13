namespace EclipseWorks.Challenger.Application.Services.Interfaces
{
    public interface ITaskProjectValidatorService
    {
        bool NotAllowedPriorityValue(int idPriority);
        Task<bool> HasTaskPending(int idProject);
        Task<bool> ExceedMaximumTask(int idProject,int numberMaxTask);
    }
}
