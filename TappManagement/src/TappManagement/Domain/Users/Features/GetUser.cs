namespace TappManagement.Domain.Users.Features;

using TappManagement.Domain.Users.Dtos;
using TappManagement.Domain.Users.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class GetUser
{
    public sealed class Query : IRequest<UserDto>
    {
        public readonly Guid Id;

        public Query(Guid id)
        {
            Id = id;
        }
    }

    public sealed class Handler : IRequestHandler<Query, UserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public Handler(IUserRepository userRepository, IMapper mapper)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<UserDto> Handle(Query request, CancellationToken cancellationToken)
        {
            var result = await _userRepository.GetById(request.Id, cancellationToken: cancellationToken);
            return _mapper.Map<UserDto>(result);
        }
    }
}