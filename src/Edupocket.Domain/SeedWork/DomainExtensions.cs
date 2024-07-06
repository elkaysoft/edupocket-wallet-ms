using Edupocket.Domain.AggregatesModel.WalletAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edupocket.Domain.SeedWork
{
    public static class DomainExtensions
    {
        public static string GetCheckSum(this Wallet wallet)
        {
            string data = string.Concat(wallet.Id, "|", wallet.WalletNumber, "|", wallet.ProfileId, "|", wallet.Status.ToString(),
                    "|", wallet.Balance, "|", wallet.WalletSchemeId);

            return BCrypt.Net.BCrypt.EnhancedHashPassword(data);
        }

        public static bool ValidateWalletCheckSum(this Wallet wallet)
        {
            var data = string.Concat(wallet.Id, "|", wallet.WalletNumber, "|", wallet.ProfileId, "|", wallet.Status.ToString(),
                    "|", wallet.Balance, "|", wallet.WalletSchemeId);

            return BCrypt.Net.BCrypt.Verify(data, wallet.CheckSum);
        }
    }
}
