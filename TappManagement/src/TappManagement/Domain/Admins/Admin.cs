namespace TappManagement.Domain.Admins;

using SharedKernel.Exceptions;
using TappManagement.Domain.Admins.Models;
using TappManagement.Domain.Admins.DomainEvents;
using FluentValidation;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using TappManagement.Domain.Users;


public class Admin : BaseEntity
{
    public string Name { get; private set; }

    public string Password { get; private set; }

    public bool IsSync { get; private set; }

    [JsonIgnore, IgnoreDataMember]
    [ForeignKey("User")]
    public Guid UserId { get; private set; }
    public User User { get; private set; }


    public static Admin Create(AdminForCreation adminForCreation)
    {
        var newAdmin = new Admin();

        newAdmin.Name = adminForCreation.Name;
        newAdmin.Password = adminForCreation.Password;
        newAdmin.IsSync = adminForCreation.IsSync;
        newAdmin.UserId = adminForCreation.UserId;

        newAdmin.QueueDomainEvent(new AdminCreated(){ Admin = newAdmin });
        
        return newAdmin;
    }

    public Admin Update(AdminForUpdate adminForUpdate)
    {
        Name = adminForUpdate.Name;
        Password = adminForUpdate.Password;
        IsSync = adminForUpdate.IsSync;
        UserId = adminForUpdate.UserId;

        QueueDomainEvent(new AdminUpdated(){ Id = Id });
        return this;
    }
    
    protected Admin() { } // For EF + Mocking
}