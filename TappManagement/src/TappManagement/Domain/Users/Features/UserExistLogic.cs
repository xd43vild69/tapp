namespace TappManagement.Domain.Users.Features;

using TappManagement.Domain.Users.Services;
using TappManagement.Domain.Users;
using TappManagement.Domain.Users.Dtos;
using TappManagement.Domain.Users.Models;
using TappManagement.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class UserExistLogic
{
    public sealed class Command : IRequest<bool>
    {
        public readonly UserExistDTO UserExistDTO;

        public Command(UserExistDTO userExistDTO)
        {
            UserExistDTO = userExistDTO;
        }
    }

    public sealed class Handler : IRequestHandler<Command, bool>
    {
        private readonly IUserRepository _addLogicRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Handler(IUserRepository addLogicRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _addLogicRepository = addLogicRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
        {
            var userExistDTO = _mapper.Map<UserExistDTO>(request.UserExistDTO);
            
            var userExist = await _addLogicRepository.UserExists(userExistDTO.Name, cancellationToken);
            await _unitOfWork.CommitChanges(cancellationToken);
                        
            return userExist;
        }
    }
}