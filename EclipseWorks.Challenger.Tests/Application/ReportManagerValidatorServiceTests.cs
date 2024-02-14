using AutoFixture.Idioms;
using EclipseWorks.Challenger.Application.Services;
using EclipseWorks.Challenger.Domain.Entities;
using EclipseWorks.Challenger.Domain.Enums;
using EclipseWorks.Challenger.Tests.AutoFixture;
using FluentAssertions;
using NSubstitute;

namespace EclipseWorks.Challenger.Tests.Application
{
    public class ReportManagerValidatorServiceTests
    {
        [Theory, AutoNSubstituteData]
        public void Sut_Should_GuardItsClause(GuardClauseAssertion assertion)
        {
            assertion.Verify(typeof(ReportManagerValidatorService).GetConstructors());
        }

        [Theory, AutoNSubstituteData]
        public async Task IsManager_Should_Return_False(ReportManagerValidatorService sut, Owner owner)
        {

            var idOwnerAuthorized = 1;
            owner.IdPosition = (int)EnumRoleOwner.SuportIT;

            sut._unitOfWork.Owners.GetById(idOwnerAuthorized).Returns(owner);

            var result = await sut.IsManager(idOwnerAuthorized);

            result.Should().BeFalse();

        }

        [Theory, AutoNSubstituteData]
        public async Task IsManager_Should_Return_True(ReportManagerValidatorService sut, Owner owner)
        {

            var idOwnerAuthorized = 1;
            owner.IdPosition = (int)EnumRoleOwner.Manager;

            sut._unitOfWork.Owners.GetById(idOwnerAuthorized).Returns(owner);

            var result = await sut.IsManager(idOwnerAuthorized);

            result.Should().BeTrue();

        }
    }
}
