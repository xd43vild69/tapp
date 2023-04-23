namespace TappManagement.Domain.Customers;

using SharedKernel.Exceptions;
using TappManagement.Domain.Customers.Models;
using TappManagement.Domain.Customers.DomainEvents;
using FluentValidation;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Sieve.Attributes;
using TappManagement.Domain.Appointments;


public class Customer : BaseEntity
{
    public string Name { get; private set; }

    public string Password { get; private set; }

    public bool IsSync { get; private set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public string Cellphone { get; private set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public string IGUser { get; private set; }

    [JsonIgnore, IgnoreDataMember]
    [ForeignKey("ICollection<Appointment>")]
    public Guid? AppointmentId { get; private set; }
    public ICollection<Appointment> Appointments { get; private set; }


    public static Customer Create(CustomerForCreation customerForCreation)
    {
        var newCustomer = new Customer();

        newCustomer.Name = customerForCreation.Name;
        newCustomer.Password = customerForCreation.Password;
        newCustomer.IsSync = customerForCreation.IsSync;
        newCustomer.Cellphone = customerForCreation.Cellphone;
        newCustomer.IGUser = customerForCreation.IGUser;
        newCustomer.AppointmentId = customerForCreation.AppointmentId;

        newCustomer.QueueDomainEvent(new CustomerCreated(){ Customer = newCustomer });
        
        return newCustomer;
    }

    public Customer Update(CustomerForUpdate customerForUpdate)
    {
        Name = customerForUpdate.Name;
        Password = customerForUpdate.Password;
        IsSync = customerForUpdate.IsSync;
        Cellphone = customerForUpdate.Cellphone;
        IGUser = customerForUpdate.IGUser;
        AppointmentId = customerForUpdate.AppointmentId;

        QueueDomainEvent(new CustomerUpdated(){ Id = Id });
        return this;
    }
    
    protected Customer() { } // For EF + Mocking
}