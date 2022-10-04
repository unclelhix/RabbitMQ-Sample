using HashGenerator.Core.DatabaseContext;
using HashGenerator.Shared.DTOs;
using HashGenerator.Shared.ResponseWrapper;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HashGenerator.Core.Queries
{
    public record GetHashesQuery : IRequest<ServiceResponse<List<HahesResponseDTO>>>;

    public class GetHashesQueryHandler : IRequestHandler<GetHashesQuery, ServiceResponse<List<HahesResponseDTO>>>
    {
        private readonly IHashGeneratorDbContext _context;
     
        public GetHashesQueryHandler(IHashGeneratorDbContext context) => _context = context;

        public async Task<ServiceResponse<List<HahesResponseDTO>>> Handle(GetHashesQuery request, CancellationToken cancellationToken)
        {

            var query = await _context.Hashes
                    .GroupBy(o => new { Date = o.CreatedOn.Date })
                    .Select(s => new 
                    {
                        Date = s.Key.Date,
                        Count = s.Count(),
                    })
                    .OrderBy(o => o.Date)                   
                    .ToListAsync();

            return new ServiceResponse<List<HahesResponseDTO>>()
            {
                Hashes = query.Select(x=> new HahesResponseDTO { 
                     Date = x.Date.ToString("yyyy-MM-dd"),
                     Count = x.Count
                }).ToList()
            };
        }
    }
}
