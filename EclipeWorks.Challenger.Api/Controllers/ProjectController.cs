using AutoMapper;
using EclipseWorks.Challenger.Application.Contracts;
using EclipseWorks.Challenger.Application.Services.Interfaces;
using EclipseWorks.Challenger.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EclipeWorks.Challenger.Api.Controllers
{

    [ApiController]
    [Route("eclipseworks/api/project")]
    public class ProjectController : Controller
    {
        private readonly IMapper _mapper;
        public IProjectService _projectService;
        public ITaskProjectValidatorService _taskProjectValidatorService;

        public ProjectController(IMapper mapper, IProjectService projectService, ITaskProjectValidatorService taskProjectValidatorService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _projectService = projectService ?? throw new ArgumentNullException(nameof(projectService));
            _taskProjectValidatorService = taskProjectValidatorService ?? throw new ArgumentNullException(nameof(taskProjectValidatorService));
        }



        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int idOwner)
        {
            //var validationResult = await _validatorFilter.ValidateAsync(filterCustomerSupplierRequest);

            //if (!validationResult.IsValid)
            //{
            //    return BadRequest(validationResult.Errors);
            //}


            var projects= await _projectService.GetAllAsync(idOwner);

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


        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int idProject)
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
