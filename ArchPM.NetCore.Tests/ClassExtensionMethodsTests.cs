﻿using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using ArchPM.NetCore.Extensions;
using ArchPM.NetCore.Tests.ClassExtensionSampleClassStructure;
using FluentAssertions;

namespace ArchPM.NetCore.Tests
{
    public class ClassExtensionMethodsTests
    {
        [Fact]
        public void Should_create_instance_class_having_arguments()
        {
            var cls = Creator.CreateInstance<ClassHavingArguments>();

            cls.Should().NotBeNull();
        }

        [Fact]
        public void Should_create_instance_class_having_no_arguments()
        {
            var cls = Creator.CreateInstance<ClassHavingNoArguments>();

            cls.Should().NotBeNull();
        }

        [Fact]
        public void Should_create_sample_data_for_a_class_having_public_parameterless_constructor()
        {
            var cls = new ClassHavingNoArguments();

            var instance = cls.CreateSampleData();

            instance.Priority.Should().Be(0);
            instance.Type.Should().Be(nameof(instance.Type));
            instance.Value.Should().Be(nameof(instance.Value));
        }

        [Fact]
        public void Should_create_sample_data_for_a_class_having_public_parameter_constructor()
        {
            var cls = Creator.CreateInstance<ClassHavingArguments>();

            var instance = cls.CreateSampleData();

            instance.Priority.Should().Be(0);
            instance.Type.Should().Be(nameof(instance.Type));
            instance.Value.Should().Be(nameof(instance.Value));
        }

        [Fact]
        public void Should_create_sample_data_for_inherited_class()
        {
            var cls = Creator.CreateInstance<InheritedClass>();

            var instance = cls.CreateSampleData();

            instance.Addressing.Should().Be(nameof(instance.Addressing));
            instance.City.Should().Be(nameof(instance.City));
            instance.CountryIsoAlpha2Code.Should().Be(nameof(instance.CountryIsoAlpha2Code));
            instance.AdditionalLines.Should().BeEmpty().Should().NotBeNull();
        }

        [Fact]
        public void Should_create_sample_data_for_having_another_class_as_property()
        {
            var cls = Creator.CreateInstance<ClassHavingAnotherClassAsProperty>();

            var instance = cls.CreateSampleData();

            instance.Address.Should().NotBeNull();
            instance.Address.Name.Should().Be(nameof(instance.Address.Name));
        }

        [Theory]
        [InlineData("","","{0}")]
        [InlineData("pre_","", "pre_{0}")]
        [InlineData("","_suf", "{0}_suf")]
        [InlineData("pre_","_suf", "pre_{0}_suf")]
        public void Should_create_sample_data_with_config(string prefix, string suffix, string result)
        {
            var cls = new ClassHavingNoArguments();

            var instance = cls.CreateSampleData(
                new CreateSampleDataConfiguration() {
                    AlwaysUsePrefixForStringAs = prefix,
                    AlwaysUseSuffixForStringAs = suffix });

            instance.Priority.Should().Be(0);
            instance.Type.Should().Be(String.Format(result,instance.Type));
            instance.Value.Should().Be(String.Format(result, instance.Value));
        }
    }
}
