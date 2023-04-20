namespace TappManagement.Domain.Users;

using SharedKernel.Exceptions;
using TappManagement.Domain.Users.Models;
using TappManagement.Domain.Users.DomainEvents;
using FluentValidation;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Sieve.Attributes;
using TappManagement.Domain.Appointments;


public class User : BaseEntity
{
    [Sieve(CanFilter = true, CanSort = true)]
    public string Name { get; private set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public string Email { get; private set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public string Cellphone { get; private set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public string IGUser { get; private set; }

    [JsonIgnore, IgnoreDataMember]
    [ForeignKey("ICollection<Appointment>")]
    public Guid? AppointmentId { get; private set; }
    public ICollection<Appointment> Appointments { get; private set; }


    public static User Create(UserForCreation userForCreation)
    {
        var newUser = new User();

        newUser.Name = userForCreation.Name;
        newUser.Email = userForCreation.Email;
        newUser.Cellphone = userForCreation.Cellphone;
        newUser.IGUser = userForCreation.IGUser;
        newUser.AppointmentId = userForCreation.AppointmentId;

        newUser.QueueDomainEvent(new UserCreated(){ User = newUser });
        
        return newUser;
    }

    public User Update(UserForUpdate userForUpdate)
    {
        Name = userForUpdate.Name;
        Email = userForUpdate.Email;
        Cellphone = userForUpdate.Cellphone;
        IGUser = userForUpdate.IGUser;
        AppointmentId = userForUpdate.AppointmentId;

        QueueDomainEvent(new UserUpdated(){ Id = Id });
        return this;
    }
    
    protected User() { } // For EF + Mocking
}