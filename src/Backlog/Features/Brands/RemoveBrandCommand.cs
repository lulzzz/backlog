using MediatR;
using Backlog.Data;
using Backlog.Model;
using Backlog.Features.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace Backlog.Features.Brands
{
    public class RemoveBrandCommand
    {
        public class RemoveBrandRequest : IRequest<RemoveBrandResponse>
        {
            public int Id { get; set; }
        }

        public class RemoveBrandResponse { }

        public class RemoveBrandHandler : IAsyncRequestHandler<RemoveBrandRequest, RemoveBrandResponse>
        {
            public RemoveBrandHandler(BacklogContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<RemoveBrandResponse> Handle(RemoveBrandRequest request)
            {
                var brand = await _context.Brands.FindAsync(request.Id);
                brand.IsDeleted = true;
                await _context.SaveChangesAsync();
                return new RemoveBrandResponse();
            }

            private readonly BacklogContext _context;
            private readonly ICache _cache;
        }
    }
}
