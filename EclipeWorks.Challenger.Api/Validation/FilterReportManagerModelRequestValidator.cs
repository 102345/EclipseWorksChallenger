using EclipseWorks.Challenger.Application.Contracts;
using EclipseWorks.Challenger.Application.Services.Interfaces;
using FluentValidation;

namespace EclipeWorks.Challenger.Api.Validation
{
    public class FilterReportManagerModelRequestValidator : AbstractValidator<FilterReportManagerModelRequest>
    {
        public IReportManagerValidatorService _reportManagerValidatorService;
        public FilterReportManagerModelRequestValidator(IReportManagerValidatorService reportManagerValidatorService)
        {
            _reportManagerValidatorService = reportManagerValidatorService ?? throw new ArgumentNullException(nameof(reportManagerValidatorService));

            RuleFor(filterReportManagerModelRequestValidator => filterReportManagerModelRequestValidator.idOwnerAuthorized)
              .NotNull()
              .WithMessage("PageIndex cannot be null")
              .NotEmpty()
              .WithMessage("PageIndex cannot be empty");
              

            When(filterReportManagerModelRequestValidator => filterReportManagerModelRequestValidator.idOwnerAuthorized != null, () =>
            {
               
                RuleFor(filterReportManagerModelRequestValidator => filterReportManagerModelRequestValidator)
                    .Must(filterReportManagerModelRequestValidator =>
                     ValidateOwnerRole(filterReportManagerModelRequestValidator.idOwnerAuthorized))
                    .WithMessage("The owner must have the role of manager");
            });
        }

        public bool ValidateOwnerRole(int idOwnerAuthorized)
        {   
            if (idOwnerAuthorized <= 0)
            {
                return false;
            }

            return _reportManagerValidatorService.IsManager(idOwnerAuthorized).Result;

        }
    }
}
