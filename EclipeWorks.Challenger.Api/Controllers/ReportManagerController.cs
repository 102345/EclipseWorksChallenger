using AutoMapper;
using EclipseWorks.Challenger.Application.Contracts;
using EclipseWorks.Challenger.Application.Services.Interfaces;
using EclipseWorks.Challenger.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EclipeWorks.Challenger.Api.Controllers
{
    [ApiController]
    [Route("eclipseworks/api/reportmanager")]
    public class ReportManagerController : Controller
    {


        private readonly IMapper _mapper;
        public IReportManagerService _reportManagerService;

        public ReportManagerController(IMapper mapper, IReportManagerService reportManagerService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _reportManagerService = reportManagerService ?? throw new ArgumentNullException(nameof(reportManagerService));
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int idProject, int Status, int idOwner)
        {
            //var validationResult = await _validatorFilter.ValidateAsync(filterCustomerSupplierRequest);

            //if (!validationResult.IsValid)
            //{
            //    return BadRequest(validationResult.Errors);
            //}


            var reports = await _reportManagerService.GetAllDeliveries(idProject == 0 ? null: idProject, Status == 0 ? null : Status, idOwner == 0 ? null : idOwner);

            if (reports.Any())
            {
                var reportsMap = _mapper.Map<IEnumerable<ReportManager>, IEnumerable<ReportManagerModel>>(reports);

                return Ok(reportsMap);

            }

            return NotFound();

        }
    }
}
