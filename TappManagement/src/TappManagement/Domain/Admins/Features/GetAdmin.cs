namespace TappManagement.Domain.Admins.Features;

using TappManagement.Domain.Admins.Dtos;
using TappManagement.Domain.Admins.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class GetAdmin
{
    public sealed class Query : IRequest<AdminDto>
    {
        public readonly Guid Id;

        public Query(Guid id)
        {
            Id = id;
        }
    }

    public sealed class Handler : IRequestHandler<Query, AdminDto>
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IMapper _mapper;

        public Handler(IAdminRepository adminRepository, IMapper mapper)
        {
            _mapper = mapper;
            _adminRepository = adminRepository;
        }

        public async Task<AdminDto> Handle(Query request, CancellationToken cancellationToken)
        {
            var result = await _adminRepository.GetById(request.Id, cancellationToken: cancellationToken);
            return _mapper.Map<AdminDto>(result);
        }
    }
}