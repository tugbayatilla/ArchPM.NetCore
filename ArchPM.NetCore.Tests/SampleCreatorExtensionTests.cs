using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using ArchPM.NetCore.Extensions;
using ArchPM.NetCore.Tests.ClassExtensionSampleClassStructure;
using FluentAssertions;
using ArchPM.NetCore.Tests.TestModel;
using ArchPM.NetCore.Tests.SampleDataClasses;

namespace ArchPM.NetCore.Tests
{
    public class SampleCreatorExtensionTests
    {
        [Fact]
        public void Should_fill_primitive_values()
        {
            var now = DateTime.Now;
            var sample = SampleCreator.CreateSample<SampleDataClass>(new SampleCreator.SampleConfiguration() { AlwaysUseDateTimeAs = now });

            sample.Should().NotBeNull();
            sample.BooleanProperty.Should().BeTrue();
            sample.BooleanPropertyNullable.Should().BeTrue();

            sample.ByteProperty.Should().Be(1273 / 128);
            sample.BytePropertyNullable.Should().Be(2088 / 128);

            sample.DateTimeProperty.Should().Be(now.AddDays(1650)); //adds days according to name
            sample.DateTimePropertyNullable.Should().Be(now.AddDays(2465));

            sample.DecimalProperty.Should().Be(1556); //number is calculated according to name
            sample.DecimalPropertyNullable.Should().Be(2371);

            sample.DoubleProperty.Should().Be(1472);
            sample.DoublePropertyNullable.Should().Be(2287);

            sample.FloatProperty.Should().Be(1371);
            sample.FloatPropertyNullable.Should().Be(2186);

            sample.Int16Property.Should().Be(1271);
            sample.Int16PropertyNullable.Should().Be(2086);

            sample.Int64Property.Should().Be(1274);
            sample.Int64PropertyNullable.Should().Be(2089);

            sample.IntProperty.Should().Be(1168);
            sample.IntPropertyNullable.Should().Be(1983);

            sample.SingleProperty.Should().Be(1479);
            sample.SinglePropertyNullable.Should().Be(2294);

            sample.StringProperty.Should().Be("StringProperty");
            sample.StringPropertyNullable.Should().Be("StringPropertyNullable");

            sample.UInt16Property.Should().Be(1356);
            sample.UInt16PropertyNullable.Should().Be(2171);

            sample.UInt64Property.Should().Be(1359);
            sample.UInt64PropertyNullable.Should().Be(2174);

            sample.UIntProperty.Should().Be(1253);
            sample.UIntPropertyNullable.Should().Be(2068);

        }


        [Fact]
        public void Should_fill_simple_sub_class_property()
        {
            var now = DateTime.Now;
            var sample = SampleCreator.CreateSample<SampleDataClass>();

            sample.SampleDataSimpleSubClass.Should().NotBeNull();
            sample.SampleDataSimpleSubClass.IntProperty.Should().Be(1168);
            sample.SampleDataSimpleSubClass.StringProperty.Should().Be("StringProperty");
        }

        [Fact]
        public void Should_fill_inherited_sub_class_property()
        {
            var now = DateTime.Now;
            var sample = SampleCreator.CreateSample<SampleDataClass>(new SampleCreator.SampleConfiguration() { AlwaysUseDateTimeAs = now });

            sample.SampleDataInheritedSubClass.Should().NotBeNull();
            sample.SampleDataInheritedSubClass.DateTimeProperty.Should().Be(now.AddDays(1650));
        }

        [Fact]
        public void Should_fill_primitive_constructor_sub_class_property()
        {
            var now = DateTime.Now;
            var sample = SampleCreator.CreateSample<SampleDataClass>(new SampleCreator.SampleConfiguration() { AlwaysUseDateTimeAs = now });

            sample.SampleDataPrimitiveConstructorSubClass.Should().NotBeNull();
            sample.SampleDataPrimitiveConstructorSubClass.DateTimeProperty.Should().Be(now.AddDays(1650));
        }

        [Fact]
        public void Should_fill_generic_list_property()
        {
            var now = DateTime.Now;
            var sample = SampleCreator.CreateSample<SampleDataClass>(new SampleCreator.SampleConfiguration() { AlwaysUseDateTimeAs = now });

            sample.SampleDataSimpleSubClassList.Should().NotBeNull();
            sample.SampleDataSimpleSubClassList.Count.Should().Be(2);

            sample.SampleDataSimpleSubClassList[0].IntProperty.Should().Be(1168);
        }



    }
}
