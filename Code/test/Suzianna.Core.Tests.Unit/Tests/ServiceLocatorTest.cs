using System;
using System.Net.Http;
using NFluent;
using Suzianna.Core.Screenplay;
using Xunit;

namespace Suzianna.Core.Tests.Unit.Utils
{
    public class ServiceLocatorTest :IDisposable
    {
        [Fact]
        public void when_request_type_didNot_declare_in_service_locator_should_return_null()
        {
            var service= ServiceLocator.GetService<SuperTypeImplementation>();

            Check.That(service).IsNull();
        }
        [Fact]
        public void when_request_type_does_did_declare_in_service_locator_should_return_object()
        {
            ServiceLocator.AddOrUpdate<SuperTypeImplementation>(new BaseTypeImplementation());

            var service = ServiceLocator.GetService<SuperTypeImplementation>();

            Check.That(service).IsNotNull();
        }
        [Fact]
        public void when_type_declared_and_new_type_added_should_return_new_type()
        {
            ServiceLocator.AddOrUpdate<SuperTypeImplementation>(new BaseTypeImplementation());

            ServiceLocator.AddOrUpdate<SuperTypeImplementation>(new NewBaseTypeImplementation());
            var service = ServiceLocator.GetService<SuperTypeImplementation>();

            Check.That(service).IsInstanceOfType(typeof(NewBaseTypeImplementation));

        }

        public void Dispose()
        {
            ServiceLocator.RemoveAll();
        }
    }

    public class SuperTypeImplementation
    {}
    public class BaseTypeImplementation : SuperTypeImplementation
    {}
    public class NewBaseTypeImplementation:SuperTypeImplementation
    {}
}
