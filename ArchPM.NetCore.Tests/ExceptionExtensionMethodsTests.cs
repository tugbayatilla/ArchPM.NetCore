using ArchPM.NetCore.Extensions;
using System;
using Xunit;

namespace ArchPM.NetCore.Tests
{
    public class ExceptionExtensionMethodsTests
    {
        [Fact]
        public void Should_throw_exception_when_object_is_null()
        {
            object obj = null;

            var ex = Assert.Throws<Exception>(()=> {
                obj.ThrowExceptionIf(p => p == null);
            });

            Assert.Equal($"An object '{nameof(obj)}' instance can't be null", ex.Message);
        }

        [Fact]
        public void Should_throw_exception_when_predicate_is_null()
        {
            object obj = null;

            var ex = Assert.Throws<Exception>(() => {
                obj.ThrowExceptionIf(null);
            });

            Assert.Equal("ThrowExceptionIf ExtensionMethod first parameter predicate is null!", ex.Message);
        }

        [Fact]
        public void Should_throw_given_exception_when_object_is_null()
        {
            Object obj = null;

            var ex = Assert.Throws<ArgumentException>(() => {
                obj.ThrowExceptionIf(p => p == null, new ArgumentException(nameof(obj)));
            });

            Assert.Equal("obj", ex.Message);
        }

        [Fact]
        public void Should_throw_argumentnullexception_when_object_is_null_while_calling_ThrowExceptionIfNull()
        {
            Object obj2 = null;

            var ex = Assert.Throws<ArgumentNullException>(() => {
                obj2.ThrowExceptionIfNull<ArgumentNullException>();
            });

            Assert.Equal($"Value cannot be null.", ex.Message);
        }

        [Fact]
        public void Should_throw_given_exception_with_given_message_when_object_is_null()
        {
            Object obj = null;

            var ex = Assert.Throws<StackOverflowException>(() => {
                obj.ThrowExceptionIfNull<StackOverflowException>("this is silly but works!");
            });

            Assert.Equal("this is silly but works!", ex.Message);
        }

        [Fact]
        public void Should_throw_exception_when_object_is_string_and_null()
        {
            String obj = null;

            var ex = Assert.Throws<StackOverflowException>(() => {
                obj.ThrowExceptionIfNull<StackOverflowException>("string is null");
            });

            Assert.Equal("string is null", ex.Message);
        }
        [Fact]
        public void Should_throw_exception_when_object_is_string_and_null_or_empty()
        {
            String obj = "";

            var ex = Assert.Throws<StackOverflowException>(() => {
                obj.ThrowExceptionIfNull<StackOverflowException>("string is null");
            });

            Assert.Equal("string is null", ex.Message);
        }
    }
}
