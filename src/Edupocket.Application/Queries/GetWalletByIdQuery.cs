using Edupocket.Application.DTO;
using MediatR;

namespace Edupocket.Application.Queries
{
    public record GetWalletByIdQuery(Guid Id): IRequest<GetWalletQueryResponse>
    {
    }
}
