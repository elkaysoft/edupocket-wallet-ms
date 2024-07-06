using Edupocket.Domain.Contracts;
using Edupocket.Domain.SeedWork;

namespace Edupocket.Domain.AggregatesModel.WalletAggregate
{
    public class Profile: BaseEntity<Guid>, IAggregateRoot
    {
        public Profile(Guid id): base(id) { }

        private Profile(): base(Guid.NewGuid()) { }

        public static Profile Create(ProfileCreationDTO profile)
        {
            ArgumentNullException.ThrowIfNull(profile);

            Profile newProfile = new Profile()
            {
                EmailAddress = profile.emailAddress,
                MobileNumber = profile.phoneNumber,
                Gender = profile.gender,
                FirstName = profile.firstName,
                LastName = profile.lastName,
                OtherName = profile.otherName,
                ProfileType = profile.profileType,
                ProfileImage = profile.profileImage,
            };
            return newProfile;
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string OtherName { get; private set; }
        public string EmailAddress { get; private set; }
        public string MobileNumber { get; private set; }
        public string Gender { get; private set; }        
        public string? ProfileImage { get; private set; }
        public string? TransactionPinHash { get; private set; }
        public ProfileType ProfileType { get; private set; }
        public Beneficiary beneficiary { get; private set; }
        public Wallet Wallet { get; set; }

        public Wallet CreateWallet(Guid profileId, Guid walletSchemeId, string walletNumber)
        {
            var userWallet = Wallet.Create(profileId, walletSchemeId, walletNumber);
            userWallet.GetCheckSum();
            return userWallet;
        }

        public void UpdateWalletBalance(decimal balance)
        {
            if(balance == 0) return;

            Wallet.BalanceUpdate(balance);
        }
       

        public Beneficiary AddBeneficiary(string name, string walletNum, string nickName)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("Beneficiary name is required");
            if (string.IsNullOrEmpty(walletNum)) throw new ArgumentException("Beneficiary Wallet number is required");
            if (string.IsNullOrWhiteSpace(nickName)) throw new ArgumentException("Beneficiary nickName is required");

           return new Beneficiary(name, walletNum, nickName);   
        }

        


    }
}
