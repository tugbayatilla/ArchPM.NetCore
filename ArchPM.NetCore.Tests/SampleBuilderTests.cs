using System;
using ArchPM.NetCore.Builders;
using Xunit;
using FluentAssertions;
using ArchPM.NetCore.Tests.TestModels;

namespace ArchPM.NetCore.Tests
{
    public class SampleBuilderTests
    {
        [Fact]
        public void Should_dictionary_initialized()
        {
            var config = new SampleBuilderConfiguration(){CollectionCount = 2};
            //Act
            var p1 = SampleBuilder.Create<SampleDataWithDictionary>(config);

            //Assert
            p1.DictionaryProperty.Should().NotBeNull();
            //p1.DictionaryProperty.Should().HaveCount(config.CollectionCount);
        }


        [Fact]
        public void Should_use_same_config_and_equal()
        {
            //Arrange
            var config = new SampleBuilderConfiguration() { AlwaysUseDateTimeAs = DateTime.Now };

            //Act
            var p1 = SampleBuilder.Create<SampleDataClassForEquality>(config);
            var p2 = SampleBuilder.Create<SampleDataClassForEquality>(config);

            //Assert
            p1.Should().Be(p2);
        }

        [Fact]
        public void Should_use_same_config_and__not_equal()
        {
            //Arrange
            var config = new SampleBuilderConfiguration() { AlwaysUseDateTimeAs = DateTime.Now };

            //Act
            var p1 = SampleBuilder.Create<SampleDataClassForEquality>(config);
            var p2 = SampleBuilder.Create<SampleDataClassForEquality>(p => p.IntValue = -1, config);

            //Assert
            p1.Should().NotBe(p2);
        }


        [Fact]
        public void Should_run_extension_method()
        {
            var cls = new ClassHavingEnumProperty();
            var sample = cls.CreateSample(p => p.SampleEnumProperty = ClassHavingEnumProperty.SampleEnum.Value2);

            sample.SampleEnumProperty.Should().Be(ClassHavingEnumProperty.SampleEnum.Value2);
        }


        [Fact]
        public void Should_fill_class_having_enum_property()
        {
            var sample = SampleBuilder.Create<ClassHavingEnumProperty>(p => p.SampleEnumProperty = ClassHavingEnumProperty.SampleEnum.Value2);

            sample.SampleEnumProperty.Should().Be(ClassHavingEnumProperty.SampleEnum.Value2);
        }


        [Fact]
        public void Should_create_and_fill_when_having_ilist_interface_property()
        {
            var now = DateTime.Now;
            var instance = SampleBuilder.Create<ClassHavingIListInterfaceAsProperty>(null, p =>
             {
                 p.AlwaysUseDateTimeAs = now;
             });

            instance.Should().NotBeNull();
            instance.IListInterfaceProperty.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void Should_create_private_constructor_property_be_null()
        {
            var instance = SampleBuilder.Create<SampleDataClass>();

            instance.Should().NotBeNull();
            instance.SampleDataPrivateConstructorSubClass.Should().BeNull();
        }


        [Fact]
        public void Should_create_and_fill_company()
        {
            var now = DateTime.Now;
            var instance = SampleBuilder.Create<Company>(null, p =>
             {
                 p.AlwaysUseDateTimeAs = now;
                 p.AlwaysUseDateTimeAddition = SampleDateTimeAdditions.AddDays;
             });

            instance.Should().NotBeNull();

            instance.CompanyId.Should().Be(900);
            instance.Branches.Should().NotBeNull();
            instance.Closed.Should().Be(now.AddDays(602));

            instance.MainAddress.Should().NotBeNull();

        }

        [Fact]
        public void Should_fill_primitive_values()
        {
            var now = DateTime.Now;
            var sample = SampleBuilder.Create<SampleDataClass>(null, p =>
            {
                p.AlwaysUseDateTimeAs = now;
                p.AlwaysUseDateTimeAddition = SampleDateTimeAdditions.AddMinutes;
            });

            sample.Should().NotBeNull();
            sample.BooleanProperty.Should().BeTrue();
            sample.BooleanPropertyNullable.Should().BeTrue();

            sample.ByteProperty.Should().Be(1273 / 128);
            sample.BytePropertyNullable.Should().Be(2088 / 128);

            sample.DateTimeProperty.Should().Be(now.AddMinutes(1650)); //adds days according to name
            sample.DateTimePropertyNullable.Should().Be(now.AddMinutes(2465));

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

            sample.GuidProperty.Should().Be(Guid.Parse("00000000-0000-0000-0000-000000000001"));
            sample.GuidPropertyNullable.Should().Be(Guid.Parse("00000000-0000-0000-0000-000000000001"));
        }


        [Fact]
        public void Should_fill_simple_sub_class_property()
        {
            var sample = SampleBuilder.Create<SampleDataClass>();

            sample.SampleDataSimpleSubClass.Should().NotBeNull();
            sample.SampleDataSimpleSubClass.IntProperty.Should().Be(1168);
            sample.SampleDataSimpleSubClass.StringProperty.Should().Be("StringProperty");
        }

        [Fact]
        public void Should_fill_inherited_sub_class_property()
        {
            var now = DateTime.Now;
            var sample = SampleBuilder.Create<SampleDataClass>(null, p =>
            {
                p.AlwaysUseDateTimeAs = now;
                p.AlwaysUseDateTimeAddition = SampleDateTimeAdditions.AddSeconds;
            });

            sample.SampleDataInheritedSubClass.Should().NotBeNull();
            sample.SampleDataInheritedSubClass.DateTimeProperty.Should().Be(now.AddSeconds(1650));
        }

        [Fact]
        public void Should_fill_primitive_constructor_sub_class_property()
        {
            var now = DateTime.Now;
            var sample = SampleBuilder.Create<SampleDataClass>(null, p =>
             {
                 p.AlwaysUseDateTimeAs = now;
                 p.AlwaysUseDateTimeAddition = SampleDateTimeAdditions.AddDays;
             });

            sample.SampleDataPrimitiveConstructorSubClass.Should().NotBeNull();
            sample.SampleDataPrimitiveConstructorSubClass.DateTimeProperty.Should().Be(now.AddDays(1650));
        }

        [Fact]
        public void Should_fill_generic_list_property()
        {
            var sample = SampleBuilder.Create<SampleDataClass>();

            sample.SampleDataSimpleSubClassList.Should().NotBeNull();
            sample.SampleDataSimpleSubClassList.Count.Should().Be(2);

            sample.SampleDataSimpleSubClassList[0].IntProperty.Should().Be(1168);
        }

        [Fact]
        public void Should_fill_generic_list_interface_property()
        {
            var sample = SampleBuilder.Create<SampleDataClass>();

            sample.SampleDataSimpleSubClassIListInterface.Should().NotBeNullOrEmpty();
            sample.SampleDataSimpleSubClassIListInterface[0].IntProperty.Should().Be(1168);
        }

        [Fact]
        public void Should_fill_generic_collection_interface_property()
        {
            var sample = SampleBuilder.Create<SampleDataClass>();

            sample.SampleDataSimpleSubClassICollectionInterface.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void Should_fill_generic_enumeration_interface_property()
        {
            var sample = SampleBuilder.Create<SampleDataClass>();

            sample.SampleDataSimpleSubClassIEnumerableInterface.Should().NotBeNullOrEmpty();
        }


        [Fact]
        public void Should_create_sample_data_for_a_class_having_public_parameterless_constructor()
        {
            var instance = SampleBuilder.Create<ClassHavingNoArguments>();

            instance.Priority.Should().Be(866);
            instance.Type.Should().Be(nameof(instance.Type));
            instance.Value.Should().Be(nameof(instance.Value));
        }

        [Fact]
        public void Should_create_sample_data_for_a_class_having_public_parameter_constructor()
        {
            var instance = SampleBuilder.Create<ClassHavingArguments>();

            instance.Priority.Should().Be(866);
            instance.Type.Should().Be(nameof(instance.Type));
            instance.Value.Should().Be(nameof(instance.Value));
        }

        [Fact]
        public void Should_create_sample_data_for_inherited_class()
        {
            var instance = SampleBuilder.Create<InheritedClass>();

            instance.Addressing.Should().Be(nameof(instance.Addressing));
            instance.City.Should().Be(nameof(instance.City));
            instance.CountryIsoAlpha2Code.Should().Be(nameof(instance.CountryIsoAlpha2Code));
            instance.AdditionalLines.Count.Should().Be(2);
            instance.AdditionalLines[0].Should().Be("string_0");
            instance.AdditionalLines[1].Should().Be("string_1");
        }

        [Fact]
        public void Should_create_sample_data_for_having_another_class_as_property()
        {
            var instance = SampleBuilder.Create<ClassHavingAnotherClassAsProperty>();

            instance.Address.Should().NotBeNull();
            instance.Address.Name.Should().Be(nameof(instance.Address.Name));
            instance.Address.AdditionalLines.Count.Should().Be(2);
            instance.Address.AdditionalLines[0].Should().Be("string_0");
            instance.Address.AdditionalLines[1].Should().Be("string_1");

        }

        [Theory]
        [InlineData("", "", "{0}")]
        [InlineData("pre_", "", "pre_{0}")]
        [InlineData("", "_suf", "{0}_suf")]
        [InlineData("pre_", "_suf", "pre_{0}_suf")]
        public void Should_create_sample_data_with_config(string prefix, string suffix, string result)
        {
            var instance = SampleBuilder.Create<ClassHavingNoArguments>(null,
                p =>
                {
                    p.AlwaysUsePrefixForStringAs = prefix;
                    p.AlwaysUseSuffixForStringAs = suffix;
                });

            instance.Priority.Should().Be(866);
            instance.Type.Should().Be(string.Format(result, nameof(instance.Type)));
            instance.Value.Should().Be(string.Format(result, nameof(instance.Value)));
        }
        
        [Fact]
        public void Should_be_create_new_sample_with_constructor()
        {
            //Arrange
            //Act
            var p1 = SampleBuilder.Create<SampleUser>();

            //Assert
            p1.Should().NotBeNull();
        }
        
        [Fact]
        public void Should_be_recursive_list_property_must_be_filled()
        {
            //Arrange
            //Act
            var p1 = SampleBuilder.Create<SampleRecursiveListClass.A>();

            //Assert
            p1.Should().NotBeNull();
            p1.Ref_B.Ref_C.Ref_A_list.Should().NotBeNull();
            p1.Ref_B.Ref_C.Ref_A_list.Count.Should().Be(2);
            p1.Ref_B.Ref_C.Ref_A_list[0].Should().NotBeNull();
            p1.Ref_B.Ref_C.Ref_A_list[0].Ref_B.Should().NotBeNull();
            p1.Ref_B.Ref_C.Ref_A_list[0].Ref_B.Ref_C.Should().NotBeNull();
            p1.Ref_B.Ref_C.Ref_A_list[0].Ref_B.Ref_C.Ref_A_list.Should().NotBeNull();
            p1.Ref_B.Ref_C.Ref_A_list[0].Ref_B.Ref_C.Ref_A_list[0].Should().NotBeNull();
        }
        
        [Fact]
        public void Should_be_nullable_enum_class_be_filled()
        {
            //Arrange
            //Act
            var p1 = SampleBuilder.Create<SampleNullableEnumClass>();

            //Assert
            p1.Should().NotBeNull();
            p1.SampleNullableEnum.Should().Be(SampleNullableEnum.OnlyItem);
        }
        
        [Fact]
        public void Should_be_nullable_enum_class_be_filled_and_enum_index_1_picked()
        {
            //Arrange
            //Act
            var p1 = SampleBuilder.Create<SampleNullableEnumClass>(new SampleBuilderConfiguration()
            {
                AlwaysPickEnumItemIndex = 1
            });

            //Assert
            p1.Should().NotBeNull();
            p1.SampleNullableEnum.Should().Be(SampleNullableEnum.OnlyItem2);
        }

    }
}
