using System.Linq;
using AutoFixture;
using AutoFixture.AutoMoq;
using NUnit.Framework;

namespace UT
{
    public abstract class BaseTest
    {
        protected Fixture BaseFixture;

        protected virtual void SetUp()
        {
            BaseFixture = new Fixture();
            BaseFixture.Customize(new AutoMoqCustomization());

            BaseFixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => BaseFixture.Behaviors.Remove(b));
            BaseFixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
    }
}