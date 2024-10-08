﻿using Edupocket.Domain.Contracts;
using Edupocket.Domain.SeedWork;

namespace Edupocket.Domain.AggregatesModel.WalletAggregate
{
    public class Profile: BaseEntity<Guid>, IAggregateRoot
    {
        public Profile(Guid id): base(id) { }

        private Profile(): base(Guid.NewGuid()) { }        

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string OtherName { get; private set; }
        public string EmailAddress { get; private set; }
        public string MobileNumber { get; private set; }
        public string Gender { get; private set; }        
        public string? ProfileImage { get; private set; }
        public string? TransactionPinHash { get; private set; }
        public ProfileType UserType { get; private set; }        
        public Wallet Wallet { get; set; }


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
                UserType = profile.userType
            };
            return newProfile;
        }


        public Wallet CreateWallet(Guid profileId, string walletNumber)
        {
            if (string.IsNullOrEmpty(walletNumber)) throw new ArgumentException("Wallet number is required");            
            if (profileId == Guid.Empty) throw new ArgumentException("Profile Id is required");
            if (walletNumber.Length < 10) throw new ArgumentException("Wallet Number must be 10 digits");
            long walletNum = long.Parse(walletNumber);
            if (walletNum < 0) throw new ArgumentException("Invalid Wallet number");

            var userWallet = Wallet.Create(profileId, walletNumber);
            userWallet.GetCheckSum();
            return userWallet;
        }
        

        public void Update(string firstName, string lastName, string otherName, string emailAddress, string mobileNumber)
        {
            if (string.IsNullOrEmpty(firstName)) throw new ArgumentException("First Name is required");
            if (string.IsNullOrEmpty(lastName)) throw new ArgumentException("Last Name is required");
            if (string.IsNullOrEmpty(emailAddress)) throw new ArgumentException("Email Address is required");
            if (string.IsNullOrEmpty(firstName)) throw new ArgumentException("Mobile Number is required");

            FirstName = firstName;
            LastName = lastName;
            OtherName = otherName;
            EmailAddress = emailAddress;
            MobileNumber = mobileNumber;
        }
       
        public void UpdateProfileImage(string imageUrl)
        {
            if (string.IsNullOrEmpty(imageUrl)) throw new ArgumentException("Image is required");

            ProfileImage = imageUrl;
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
