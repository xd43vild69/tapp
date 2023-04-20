namespace TappManagement.Domain.Admins.Features;

using TappManagement.Domain.Admins;
using TappManagement.Domain.Admins.Dtos;
using TappManagement.Domain.Admins.Services;
using TappManagement.Services;
using TappManagement.Domain.Admins.Models;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class UpdateAdmin
{
    public sealed class Command : IRequest<bool>
    {
        public readonly Guid Id;
        public readonly AdminForUpdateDto UpdatedAdminData;

        public Command(Guid id, AdminForUpdateDto updatedAdminData)
        {
            Id = id;
            UpdatedAdminData = updatedAdminData;
        }
    }

    public sealed class Handler : IRequestHandler<Command, bool>
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Handler(IAdminRepository adminRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _adminRepository = adminRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
        {
            var adminToUpdate = await _adminRepository.GetById(request.Id, cancellationToken: cancellationToken);

            var adminToAdd = _mapper.Map<AdminForUpdate>(request.UpdatedAdminData);
            adminToUpdate.Update(adminToAdd);

            _adminRepository.Update(adminToUpdate);
            return await _unitOfWork.CommitChanges(cancellationToken) >= 1;
        }
    }
}