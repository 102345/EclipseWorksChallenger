namespace EclipseWorks.Challenger.Domain.Entities
{
    public class TaskProject
    {
        public int IdTask { get; set; }
        public int IdPriority { get; set; }
        public int Status { get; set; }
        public int IdProject { get; set; }
        public int IdOwner { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }


    }
}
