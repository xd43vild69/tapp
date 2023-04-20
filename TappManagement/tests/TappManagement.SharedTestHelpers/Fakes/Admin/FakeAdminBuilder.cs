namespace TappManagement.SharedTestHelpers.Fakes.Admin;

using TappManagement.Domain.Admins;
using TappManagement.Domain.Admins.Models;

public class FakeAdminBuilder
{
    private AdminForCreation _creationData = new FakeAdminForCreation().Generate();

    public FakeAdminBuilder WithModel(AdminForCreation model)
    {
        _creationData = model;
        return this;
    }
    
    public FakeAdminBuilder WithName(string name)
    {
        _creationData.Name = name;
        return this;
    }
    
    public FakeAdminBuilder WithPassword(string password)
    {
        _creationData.Password = password;
        return this;
    }
    
    public FakeAdminBuilder WithIsSync(bool isSync)
    {
        _creationData.IsSync = isSync;
        return this;
    }
    
    public FakeAdminBuilder WithUserId(Guid userId)
    {
        _creationData.UserId = userId;
        return this;
    }
    
    public Admin Build()
    {
        var result = Admin.Create(_creationData);
        return result;
    }
}