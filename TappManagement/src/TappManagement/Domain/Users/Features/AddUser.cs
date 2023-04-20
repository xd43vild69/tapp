namespace TappManagement.Domain.Users.Features;

using TappManagement.Domain.Users.Services;
using TappManagement.Domain.Users;
using TappManagement.Domain.Users.Dtos;
using TappManagement.Domain.Users.Models;
using TappManagement.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class AddUser
{
    public sealed class Command : IRequest<UserDto>
    {
        public readonly UserForCreationDto UserToAdd;

        public Command(UserForCreationDto userToAdd)
        {
            UserToAdd = userToAdd;
        }
    }

    public sealed class Handler : IRequestHandler<Command, UserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Handler(IUserRepository userRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<UserDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var userToAdd = _mapper.Map<UserForCreation>(request.UserToAdd);
            var user = User.Create(userToAdd);

            await _userRepository.Add(user, cancellationToken);
            await _unitOfWork.CommitChanges(cancellationToken);

            return _mapper.Map<UserDto>(user);
        }
    }
}