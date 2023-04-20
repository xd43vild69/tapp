namespace TappManagement.Domain.Users.Features;

using TappManagement.Domain.Users;
using TappManagement.Domain.Users.Dtos;
using TappManagement.Domain.Users.Services;
using TappManagement.Services;
using TappManagement.Domain.Users.Models;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class UpdateUser
{
    public sealed class Command : IRequest<bool>
    {
        public readonly Guid Id;
        public readonly UserForUpdateDto UpdatedUserData;

        public Command(Guid id, UserForUpdateDto updatedUserData)
        {
            Id = id;
            UpdatedUserData = updatedUserData;
        }
    }

    public sealed class Handler : IRequestHandler<Command, bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Handler(IUserRepository userRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
        {
            var userToUpdate = await _userRepository.GetById(request.Id, cancellationToken: cancellationToken);

            var userToAdd = _mapper.Map<UserForUpdate>(request.UpdatedUserData);
            userToUpdate.Update(userToAdd);

            _userRepository.Update(userToUpdate);
            return await _unitOfWork.CommitChanges(cancellationToken) >= 1;
        }
    }
}