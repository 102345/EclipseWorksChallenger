using EclipseWorks.Challenger.Application.Contracts;
using EclipseWorks.Challenger.Application.Services.Interfaces;
using FluentValidation;

namespace EclipeWorks.Challenger.Api.Validation
{
    public class TaskProjectValidator : AbstractValidator<TaskProjectModel>
    {
        public ITaskProjectValidatorService _taskProjectValidatorService;

        private readonly IConfiguration _configuration;
        public TaskProjectValidator(ITaskProjectValidatorService taskProjectValidatorService,IConfiguration configuration)
        {
            _taskProjectValidatorService = taskProjectValidatorService ?? throw new ArgumentNullException(nameof(taskProjectValidatorService));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

            int numberMaxTasks = _configuration.GetValue<int>("ParametersRulesBussiness:NumberMaxTasks");

            RuleFor(taskProjectValidator => taskProjectValidator.IdPriority)
            .NotNull()
            .WithMessage("IdPriority cannot be null")
            .NotEmpty()
            .WithMessage("IdPriority cannot be empty");

            When(taskProjectValidator => taskProjectValidator.IdPriority != null, () =>
            {

                RuleFor(taskProjectValidator => taskProjectValidator)
                    .Must(taskProjectValidator =>
                     _taskProjectValidatorService.NotAllowedPriorityValue(taskProjectValidator.IdPriority))
                    .WithMessage("Only valid priority entry Low = 0 or Medium = 1 or High = 3");
            });

            RuleFor(taskProjectValidator => taskProjectValidator.Status)
            .NotNull()
            .WithMessage("Status cannot be null")
            .NotEmpty()
            .WithMessage("Status cannot be empty");

            RuleFor(taskProjectValidator => taskProjectValidator.IdProject)
            .NotNull()
            .WithMessage("IdProject cannot be null")
            .NotEmpty()
            .WithMessage("IdProject cannot be empty");

            RuleFor(taskProjectValidator => taskProjectValidator.Title)
            .NotNull()
            .WithMessage("Title cannot be null")
            .NotEmpty()
            .WithMessage("Title cannot be empty");

            RuleFor(taskProjectValidator => taskProjectValidator.Description)
           .NotNull()
           .WithMessage("Description cannot be null")
           .NotEmpty()
           .WithMessage("Description cannot be empty");


            RuleFor(taskProjectValidator => taskProjectValidator.IdOwner)
            .NotNull()
            .WithMessage("IdOwner cannot be null")
            .NotEmpty()
            .WithMessage("IdOwner cannot be empty");


            When(taskProjectValidator => taskProjectValidator.IdProject != null, () =>
            {

                RuleFor(taskProjectValidator => taskProjectValidator)
                    .Must(taskProjectValidator =>
                     NotExceedMaximumTask(taskProjectValidator.IdProject,numberMaxTasks))
                    .WithMessage(string.Format("Exceeded the maximum number of {0} tasks per project",numberMaxTasks));
            });

        }

        private bool NotExceedMaximumTask(int idProjet, int numberMaxTask)
        {
          var result = _taskProjectValidatorService.ExceedMaximumTask(idProjet, numberMaxTask).Result;

          return result == true ? false : true ;
        }

    }
}
