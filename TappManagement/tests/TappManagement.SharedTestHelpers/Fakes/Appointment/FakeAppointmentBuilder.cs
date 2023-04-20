namespace TappManagement.SharedTestHelpers.Fakes.Appointment;

using TappManagement.Domain.Appointments;
using TappManagement.Domain.Appointments.Models;

public class FakeAppointmentBuilder
{
    private AppointmentForCreation _creationData = new FakeAppointmentForCreation().Generate();

    public FakeAppointmentBuilder WithModel(AppointmentForCreation model)
    {
        _creationData = model;
        return this;
    }
    
    public FakeAppointmentBuilder WithSize(int size)
    {
        _creationData.Size = size;
        return this;
    }
    
    public FakeAppointmentBuilder WithLocation(string location)
    {
        _creationData.Location = location;
        return this;
    }
    
    public FakeAppointmentBuilder WithDescription(string description)
    {
        _creationData.Description = description;
        return this;
    }
    
    public FakeAppointmentBuilder WithReference(string reference)
    {
        _creationData.Reference = reference;
        return this;
    }
    
    public FakeAppointmentBuilder WithPayment(string payment)
    {
        _creationData.Payment = payment;
        return this;
    }
    
    public FakeAppointmentBuilder WithApprove(bool approve)
    {
        _creationData.Approve = approve;
        return this;
    }
    
    public FakeAppointmentBuilder WithUserId(Guid userId)
    {
        _creationData.UserId = userId;
        return this;
    }
    
    public Appointment Build()
    {
        var result = Appointment.Create(_creationData);
        return result;
    }
}