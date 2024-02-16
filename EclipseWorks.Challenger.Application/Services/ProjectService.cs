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
            try
            {
                _unitOfWork.BeginTransaction();

                await _unitOfWork.Projects.Add(project);

                _unitOfWork.Commit();
            }
            catch (Exception ex) 
            { 
                string msg = ex.Message;
                _unitOfWork.Rollback();
            }

        }

        public async Task<IEnumerable<Project>> GetAllAsync(int idOwner)
        {
            _unitOfWork.BeginTransaction();

            var projects =  await _unitOfWork.Projects.GetAllAsync(idOwner);

            _unitOfWork.Commit();

            return projects;

        }

        public async Task DeleteAsync(int idProject)
        {

            try
            {
                _unitOfWork.BeginTransaction();

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
            catch(Exception ex)
            {

                _unitOfWork.Rollback();
            }

            

        }

        public async Task<Project> GetById(int id)
        {
            _unitOfWork.BeginTransaction();

            var project = await _unitOfWork.Projects.GetById(id);

            _unitOfWork.Commit();

            return project;
        }
    }
}
