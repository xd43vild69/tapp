namespace TappManagement.Domain.Users.Features;

using TappManagement.Domain.Users.Services;
using TappManagement.Services;
using SharedKernel.Exceptions;
using MediatR;

public static class DeleteUser
{
    public sealed class Command : IRequest<bool>
    {
        public readonly Guid Id;

        public Command(Guid id)
        {
            Id = id;
        }
    }

    public sealed class Handler : IRequestHandler<Command, bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
        {
            var recordToDelete = await _userRepository.GetById(request.Id, cancellationToken: cancellationToken);
            _userRepository.Remove(recordToDelete);
            return await _unitOfWork.CommitChanges(cancellationToken) >= 1;
        }
    }
}