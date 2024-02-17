using System.Diagnostics.CodeAnalysis;

namespace EclipseWorks.Challenger.Application.Contracts
{
    [ExcludeFromCodeCoverage]
    public class CommentModel
    {
        public int IdTask { get; set; }
        public string Description { get; set; }
    }
}
