using HashGenerator.Core.DatabaseContext;
using HashGenerator.Models;
using HashGenerator.Shared.DTOs;
using HashGenerator.Shared.ResponseWrapper;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HashGenerator.Core.Commands
{
    public class AddHashCommand : IRequest<Unit>
    {
        public List<HashesDTO> HashesDTO { get; set; }
    }

    public class AddHashCommandHandler : IRequestHandler<AddHashCommand>
    {

        private readonly IHashGeneratorDbContext _context;
        private readonly IMapper _mapper;

        public AddHashCommandHandler(IMapper mapper, IHashGeneratorDbContext context)
        {        
            _mapper = mapper;
            _context = context;
        }

        public async Task<Unit> Handle(AddHashCommand request, CancellationToken cancellationToken)
        {
            var hashes = _mapper.Map<List<Hashes>>(request.HashesDTO);

            await _context.Hashes.AddRangeAsync(hashes);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;

        }  


    }
}
