namespace TappManagement.Domain.Admins.Features;

using TappManagement.Domain.Admins.Services;
using TappManagement.Domain.Admins;
using TappManagement.Domain.Admins.Dtos;
using TappManagement.Domain.Admins.Models;
using TappManagement.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class AddAdmin
{
    public sealed class Command : IRequest<AdminDto>
    {
        public readonly AdminForCreationDto AdminToAdd;

        public Command(AdminForCreationDto adminToAdd)
        {
            AdminToAdd = adminToAdd;
        }
    }

    public sealed class Handler : IRequestHandler<Command, AdminDto>
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Handler(IAdminRepository adminRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _adminRepository = adminRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<AdminDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var adminToAdd = _mapper.Map<AdminForCreation>(request.AdminToAdd);
            var admin = Admin.Create(adminToAdd);

            await _adminRepository.Add(admin, cancellationToken);
            await _unitOfWork.CommitChanges(cancellationToken);

            return _mapper.Map<AdminDto>(admin);
        }
    }
}