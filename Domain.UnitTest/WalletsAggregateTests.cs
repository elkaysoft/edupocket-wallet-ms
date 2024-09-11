using Edupocket.Domain.AggregatesModel;
using Edupocket.Domain.AggregatesModel.WalletAggregate;

namespace Domain.UnitTest
{
    public class WalletsAggregateTests
    {
        [Fact]
        public void Ensure_profile_creation_returns_null()
        {
            //arrange

            //arrange and act
            var profile = Profile.Create(null);
            //assert
            Assert.Null(profile);
        }

        [Fact]
        public void Ensure_profile_creation_returns_data()
        {
            //arrange            
            var profileDto = new ProfileCreationDTO("Olamilekan", "Sheriff", "", "elkaysoft@yahoo.com", "07038704611",
                "Male", ProfileType.Student);
            //act
            var profile = Profile.Create(profileDto);

            //assert
            Assert.NotNull(profile);
        }

        [Fact]
        public void Ensure_Beneficiary_creation_returns_exception_for_empty_firstname()
        {
            //arrange
            var profile = new Profile(Guid.NewGuid());

            //act            
            Action act = () => profile.AddBeneficiary("", "7039704622", "Johnny");

            //assert
            ArgumentException exception = Assert.Throws<ArgumentException>(act);            
        }

        [Fact]
        public void Ensure_Beneficiary_creation_returns_exception_for_empty_walletNumber()
        {
            //arrange
            var profile = new Profile(Guid.NewGuid());

            //act            
            Action act = () => profile.AddBeneficiary("John Doe", "", "Johnny");

            //assert
            ArgumentException exception = Assert.Throws<ArgumentException>(act);
        }

        [Fact]
        public void Ensure_Beneficiary_creation_returns_exception_for_empty_Nicname()
        {
            //arrange
            var profile = new Profile(Guid.NewGuid());

            //act            
            Action act = () => profile.AddBeneficiary("John Doe", "7038704611", "");

            //assert
            ArgumentException exception = Assert.Throws<ArgumentException>(act);
        }

        [Fact]
        public void Ensure_Wallet_Creation_throw_exception_when_profileId_is_empty()
        {
            //arrange
            var profile = new Profile(Guid.NewGuid());

            //act
            Action act = () => profile.CreateWallet(Guid.Empty, "7038704611");

            //asert
            ArgumentException exception = Assert.Throws<ArgumentException>(act);
        }

        [Fact]
        public void Ensure_Wallet_Creation_throw_exception_when_walletScheme_is_empty()
        {
            //arrange
            var profile = new Profile(Guid.NewGuid());

            //act
            Action act = () => profile.CreateWallet(Guid.NewGuid(), "7038704611");

            //asert
            ArgumentException exception = Assert.Throws<ArgumentException>(act);
        }

        [Fact]
        public void Ensure_Wallet_Creation_throw_exception_when_walletNumber_is_empty()
        {
            //arrange
            var profile = new Profile(Guid.NewGuid());

            //act
            Action act = () => profile.CreateWallet(Guid.NewGuid(), string.Empty);

            //asert
            ArgumentException exception = Assert.Throws<ArgumentException>(act);
        }

        [Fact]
        public void Ensure_Wallet_Creation_Throws_Exception_When_Less_Than_Ten_Digits()
        {
            //Arrange
            var profile = new Profile(Guid.NewGuid());

            //Act
            Action act = () => profile.CreateWallet(Guid.NewGuid(), "123456789");

            //Assert
            ArgumentException exception = Assert.Throws<ArgumentException>(act);
        }

        [Fact]
        public void Ensure_Beneficiary_WithNoName_Throws_Exception()
        {
            //Arrange
            var profile = new Profile(Guid.NewGuid());

            //Act
            Action act = () => profile.AddBeneficiary(string.Empty, "0299202091", "Ajoks");

            //Assert
            ArgumentException exception = Assert.Throws<ArgumentException>(act);
        }

        [Fact]
        public void Ensure_Beneficiary_WithNoWalletNumber_Throws_Exception()
        {
            //Arrange
            var profile = new Profile(Guid.NewGuid());

            //Act
            Action act = () => profile.AddBeneficiary("Ajoke Salawu", "", "Ajoks");

            //Assert
            ArgumentException exception = Assert.Throws<ArgumentException>(act);
        }


        [Fact]
        public void Beneficiary_Equals_ReturnsTrue_ForEqualValues()
        {
            //Arrange
            var profile = new Profile(Guid.NewGuid());

            //Act
            var beneficiary1 = profile.AddBeneficiary("Joke", "1203920391", "Ajoks");
            var beneficiary2 = profile.AddBeneficiary("Joke", "1203920391", "ajoks");

            //Assert
            Assert.Equal(beneficiary1, beneficiary2);
        }


    }
}