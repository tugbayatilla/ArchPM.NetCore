using Xunit;
using FluentAssertions;
using ArchPM.NetCore.Tests.TestModels;
using ArchPM.NetCore.Creators;

namespace ArchPM.NetCore.Tests
{
    public class CreatorTests
    {
        [Fact]
        public void Should_create_instance_class_having_arguments()
        {
            var cls = ObjectCreator.CreateInstance<ClassHavingArguments>();

            cls.Should().NotBeNull();
        }

        [Fact]
        public void Should_create_instance_class_having_no_arguments()
        {
            var cls = ObjectCreator.CreateInstance<ClassHavingNoArguments>();

            cls.Should().NotBeNull();
        }

    }
}
