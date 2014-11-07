using FluentAssertions;

using Xunit;

namespace Fluenter.Tests
{
    public class FluenterSpecs
    {
        [Fact]
        public void It_should_map_constructor()
        {
            dynamic subject = Fluenter<TestSubject>.Get("some name", "some description");

            TestSubject result = subject;

            result.Name.Should().Be("some name");
            result.Description.Should().Be("some description");
        }

        [Fact]
        public void It_should_allow_to_call_method()
        {
            dynamic subject = Fluenter<TestSubject>.Get().Describe("some description");

            TestSubject result = subject;

            result.Name.Should().BeNull();
            result.Description.Should().Be("some description");
        }

        [Fact]
        public void It_should_allow_to_chain_method_calls()
        {
            dynamic subject = Fluenter<TestSubject>.Get().Rename("some name").Describe("some description");

            TestSubject result = subject;

            result.Name.Should().Be("some name");
            result.Description.Should().Be("some description");
        }

        [Fact]
        public void It_should_work_in_one_line_use_case()
        {
            var result = (TestSubject)Fluenter<TestSubject>.Get().Rename("some name").Describe("some description");

            result.Name.Should().Be("some name");
            result.Description.Should().Be("some description");
        }

        public class TestSubject
        {
            public TestSubject() {}

            public TestSubject(string name, string description)
            {
                Name = name;
                Description = description;
            }

            public string Name { get; private set; }
            public string Description { get; private set; }

            public void Rename(string name)
            {
                Name = name;
            }

            public void Describe(string description)
            {
                Description = description;
            }
        }
    }
}