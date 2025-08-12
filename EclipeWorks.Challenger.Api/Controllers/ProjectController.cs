using AutoMapper;
using EclipseWorks.Challenger.Application.Contracts;
using EclipseWorks.Challenger.Application.Services.Interfaces;
using EclipseWorks.Challenger.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace EclipeWorks.Challenger.Api.Controllers
{
    [ExcludeFromCodeCoverage]
    [ApiController]
    [Route("eclipseworks/api/project")]
    public class ProjectController : Controller
    {
        private readonly IMapper _mapper;
        public IProjectService _projectService;
        public IOwnerService _ownerService;
        public ITaskProjectValidatorService _taskProjectValidatorService;

        public ProjectController(IMapper mapper, IProjectService projectService, ITaskProjectValidatorService taskProjectValidatorService, 
                                    IOwnerService ownerService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _projectService = projectService ?? throw new ArgumentNullException(nameof(projectService));
            _taskProjectValidatorService = taskProjectValidatorService ?? throw new ArgumentNullException(nameof(taskProjectValidatorService));
            _ownerService = ownerService ?? throw new ArgumentNullException(nameof(ownerService));
        }


        [HttpGet("idOwner/{idOwner}")]
        public async Task<IActionResult> Get(int idOwner)
        {
            var owner = await _ownerService.GetById(idOwner);

            if (owner == null)
            {
                return BadRequest(new { Message = "There is no Owner with this idOwner" });
            }


            var projects = await _projectService.GetAllAsync(idOwner);

            if (projects != null)
            {
                var projectsMap = _mapper.Map<IEnumerable<Project>, IEnumerable<ProjectModelResponse>>(projects);

                return Ok(projectsMap);

            }

            return NotFound();

        }



        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProjectModel projectModel)
        {

            //var validationResult = await _validator.ValidateAsync(customerSupplierRequest);

            //if (!validationResult.IsValid)
            //{
            //    return BadRequest(validationResult.Errors);
            //}



             var project = _mapper.Map<ProjectModel, Project>(projectModel);

             await _projectService.CreateAsync(project);

            return NoContent();

        }


        [HttpDelete("{idProject}")]
        public async Task<IActionResult> Delete(int idProject)
        {

            var project = await _projectService.GetById(idProject);

            if (project == null)
            {
                return BadRequest(new { Message = "There is no Project with this idProject" });
            }

            var retValidator = await _taskProjectValidatorService.HasTaskPending(idProject);

            if(retValidator == true)
            {
                return BadRequest(new { Message = "Please complete pending tasks or remove tasks first" });
            }

            await _projectService.DeleteAsync(idProject);

            return NoContent();

        }
    }
}
