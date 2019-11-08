using ArchPM.NetCore.Tests.TestModels;
using ArchPM.NetCore.Utilities;
using FluentAssertions;
using Xunit;

namespace ArchPM.NetCore.Tests.UtilitiesTests
{
    public class ClassUtilitiesTests
    {
        [Theory]
        [InlineData("ConstantName", "ConstantName", false, ClassItemType.Constant)]
        [InlineData("Name2", "Name2", false, ClassItemType.Property)]
        [InlineData("Name3", "Name3", false, ClassItemType.Property)]
        [InlineData("IntNullable", 1, true, ClassItemType.Property)]
        public void GetItemByName_should_get_valid_class_item_from_type(string name, object value, bool nullable, ClassItemType itemType)
        {
            var @class = ClassUtilities.GetItemByName<SampleClassUtilities>(name);

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
            var item = new SampleClassUtilities();

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
            var item = new SampleClassUtilities();

            var items = item.GetItems();

            items.Should().NotBeNull();
            items.Count.Should().Be(6);
        }


    }
}
