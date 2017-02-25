using MediatR;
using Backlog.Data;
using Backlog.Data.Models;
using Backlog.Features.Core;
using System.Collections.Generic;
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

        public class AddOrUpdateEpicResponse
        {

        }

        public class AddOrUpdateEpicHandler : IAsyncRequestHandler<AddOrUpdateEpicRequest, AddOrUpdateEpicResponse>
        {
            public AddOrUpdateEpicHandler(IDataContext dataContext, ICache cache)
            {
                _dataContext = dataContext;
                _cache = cache;
            }

            public async Task<AddOrUpdateEpicResponse> Handle(AddOrUpdateEpicRequest request)
            {
                var entity = await _dataContext.Epics
                    .SingleOrDefaultAsync(x => x.Id == request.Epic.Id && x.IsDeleted == false);
                if (entity == null) _dataContext.Epics.Add(entity = new Epic());
                entity.Name = request.Epic.Name;
                await _dataContext.SaveChangesAsync();

                return new AddOrUpdateEpicResponse()
                {

                };
            }

            private readonly IDataContext _dataContext;
            private readonly ICache _cache;
        }

    }

}