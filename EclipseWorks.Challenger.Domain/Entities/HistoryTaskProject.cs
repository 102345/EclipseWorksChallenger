namespace EclipseWorks.Challenger.Domain.Entities
{
    public class HistoryTaskProject
    {
        public int IdHistoryTask { get; set; }

        public int Status { get; set; }

        public int? IdComment { get; set; }

        public string? DescriptionComment { get; set; }

        public int IdOwner { get; set; }

        public int IdProject { get; set; }

        public int? IdTask { get; set; }

        public string? DescriptionTask { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }
    }
}
