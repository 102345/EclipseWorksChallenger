namespace EclipseWorks.Challenger.Domain.Entities
{
    public class Comment
    { 
        public int IdComment { get; set; }
        public int IdTask { get; set; }
        public string Description { get; set; }
        public DateTime  CreatedAt { get; set; }
    }
}
