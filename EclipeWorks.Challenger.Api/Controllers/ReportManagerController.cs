using AutoMapper;
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
    [Route("eclipseworks/api/reportmanager")]
    public class ReportManagerController : Controller
    {


        private readonly IMapper _mapper;
        public IReportManagerService _reportManagerService;
        private readonly IValidator<FilterReportManagerModelRequest> _validator;

        public ReportManagerController(IMapper mapper, IReportManagerService reportManagerService,
                                         IValidator<FilterReportManagerModelRequest> validator)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _reportManagerService = reportManagerService ?? throw new ArgumentNullException(nameof(reportManagerService));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] FilterReportManagerModelRequest filterReportManagerModelRequest)
        {
            var validationResult = await _validator.ValidateAsync(filterReportManagerModelRequest);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }


            var reports = await _reportManagerService.GetAllDeliveries(filterReportManagerModelRequest.idProject == 0 ? null: filterReportManagerModelRequest.idProject,
                                                filterReportManagerModelRequest.Status == 0 ? null : filterReportManagerModelRequest.Status,
                                                filterReportManagerModelRequest.idOwner == 0 ? null : filterReportManagerModelRequest.idOwner);

            if (reports.Any())
            {
                var reportsMap = _mapper.Map<IEnumerable<ReportManager>, IEnumerable<ReportManagerModel>>(reports);

                return Ok(reportsMap);

            }

            return NotFound();

        }
    }
}
