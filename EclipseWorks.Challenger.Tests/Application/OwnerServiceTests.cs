using AutoFixture.Idioms;
using EclipseWorks.Challenger.Application.Services;
using EclipseWorks.Challenger.Domain.Entities;
using EclipseWorks.Challenger.Tests.AutoFixture;
using NSubstitute;

namespace EclipseWorks.Challenger.Tests.Application
{
    public class OwnerServiceTests
    {
        [Theory, AutoNSubstituteData]
        public void Sut_Should_GuardItsClause(GuardClauseAssertion assertion)
        {
            assertion.Verify(typeof(OwnerService).GetConstructors());
        }


        [Theory, AutoNSubstituteData]
        public async Task GetById_Should_Return_Owner(OwnerService sut, Owner owner)
        {
            int idOwner = 1;
            sut._unitOfWork.BeginTransaction();

            sut._unitOfWork.Owners.GetById(idOwner).Returns(owner);

            sut._unitOfWork.Commit();

            var result = await sut.GetById(idOwner);

            result.Equals(owner);

        }
    }
}
