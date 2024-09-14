using Edupocket.Application.DTO;
using Edupocket.Application.Queries;
using Edupocket.Domain.AggregatesModel.WalletAggregate;
using Edupocket.Domain.SeedWork;
using Edupocket.Infrastructure.Contracts;
using MediatR;

namespace Edupocket.Application.Handlers
{
    public class GetWalletDetailsQueryHandler : IRequestHandler<GetWalletByIdQuery, GetWalletQueryResponse>
    {
        private readonly IAsyncRepository<Wallet> _walletRepository;
        private readonly IAsyncRepository<Profile> _profileRepository;

        public GetWalletDetailsQueryHandler(IAsyncRepository<Wallet> walletRepository,
            IAsyncRepository<Profile> profileRepository)
        {
            _walletRepository = walletRepository;
            _profileRepository = profileRepository;
        }

        public async Task<GetWalletQueryResponse> Handle(GetWalletByIdQuery request, CancellationToken cancellationToken)
        {
            var response = new GetWalletQueryResponse();

            try
            {
                var wallet = await _walletRepository.GetSingleAsync(x => x.Id == request.Id);
                if (wallet == null)
                    return new GetWalletQueryResponse { Message = Constants.WalletNotFound };

                var profile = await _profileRepository.GetSingleAsync(x => x.Id == wallet.ProfileId);
                if (profile == null)
                    return new GetWalletQueryResponse { Message = Constants.ProfileNotFound };

                response = new GetWalletQueryResponse
                {
                    Result = new GetWalletDetails
                    {
                        Balance = wallet.Balance,
                        Status = wallet.Status,
                        EmailAddress = profile.EmailAddress,
                        FirstName = profile.FirstName,
                        LastName = profile.LastName,
                        Gender = profile.Gender,
                        Id = wallet.Id,
                        MobileNumber = profile.MobileNumber,
                        OtherName = profile.OtherName,
                        ProfileId = wallet.ProfileId,
                        WalletNumber = wallet.WalletNumber,
                    },
                    IsSuccessful = true,
                    Message = Constants.SuccessResponse                    
                };                    
            }            
            catch (Exception ex)
            {
                response.Message = Constants.ErrorResponse;
            }

            return response;
        }


    }
}
