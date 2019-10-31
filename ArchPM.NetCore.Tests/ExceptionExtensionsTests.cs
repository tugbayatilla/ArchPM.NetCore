using ArchPM.NetCore.Extensions;
using System;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace ArchPM.NetCore.Tests
{
    public class ExceptionExtensionsTests
    {
        [Fact]
        public void GetAllExceptions_Should_get_all_3_exceptions()
        {
            var exception = new Exception("parent", new Exception("first_child", new Exception("second_child")));
            var exceptionList = exception.GetAllExceptions();
            exceptionList.Should().HaveCount(3);
        }

        [Fact]
        public void GetAllExceptions_Should_not_return_null()
        {
            var exceptionList = ((Exception) null).GetAllExceptions();
            var exceptions = exceptionList.ToList();
            exceptions.Should().NotBeNull();
            exceptions.Should().BeEmpty();
        }

        [Fact]
        public void GetAllMessages_Should_return_valid_messages()
        {
            var exception = new Exception("parent", new ArgumentOutOfRangeException("first_child", new Exception("second_child")));

            var message = exception.GetAllMessages();
            message.Should().Be($"[{typeof(Exception).Name}]:parent\r\n[{typeof(ArgumentOutOfRangeException).Name}]:first_child\r\n[{typeof(Exception).Name}]:second_child\r\n");
        }

        [Fact]
        public void GetAllMessages_Should_not_return_null()
        {
            var message = ((Exception) null).GetAllMessages();
            message.Should().Be("");
        }

        [Fact]
        public void GetAllMessages_Should_change_message_format_with_predicate()
        {
            var exception = new Exception("parent", new ArgumentOutOfRangeException("first_child", new Exception("second_child")));

            var message = exception.GetAllMessages(ex => $"{ex.Message}=>");
            message.Should().Be($"parent=>first_child=>second_child=>");
        }

        [Fact]
        public void Should_throw_exception_when_object_is_null()
        {
            var ex = Assert.Throws<Exception>(()=> {
                ((object) null).ThrowExceptionIf(p => p == null);
            });

            Assert.Equal($"An object 'obj' instance can't be null", ex.Message);
        }

        [Fact]
        public void Should_throw_exception_when_predicate_is_null()
        {
            var ex = Assert.Throws<Exception>(() => {
                ((object) null).ThrowExceptionIf(null);
            });

            Assert.Equal("ThrowExceptionIf ExtensionMethod first parameter predicate is null!", ex.Message);
        }

        [Fact]
        public void Should_throw_given_exception_when_object_is_null()
        {
            var ex = Assert.Throws<ArgumentException>(() => {
                ((object) null).ThrowExceptionIf(p => p == null, new ArgumentException("obj"));
            });

            Assert.Equal("obj", ex.Message);
        }

        [Fact]
        public void Should_throw_argument_null_exception_when_object_is_null_while_calling_ThrowExceptionIfNull()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => {
                ((object) null).ThrowExceptionIfNull<ArgumentNullException>();
            });

            Assert.Equal($"Value cannot be null.", ex.Message);
        }

        [Fact]
        public void Should_throw_given_exception_with_given_message_when_object_is_null()
        {
            var ex = Assert.Throws<StackOverflowException>(() => {
                ((object) null).ThrowExceptionIfNull<StackOverflowException>("this is silly but works!");
            });

            Assert.Equal("this is silly but works!", ex.Message);
        }

        [Fact]
        public void Should_throw_exception_when_object_is_string_and_null()
        {
            var ex = Assert.Throws<StackOverflowException>(() => {
                ((string) null).ThrowExceptionIfNull<StackOverflowException>("string is null");
            });

            Assert.Equal("string is null", ex.Message);
        }
        [Fact]
        public void Should_throw_exception_when_object_is_string_and_null_or_empty()
        {
            var obj = "";

            var ex = Assert.Throws<StackOverflowException>(() => {
                obj.ThrowExceptionIfNull<StackOverflowException>("string is null");
            });

            Assert.Equal("string is null", ex.Message);
        }
    }
}
