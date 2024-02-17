using System.Diagnostics.CodeAnalysis;

namespace EclipseWorks.Challenger.Application.Contracts
{
    [ExcludeFromCodeCoverage]
    public class TaskProjectUpdateModel
    {   
        public int IdTask { get; set; }
        public int Status { get; set; }
        public int IdProject { get; set; }
        public int IdOwner { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
    }
}
