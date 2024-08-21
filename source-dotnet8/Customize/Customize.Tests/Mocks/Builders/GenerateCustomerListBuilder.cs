using Customize.Domain.Entities;
using Customize.Tests.Extensions;

namespace Customize.Tests.Mocks.Builders
{
    public class GenerateCustomerListBuilder
    {
        public readonly List<Customer> _customers = [];

        public GenerateCustomerListBuilder AddSimpleCustomer(string name) 
        {
            _customers.Add(new Customer(Guid.NewGuid().ToString(), name, cellphone: new Random().GenerateStringNumbers(13), "teste@test.com", DateTime.Now));
            return this;
        }

        public List<Customer> Build() => _customers;
    }
}