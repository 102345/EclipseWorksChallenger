namespace EclipseWorks.Challenger.Application.Contracts
{
    public class ProjectModelResponse
    {
        public int IdProject { get; set; }

        public string NameProject { get; set; }

        public string Observation { get; set; }

        public string CreatedAt { get; set; }

        public int IdOwner { get; set; }
    }
}
