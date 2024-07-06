using Edupocket.Domain.AggregatesModel;
using Edupocket.Domain.AggregatesModel.WalletAggregate;

namespace Domain.UnitTest
{
    public class UnitTest1
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
                "Male", ProfileType.Student, "");
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

    }
}