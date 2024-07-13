using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edupocket.Domain.AggregatesModel.WalletAggregate
{
    public record ProfileCreationDTO(string firstName, string lastName, string otherName, string emailAddress, string phoneNumber,
        string gender, ProfileType profileType, string profileImage)
    {
    }
}
