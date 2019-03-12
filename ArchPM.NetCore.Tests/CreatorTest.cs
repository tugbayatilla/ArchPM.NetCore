using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using ArchPM.NetCore.Extensions;
using ArchPM.NetCore.Tests.ClassExtensionSampleClassStructure;
using FluentAssertions;
using ArchPM.NetCore.Tests.TestModel;

namespace ArchPM.NetCore.Tests
{
    public class CreatorTests
    {
        [Fact]
        public void Should_create_instance_class_having_arguments()
        {
            var cls = Creator.CreateInstance<ClassHavingArguments>();

            cls.Should().NotBeNull();
        }


        [Fact]
        public void Should_create_and_fill_company()
        {
            var now = DateTime.Now;
            var instance = Creator.FillWithData<Company>(new CreateSampleDataConfiguration()
            {
                AlwaysUseDateTimeAs = now
            });

            instance.Should().NotBeNull();

            instance.CompanyId.Should().Be(9);
            instance.Branches.Should().NotBeNull();
            instance.Closed.Should().Be(now);

            instance.MainAddress.Should().NotBeNull();

        }
    }
}
