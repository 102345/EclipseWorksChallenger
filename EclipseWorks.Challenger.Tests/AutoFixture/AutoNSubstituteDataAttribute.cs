using AutoFixture.Xunit2;
using AutoFixture;
using AutoFixture.AutoNSubstitute;

namespace EclipseWorks.Challenger.Tests.AutoFixture
{
    public class AutoNSubstituteDataAttribute : AutoDataAttribute
    {
        public AutoNSubstituteDataAttribute()
            : base(FixtureFactory)
        {
        }

        public static IFixture FixtureFactory()
        {
            var fixture = new Fixture()
                .Customize(new AutoNSubstituteCustomization { ConfigureMembers = true });

            fixture.Behaviors
                .OfType<ThrowingRecursionBehavior>()
                .ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));

            fixture.Behaviors.Add(new OmitOnRecursionBehavior(1));
            fixture.RepeatCount = 2;

            return fixture;
        }
    }
}
