using AutoFixture.Idioms;
using EclipeWorks.Challenger.Api.Validation;
using EclipseWorks.Challenger.Tests.AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using NSubstitute;

namespace EclipseWorks.Challenger.Tests.Api.Validation
{
    public class TaskProjectValidatorTests
    {

        [Theory, AutoNSubstituteData]
        public void Sut_Should_GuardItsClause(GuardClauseAssertion assertion)
        {
            assertion.Verify(typeof(TaskProjectValidator).GetConstructors());
        }

        [Theory, AutoNSubstituteData]
        public async Task NotExceedMaximumTask_Should_Return_False(TaskProjectValidator sut)
        {

            var idProject = 1;
            var numberMaxTask = 20;

            sut._configuration.GetValue<int>("ParametersRulesBussiness:NumberMaxTasks").Returns(20);

            sut._taskProjectValidatorService.ExceedMaximumTask(idProject, numberMaxTask).Returns(false);

            var result = sut.NotExceedMaximumTask(idProject, numberMaxTask);

            result.Should().Equals(false);

        }
    }
}
