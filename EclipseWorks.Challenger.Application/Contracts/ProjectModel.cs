using System.Diagnostics.CodeAnalysis;

namespace EclipseWorks.Challenger.Application.Contracts
{
    [ExcludeFromCodeCoverage]
    public class ProjectModel
    {
        public string NameProject { get; set; }

        public string Observation { get; set; }
        public int IdOwner { get; set; }
    }
}
