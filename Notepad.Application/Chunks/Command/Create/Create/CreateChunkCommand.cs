﻿using MediatR;
using Notepad.Domain.ViewModel;

namespace Notepad.Application.Chunks.Command.Create.Create
{
    public record CreateChunkCommand(string Name) : IRequest<ChunkViewModel>;
}
