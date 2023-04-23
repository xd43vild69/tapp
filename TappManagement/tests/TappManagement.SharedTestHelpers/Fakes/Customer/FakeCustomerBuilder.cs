namespace TappManagement.SharedTestHelpers.Fakes.Customer;

using TappManagement.Domain.Customers;
using TappManagement.Domain.Customers.Models;

public class FakeCustomerBuilder
{
    private CustomerForCreation _creationData = new FakeCustomerForCreation().Generate();

    public FakeCustomerBuilder WithModel(CustomerForCreation model)
    {
        _creationData = model;
        return this;
    }
    
    public FakeCustomerBuilder WithName(string name)
    {
        _creationData.Name = name;
        return this;
    }
    
    public FakeCustomerBuilder WithPassword(string password)
    {
        _creationData.Password = password;
        return this;
    }
    
    public FakeCustomerBuilder WithIsSync(bool isSync)
    {
        _creationData.IsSync = isSync;
        return this;
    }
    
    public FakeCustomerBuilder WithCellphone(string cellphone)
    {
        _creationData.Cellphone = cellphone;
        return this;
    }
    
    public FakeCustomerBuilder WithIGUser(string iGUser)
    {
        _creationData.IGUser = iGUser;
        return this;
    }
    
    public FakeCustomerBuilder WithAppointmentId(Guid? appointmentId)
    {
        _creationData.AppointmentId = appointmentId;
        return this;
    }
    
    public Customer Build()
    {
        var result = Customer.Create(_creationData);
        return result;
    }
}