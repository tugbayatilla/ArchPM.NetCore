using ArchPM.NetCore.Tests.TestModels;
using ArchPM.NetCore.Utilities;
using FluentAssertions;
using Xunit;

namespace ArchPM.NetCore.Tests.UtilitiesTests
{
    public class ClassUtilitiesTests
    {
        [Theory]
        [InlineData("Name2")]
        [InlineData("Name3")]
        [InlineData("ConstantName")]
        public void GetItemByName_should_get_by_filter(string name)
        {
            var item = ClassUtilities.GetItemByName<SampleClassForUtilitiesTests, string>(null, name);

            item.Name.Should().Be(name);
        }

        [Fact]
        public void GetItems_should_define_multiple_filter_types()
        {
            var items = ClassUtilities.GetItems<SampleClassForUtilitiesTests>(null,typeof(int?), typeof(float));

            items.Should().NotBeNull();
            items.Count.Should().Be(2);
        }


        [Fact]
        public void GetItems_should_return_only_given_types()
        {
            var items = ClassUtilities.GetItems<SampleClassForUtilitiesTests, int?>(null);

            items.Should().NotBeNull();
            items.Count.Should().Be(1);
        }

        [Fact]
        public void GetItems_should_return_every_types()
        {
            var @class = ClassUtilities.GetItems<SampleClassForUtilitiesTests>(null);

            @class.Should().NotBeNull();
            @class.Count.Should().Be(6);

            var intNullable = @class.Find(p => p.Name == "IntNullable");
            intNullable.Value.Should().Be(null);
            intNullable.ItemType.Should().Be(ClassItemType.Property);
        }

        [Fact]
        public void GetItems_should_return_values_when_not_null()
        {
            var @class = new SampleClassForUtilitiesTests() { IntNullable = 1 }.GetItems();

            var intNullable = @class.Find(p => p.Name == "IntNullable");
            intNullable.Value.Should().Be(1);
            intNullable.ItemType.Should().Be(ClassItemType.Property);
        }


        [Theory]
        [InlineData("ConstantName", "ConstantName", false, ClassItemType.Constant)]
        [InlineData("Name2", "Name2", false, ClassItemType.Property)]
        [InlineData("Name3", "Name3", false, ClassItemType.Property)]
        [InlineData("IntNullable", 1, true, ClassItemType.Property)]
        public void GetItemByName_should_get_valid_class_item_from_type(string name, object value, bool nullable, ClassItemType itemType)
        {
            var @class = ClassUtilities.GetItemByName<SampleClassForUtilitiesTests>(name);

            @class.Should().NotBeNull();
            @class.Name.Should().Be(name);
            @class.Value.Should().Be(value);
            @class.ItemType.Should().Be(itemType);
            @class.Nullable.Should().Be(nullable);
        }

        [Theory]
        [InlineData("ConstantName", "ConstantName", false, ClassItemType.Constant)]
        [InlineData("Name2", "Name2", false, ClassItemType.Property)]
        [InlineData("Name3", "Name3", false, ClassItemType.Property)]
        [InlineData("IntNullable", 1, true, ClassItemType.Property)]
        public void GetItemByName_should_get_valid_class_item_from_instance(string name, object value, bool nullable, ClassItemType itemType)
        {
            var item = new SampleClassForUtilitiesTests();

            var @class = item.GetItemByName(name);

            @class.Should().NotBeNull();
            @class.Name.Should().Be(name);
            @class.Value.Should().Be(value);
            @class.ItemType.Should().Be(itemType);
            @class.Nullable.Should().Be(nullable);
        }


        [Fact]
        public void GetItem_should_return_all_public_items()
        {
            var item = new SampleClassForUtilitiesTests();

            var items = item.GetItems();

            items.Should().NotBeNull();
            items.Count.Should().Be(6);
        }


    }
}
