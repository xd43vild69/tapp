namespace TappManagement.Domain.Appointments;

using SharedKernel.Exceptions;
using TappManagement.Domain.Appointments.Models;
using TappManagement.Domain.Appointments.DomainEvents;
using FluentValidation;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Sieve.Attributes;
using TappManagement.Domain.Users;


public class Appointment : BaseEntity
{
    [Sieve(CanFilter = true, CanSort = false)]
    public int Size { get; private set; }

    [Sieve(CanFilter = true, CanSort = false)]
    public string Location { get; private set; }

    [Sieve(CanFilter = true, CanSort = false)]
    public string Description { get; private set; }

    public string Reference { get; private set; }

    public string Payment { get; private set; }

    public bool Approve { get; private set; }

    [JsonIgnore, IgnoreDataMember]
    [ForeignKey("User")]
    public Guid UserId { get; private set; }
    public User User { get; private set; }


    public static Appointment Create(AppointmentForCreation appointmentForCreation)
    {
        var newAppointment = new Appointment();

        newAppointment.Size = appointmentForCreation.Size;
        newAppointment.Location = appointmentForCreation.Location;
        newAppointment.Description = appointmentForCreation.Description;
        newAppointment.Reference = appointmentForCreation.Reference;
        newAppointment.Payment = appointmentForCreation.Payment;
        newAppointment.Approve = appointmentForCreation.Approve;
        newAppointment.UserId = appointmentForCreation.UserId;

        newAppointment.QueueDomainEvent(new AppointmentCreated(){ Appointment = newAppointment });
        
        return newAppointment;
    }

    public Appointment Update(AppointmentForUpdate appointmentForUpdate)
    {
        Size = appointmentForUpdate.Size;
        Location = appointmentForUpdate.Location;
        Description = appointmentForUpdate.Description;
        Reference = appointmentForUpdate.Reference;
        Payment = appointmentForUpdate.Payment;
        Approve = appointmentForUpdate.Approve;
        UserId = appointmentForUpdate.UserId;

        QueueDomainEvent(new AppointmentUpdated(){ Id = Id });
        return this;
    }
    
    protected Appointment() { } // For EF + Mocking
}