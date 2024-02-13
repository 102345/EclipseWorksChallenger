namespace EclipseWorks.Challenger.Application.Contracts
{
    public class FilterReportManagerModelRequest
    {
        public int idOwnerAuthorized {  get; set; }

        public int? idProject { get; set; }
            
        public int? Status { get; set; }

        public int? idOwner { get; set; }
    }
}
