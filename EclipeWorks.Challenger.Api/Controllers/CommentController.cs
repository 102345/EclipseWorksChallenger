using AutoMapper;
using EclipseWorks.Challenger.Application.Contracts;
using EclipseWorks.Challenger.Application.Services.Interfaces;
using EclipseWorks.Challenger.Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace EclipeWorks.Challenger.Api.Controllers
{
    [ApiController]
    [Route("eclipseworks/api/project/task/comment")]
    public class CommentController : Controller
    {

        private readonly IMapper _mapper;
        public ICommentService _commentService;
        private readonly IValidator<CommentModel> _validator;

        public CommentController(IMapper mapper, ICommentService commentService, IValidator<CommentModel> validator)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _commentService = commentService ?? throw new ArgumentNullException(nameof(commentService));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CommentModel commentModel)
        {

            var validationResult = await _validator.ValidateAsync(commentModel);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }


            var comment = _mapper.Map<CommentModel, Comment>(commentModel);

            await _commentService.CreateCommentAsync(comment);

            return NoContent();

        }
    }
}
