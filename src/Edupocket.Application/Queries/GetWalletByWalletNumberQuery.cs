using Edupocket.Application.DTO;
using MediatR;

namespace Edupocket.Application.Queries
{
    public record GetWalletByWalletNumberQuery(string walletNumber): IRequest<GetWalletQueryResponse>
    {
    }
}
