using System.Text.Json.Serialization;

namespace EclipseWorks.Challenger.Domain.Entities
{
    public class Project
    {
        public int IdProject { get; set; }

        public string NameProject { get; set; }

        public string Observation { get; set; }

        public string CreatedAt { get; set; }

        public int IdOwner { get; set; }

    }
}
