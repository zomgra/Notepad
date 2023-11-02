using AutoMapper;
using MediatR;
using Notepad.Application.Common.Authentication;
using Notepad.Domain.Entities;
using Notepad.Domain.ViewModel;
using Notepad.Storage;

namespace Notepad.Application.Chunks.Command.Create.Create
{
    public class CreateChunkCommandHandler : IRequestHandler<CreateChunkCommand, ChunkViewModel>
    {
        private readonly NoteDbContext _context;
        private readonly IMapper _mapper;
        private readonly IIdentityProvider _provider;

        public CreateChunkCommandHandler(NoteDbContext context,
            IMapper mapper, 
            IIdentityProvider provider)
        {
            _context = context;
            _mapper = mapper;
            _provider = provider;
        }

        public async Task<ChunkViewModel> Handle(CreateChunkCommand request, CancellationToken cancellationToken)
        {
            var chunk = new NoteChunk(request.Name);

            chunk.OwnerId = _provider.UserId;
            var transient = await _context.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                await _context.Chunks.AddAsync(chunk, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                await transient.CommitAsync(cancellationToken);
            }
            catch(Exception e)
            {
                await transient.RollbackAsync(cancellationToken);
            }

            return _mapper.Map<ChunkViewModel>(chunk);
        }
    }
}
