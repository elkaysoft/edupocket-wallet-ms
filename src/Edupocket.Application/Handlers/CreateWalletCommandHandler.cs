using Edupocket.Application.Commands;
using Edupocket.Application.DTO;
using Edupocket.Application.Exceptions;
using Edupocket.Application.Validations;
using Edupocket.DAL.Contracts;
using Edupocket.Domain.AggregatesModel.WalletAggregate;
using Edupocket.Domain.SeedWork;
using Edupocket.Infrastructure.Contracts;
using MediatR;

namespace Edupocket.Application.Handlers
{
    public class CreateWalletCommandHandler : IRequestHandler<CreateWalletCommand, CreateWalletResponse>
    {
        private readonly IAsyncRepository<Wallet> _walletRepository;
        private IUnitOfWork _unitOfWork;

        public CreateWalletCommandHandler(IAsyncRepository<Wallet> walletRepository, 
            IUnitOfWork unitOfWork)
        {
            _walletRepository = walletRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CreateWalletResponse> Handle(CreateWalletCommand request, CancellationToken cancellationToken)
        {
            var response = new CreateWalletResponse();

            try
            {
                var validator = new CreateWalletValidator();
                var validationResult = await validator.ValidateAsync(request, cancellationToken);
                if (validationResult.Errors.Any()) throw new ValidationException(validationResult);

                var profile = Profile.Create(new ProfileCreationDTO(emailAddress: request.EmailAddress, firstName: request.FirstName,
                                            gender: request.Gender, lastName: request.LastName,
                                            otherName: request.OtherName, phoneNumber: request.MobileNumber,
                                            userType: request.ProfileType));

                //generate Wallet Number
                string walletNumber = Cryptography.CharGenerator.GenerateRandomNumber(10);
                //check duplicate wallet number
                walletNumber = await DeDupCheck(walletNumber);

                var wallet = profile.CreateWallet(profile.Id, walletNumber);
                

                _unitOfWork.BeginTransaction();

                _unitOfWork.ProfileRepository.Add(profile);
                _unitOfWork.WalletRepository.Add(wallet);

                _unitOfWork.CommitTransaction();

                response.Result = new CreateWalletDetails
                {
                    FirstName = request.FirstName,
                    LastName = profile.LastName,
                    MobileNumber = profile.MobileNumber,
                    OtherName = profile.OtherName,
                    WalletNumber = walletNumber,
                    EmailAddress = profile.EmailAddress
                };

                response.Message = "Completed successfully";
                response.IsSuccessful = true;
            }
            catch(ValidationException ex)
            {
                response.Message = ex.Message;
            }
            catch(Exception ex)
            {
                response.Message = Constants.ErrorResponse;
            }

            return response;
        }

        private async Task<string> DeDupCheck(string walletNumber)
        {
            while (await _walletRepository.GetSingleAsync(x => x.WalletNumber == walletNumber) != null)
            {
                walletNumber = Cryptography.CharGenerator.GenerateRandomNumber(10);
            }

            return walletNumber;
        }


    }
}
