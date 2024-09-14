using Edupocket.Application.DTO;
using Edupocket.Domain.AggregatesModel.WalletAggregate;
using MediatR;

namespace Edupocket.Application.Queries
{
    public class GetWalletListQuery: PagingModel, IRequest<WalletListResponse>
    {
        public string? WalletNumber { get; set; }
        public string? PhoneNumber { get; set; }
        public string? EmailAddress { get; set; }
        public Guid? ProfileId { get; set; }
        public Guid? WalletId { get; set; }
        public ProfileType? UserType { get; set; }
    }
}
