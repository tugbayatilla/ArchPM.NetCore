using ArchPM.NetCore.Extensions;
using Xunit;
using FluentAssertions;
using ArchPM.NetCore.Tests.TestModels;
// ReSharper disable ObjectCreationAsStatement

namespace ArchPM.NetCore.Tests
{
    public class CheckExtensionsTests
    {
        [Fact]
        public void IfNull_Should_create_new_instance_of_the_object()
        {
            var @class = new SampleDataClass();
            @class.IfNull(p => new SampleDataClass());

            @class.Should().NotBeNull();
        }


    }
}
