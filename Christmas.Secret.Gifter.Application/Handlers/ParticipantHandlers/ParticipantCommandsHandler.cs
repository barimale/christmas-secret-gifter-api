using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Christmas.Secret.Gifter.Domain;
using Christmas.Secret.Gifter.Application.Services.Abstractions;
using Christmas.Secret.Gifter.Application.Commands.ParticipantCommands;

namespace Christmas.Secret.Gifter.Application.Handlers.ParticipantHandlers
{
    internal sealed class ParticipantCommandsHandler :
        IRequestHandler<AddParticipantCommand, Participant>,
        IRequestHandler<UpdateParticipantCommand, Participant>,
        IRequestHandler<DeleteParticipantCommand, int>,
        IRequestHandler<CheckIfNameAlreadyExistCommand, bool>,
        IRequestHandler<CheckIfEmailAlreadyExistCommand, bool>,
        IRequestHandler<CheckIfNameAlreadyExistEditModeCommand, bool>,
        IRequestHandler<CheckIfEmailAlreadyExistEditModeCommand, bool>
    {
        private readonly IParticipantService _participantService;

        public ParticipantCommandsHandler(IParticipantService participantService)
            => _participantService = participantService;

        public Task<Participant> Handle(AddParticipantCommand request, CancellationToken cancellationToken)
        {
            return _participantService.AddAsync(request.Participant, cancellationToken);
        }

        public Task<Participant> Handle(UpdateParticipantCommand request, CancellationToken cancellationToken)
        {
            return _participantService.UpdateAsync(request.Participant, cancellationToken);
        }

        public Task<int> Handle(DeleteParticipantCommand request, CancellationToken cancellationToken)
        {
            return _participantService.DeleteAsync(request.ParticipantId, cancellationToken);
        }

        public Task<bool> Handle(CheckIfNameAlreadyExistCommand request, CancellationToken cancellationToken)
        {
            return _participantService.CheckIfNameAlreadyExist(request.EventId, request.Name, cancellationToken);
        }

        public Task<bool> Handle(CheckIfEmailAlreadyExistCommand request, CancellationToken cancellationToken)
        {
            return _participantService.CheckIfEmailAlreadyExist(request.EventId, request.Email, cancellationToken);
        }

        public Task<bool> Handle(CheckIfNameAlreadyExistEditModeCommand request, CancellationToken cancellationToken)
        {
            return _participantService.CheckIfNameAlreadyExistEditMode(request.EventId, request.ParticipantId, request.Name, cancellationToken);
        }

        public Task<bool> Handle(CheckIfEmailAlreadyExistEditModeCommand request, CancellationToken cancellationToken)
        {
            return _participantService.CheckIfEmailAlreadyExistEditMode(request.EventId, request.ParticipantId, request.Email, cancellationToken);
        }
    }
}
