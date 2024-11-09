using System.Diagnostics.CodeAnalysis;

namespace EclipseWorks.Challenger.Application.Contracts
{   
    //Contrato de requisição de Relatorio Gerencial
    [ExcludeFromCodeCoverage]
    public class FilterReportManagerModelRequest
    {
        public int idOwnerAuthorized {  get; set; }

        public int? idProject { get; set; }
            
        public int? Status { get; set; }

        public int? idOwner { get; set; }
    }
}
