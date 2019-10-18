using ArchPM.NetCore.Builders;
using Xunit;
using FluentAssertions;
using ArchPM.NetCore.Tests.TestModels;

namespace ArchPM.NetCore.Tests
{
    public class CreatorTests
    {
        [Fact]
        public void Should_create_instance_class_having_arguments()
        {
            var cls = ObjectBuilder.CreateInstance<ClassHavingArguments>();

            cls.Should().NotBeNull();
        }

        [Fact]
        public void Should_create_instance_class_having_no_arguments()
        {
            var cls = ObjectBuilder.CreateInstance<ClassHavingNoArguments>();

            cls.Should().NotBeNull();
        }

    }
}
