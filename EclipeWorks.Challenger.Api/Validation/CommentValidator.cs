using EclipseWorks.Challenger.Application.Contracts;
using EclipseWorks.Challenger.Application.Services.Interfaces;
using FluentValidation;

namespace EclipeWorks.Challenger.Api.Validation
{
    public class CommentValidator : AbstractValidator<CommentModel>
    {
        public ITaskProjectService  _taskProjectService;
        public CommentValidator(ITaskProjectService taskProjectService)
        {
            _taskProjectService = taskProjectService ?? throw new ArgumentNullException(nameof(taskProjectService));

            RuleFor(commentValidator => commentValidator.IdTask)
              .NotNull()
              .WithMessage("IdPriority cannot be null")
              .NotEmpty()
              .WithMessage("IdPriority cannot be empty");

            RuleFor(commentValidator => commentValidator.Description)
            .NotNull()
            .WithMessage("Description cannot be null")
            .NotEmpty()
            .WithMessage("Description cannot be empty");

            When(commentValidator => commentValidator.IdTask != null, () =>
            {

                RuleFor(commentValidator => commentValidator)
                    .Must(commentValidator =>
                     ExistIdTask(commentValidator.IdTask))
                    .WithMessage(string.Format("There is no Task with this IdTask"));
            });

        }

        private bool ExistIdTask(int idTask)
        {
            var task = _taskProjectService.GetById(idTask).Result;

            return task != null ? true : false;

        }
    }
}
