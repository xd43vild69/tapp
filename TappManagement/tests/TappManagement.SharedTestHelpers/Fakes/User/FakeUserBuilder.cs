namespace TappManagement.SharedTestHelpers.Fakes.User;

using TappManagement.Domain.Users;
using TappManagement.Domain.Users.Models;

public class FakeUserBuilder
{
    private UserForCreation _creationData = new FakeUserForCreation().Generate();

    public FakeUserBuilder WithModel(UserForCreation model)
    {
        _creationData = model;
        return this;
    }
    
    public FakeUserBuilder WithName(string name)
    {
        _creationData.Name = name;
        return this;
    }
    
    public FakeUserBuilder WithEmail(string email)
    {
        _creationData.Email = email;
        return this;
    }
    
    public FakeUserBuilder WithCellphone(string cellphone)
    {
        _creationData.Cellphone = cellphone;
        return this;
    }
    
    public FakeUserBuilder WithIGUser(string iGUser)
    {
        _creationData.IGUser = iGUser;
        return this;
    }
    
    public FakeUserBuilder WithAppointmentId(Guid? appointmentId)
    {
        _creationData.AppointmentId = appointmentId;
        return this;
    }
    
    public User Build()
    {
        var result = User.Create(_creationData);
        return result;
    }
}