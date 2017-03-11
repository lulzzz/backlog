using MediatR;
using Backlog.Data;
using Backlog.Data.Model;
using Backlog.Features.Core;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace Backlog.Features.Epics
{
    public class AddOrUpdateEpicCommand
    {
        public class AddOrUpdateEpicRequest : IRequest<AddOrUpdateEpicResponse>
        {
            public EpicApiModel Epic { get; set; }
        }

        public class AddOrUpdateEpicResponse { }

        public class AddOrUpdateEpicHandler : IAsyncRequestHandler<AddOrUpdateEpicRequest, AddOrUpdateEpicResponse>
        {
            public AddOrUpdateEpicHandler(IBacklogContext dataContext, ICache cache)
            {
                _dataContext = dataContext;
                _cache = cache;
            }

            public async Task<AddOrUpdateEpicResponse> Handle(AddOrUpdateEpicRequest request)
            {
                var entity = await _dataContext.Epics
                    .SingleOrDefaultAsync(x => x.Id == request.Epic.Id);
                if (entity == null) _dataContext.Epics.Add(entity = new Epic());
                entity.Name = request.Epic.Name;
                entity.Description = request.Epic.Description;
                entity.IsTemplate = request.Epic.IsTemplate;
                entity.ProductId = request.Epic.ProductId;
                entity.Product = await _dataContext.Products.Where(x=>x.Id == request.Epic.ProductId.Value)
                    .SingleOrDefaultAsync();
                entity.Slug = request.Epic.Name.GenerateSlug();

                if (request.Epic.Priority.HasValue)
                    entity.Priority = request.Epic.Priority.Value;
                
                await _dataContext.SaveChangesAsync();

                return new AddOrUpdateEpicResponse()
                {

                };
            }

            private readonly IBacklogContext _dataContext;
            private readonly ICache _cache;
        }
    }
}
