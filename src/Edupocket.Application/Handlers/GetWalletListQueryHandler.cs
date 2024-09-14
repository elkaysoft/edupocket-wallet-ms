// ***********************************************************************
// Assembly         : Edupocket.Application
// Author           : Sheriff Olamilekan
// Created          : 14-08-2024
//
// Last Modified By : Sheriff Kareem
// Last Modified On : 14-08-2024
// ***********************************************************************
// <copyright file="WalletSchemeListQueryHandler.cs" company="Etranzact">
//     Copyright (c) Etranzact. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Edupocket.Application.DTO;
using Edupocket.Application.Queries;
using Edupocket.Domain.AggregatesModel.WalletAggregate;
using Edupocket.Domain.SeedWork;
using Edupocket.Infrastructure.Contracts;
using MediatR;

namespace Edupocket.Application.Handlers
{
    public class GetWalletListQueryHandler : IRequestHandler<GetWalletListQuery, WalletListResponse>
    {
        private readonly IAsyncRepository<Wallet> _walletRepository;
        private readonly IAsyncRepository<Profile> _profileRepository;

        public GetWalletListQueryHandler(IAsyncRepository<Wallet> walletRepository,
            IAsyncRepository<Profile> profileRepository)
        {
            _walletRepository = walletRepository;
            _profileRepository = profileRepository;
        }



        public async Task<WalletListResponse> Handle(GetWalletListQuery request, CancellationToken cancellationToken)
        {
            var response = new WalletListResponse();
            try
            {
                var filter = PredicateBuilder.True<Profile>();

                if (!string.IsNullOrWhiteSpace(request.PhoneNumber))
                {
                    filter = filter.And(p => p.MobileNumber.Contains(request.PhoneNumber));  
                }
                if (!string.IsNullOrWhiteSpace(request.EmailAddress))
                {
                    filter = filter.And(p => p.EmailAddress.Contains(request.EmailAddress));
                }
                if (request.UserType.HasValue)
                {
                    filter = filter.And(p => p.UserType == request.UserType);
                }
                if (!string.IsNullOrWhiteSpace(request.WalletNumber))
                {
                    var wallets = await _walletRepository.GetAllAsync(false, p => p.WalletNumber.Contains(request.WalletNumber));
                    if (wallets.Any())
                    {
                        var profileIds = string.Join(",", wallets.Select(x => x.ProfileId).ToList());
                        //use the retrieved profile Ids to query profile table
                        filter = filter.And(i => profileIds.Contains(i.Id.ToString()));
                    }
                }
                if (request.ProfileId.HasValue)
                {
                    filter = filter.And(p => p.Id == request.ProfileId.Value);
                }
                if (request.WalletId.HasValue)
                {
                    var wallet = await _walletRepository.GetSingleAsync(x => x.Id == request.WalletId.Value);
                    if (wallet != null)
                        filter = filter.And(i => i.Id == wallet.ProfileId);
                }

                var pagedResult = await _profileRepository.GetPagedFilteredAsync(filter, request.Page, request.PageSize,
                    request.SortColumn, request.SortOrder, false, p => p.Wallet);

                PagedListModel<WalletListQueryVm> result = new();

                if(pagedResult.Items.Count > 0)
                {
                    foreach (var item in pagedResult.Items)
                    {
                        WalletListQueryVm data = new WalletListQueryVm()
                        {
                            EmailAddress = item.EmailAddress,
                            Balance = item.Wallet.Balance,
                            IsPndActive = item.Wallet.IsPndActive,
                            FirstName = item.FirstName,
                            LastName = item.LastName,
                            Gender = item.Gender,
                            Status = item.Wallet.Status,
                            Id = item.Wallet.Id,
                            MobileNumber = item.MobileNumber,
                            OtherName = item.OtherName,
                            ProfileId = item.Id,
                            WalletNumber = item.Wallet.WalletNumber
                        };

                        result.Items.Add(data);
                    }
                    result.CurrentPage = pagedResult.CurrentPage;
                    result.TotalPages = pagedResult.TotalPages;
                }

                response.Result = result;
                response.Message = Constants.SuccessResponse;
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                ex.ToString();
                response.Message = Constants.ErrorResponse;
            }

            return response;
        }
    }
}
