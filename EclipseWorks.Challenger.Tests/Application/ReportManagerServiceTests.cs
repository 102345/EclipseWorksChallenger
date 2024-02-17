using AutoFixture.Idioms;
using EclipseWorks.Challenger.Application.Services;
using EclipseWorks.Challenger.Domain.Entities;
using EclipseWorks.Challenger.Domain.Enums;
using EclipseWorks.Challenger.Tests.AutoFixture;
using NSubstitute;

namespace EclipseWorks.Challenger.Tests.Application
{
    public  class ReportManagerServiceTests
    {
        [Theory, AutoNSubstituteData]
        public void Sut_Should_GuardItsClause(GuardClauseAssertion assertion)
        {
            assertion.Verify(typeof(ReportManagerService).GetConstructors());
        }


        [Theory, AutoNSubstituteData]
        public async Task GetAllDeliveries_Should_Return_Report_Data(ReportManagerService sut, List<ReportManager> reportManagers)
        {
            int idProject = 1;
            int Status = (int)EnumStatusTask.Completed;
            int idOwner = 1;

            sut._unitOfWork.BeginTransaction();

            sut._unitOfWork.ReportManagers.GetAllDeliveries(idProject, Status, idOwner).Returns(reportManagers);

            sut._unitOfWork.Commit();

            var result = await sut.GetAllDeliveries(idProject,Status, idOwner);

            result.Equals(reportManagers);

        }
    }
}
