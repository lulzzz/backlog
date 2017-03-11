using MediatR;
using Backlog.Data;
using Backlog.Features.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace Backlog.Features.Categories
{
    public class GetCategoryByIdQuery
    {
        public class GetCategoryByIdRequest : IRequest<GetCategoryByIdResponse> { 
			public int Id { get; set; }
		}

        public class GetCategoryByIdResponse
        {
            public CategoryApiModel Category { get; set; } 
		}

        public class GetCategoryByIdHandler : IAsyncRequestHandler<GetCategoryByIdRequest, GetCategoryByIdResponse>
        {
            public GetCategoryByIdHandler(IBacklogContext dataContext, ICache cache)
            {
                _dataContext = dataContext;
                _cache = cache;
            }

            public async Task<GetCategoryByIdResponse> Handle(GetCategoryByIdRequest request)
            {                
                return new GetCategoryByIdResponse()
                {
                    Category = CategoryApiModel.FromCategory(await _dataContext.Categories.FindAsync(request.Id))
                };
            }

            private readonly IBacklogContext _dataContext;
            private readonly ICache _cache;
        }

    }

}