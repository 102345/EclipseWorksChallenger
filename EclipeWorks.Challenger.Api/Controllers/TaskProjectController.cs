﻿using AutoMapper;
using EclipseWorks.Challenger.Application.Contracts;
using EclipseWorks.Challenger.Application.Services.Interfaces;
using EclipseWorks.Challenger.Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace EclipeWorks.Challenger.Api.Controllers
{
    [ExcludeFromCodeCoverage]
    [ApiController]
    [Route("eclipseworks/api/project/task")]
    public class TaskProjectController : Controller
    {
        private readonly IMapper _mapper;
        public ITaskProjectService _taskProjectService;
        private readonly IValidator<TaskProjectModel> _validator;

        public TaskProjectController(IMapper mapper, ITaskProjectService taskProjectService, IValidator<TaskProjectModel> validator)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _taskProjectService = taskProjectService ?? throw new ArgumentNullException(nameof(taskProjectService));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }



        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int idProject)
        {

            var taskProjects = await _taskProjectService.GetAllTasks(idProject);

            if(taskProjects != null)
            {
                var taskProjectsModel = _mapper.Map<IEnumerable<TaskProject>, IEnumerable<TaskProjectModel>>(taskProjects);

                return Ok(taskProjectsModel);

            }


            return NoContent();

        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TaskProjectModel taskprojectModel)
        {

            var validationResult = await _validator.ValidateAsync(taskprojectModel);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }



            var taskProject = _mapper.Map<TaskProjectModel, TaskProject>(taskprojectModel);

            await _taskProjectService.CreateTaskAsync(taskProject);

            return NoContent();

        }



        [HttpPut]
        public async Task<IActionResult> Put([FromBody] TaskProjectUpdateModel taskprojectUpdateModel)
        {

            //var validationResult = await _validator.ValidateAsync(customerSupplierRequest);

            //if (!validationResult.IsValid)
            //{
            //    return BadRequest(validationResult.Errors);
            //}

            var taskProject = _mapper.Map<TaskProjectUpdateModel, TaskProject>(taskprojectUpdateModel);

            await _taskProjectService.UpdateTaskAsync(taskProject);

            return NoContent();

        }


        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int idTask)
        {

            var taskProjet = await _taskProjectService.GetById(idTask);

            if (taskProjet == null)
            {
                return BadRequest(new { Message = "There is no Task with this IdTask" });
            }

            await _taskProjectService.DeleteTaskAsync(idTask);

            return NoContent();

        }
    }
}
