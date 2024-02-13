using AutoMapper;
using EclipseWorks.Challenger.Application.Contracts;
using EclipseWorks.Challenger.Application.Services.Interfaces;
using EclipseWorks.Challenger.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EclipeWorks.Challenger.Api.Controllers
{
    [ApiController]
    [Route("eclipseworks/api/project/task/comment")]
    public class CommentController : Controller
    {

        private readonly IMapper _mapper;
        public ICommentService _commentService;

        public CommentController(IMapper mapper, ICommentService commentService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _commentService = commentService ?? throw new ArgumentNullException(nameof(commentService));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CommentModel commentModel)
        {

            //var validationResult = await _validator.ValidateAsync(customerSupplierRequest);

            //if (!validationResult.IsValid)
            //{
            //    return BadRequest(validationResult.Errors);
            //}



            var comment = _mapper.Map<CommentModel, Comment>(commentModel);

            await _commentService.CreateCommentAsync(comment);

            return NoContent();

        }
    }
}
