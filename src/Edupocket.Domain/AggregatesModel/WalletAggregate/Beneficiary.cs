using Edupocket.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edupocket.Domain.AggregatesModel.WalletAggregate
{
    public class Beneficiary : ValueObject
    {
        public string Name { get; private set; }
        public string WalletNumber { get; private set; }
        public string NickName { get; private set; }

        public Beneficiary()
        {            
        }

        public Beneficiary(string name, string walletNumber, string nickName)
        {
            if(string.IsNullOrEmpty(name)) throw new ArgumentException("Beneficiary name is required");
            if (string.IsNullOrEmpty(walletNumber)) throw new ArgumentException("Beneficiary Wallet number is required");
            if(string.IsNullOrWhiteSpace(nickName)) throw new ArgumentException("Beneficiary nickName is required");

            Name = name;
            WalletNumber = walletNumber;
            NickName = nickName;
        }

        //protected bool Equals(Beneficiary other)
        //{
        //    return string.Equals(Name, other.Name) && string.Equals(WalletNumber, other.WalletNumber) && string.Equals(NickName, other.NickName);
        //}

        //public override bool Equals(object obj)
        //{
        //    if(ReferenceEquals(null, obj)) return false;
        //    if(ReferenceEquals (this, obj)) return true;
        //    if(obj.GetType() != GetType()) return false;

        //    return Equals((Beneficiary)obj);
        //}

        //public override int GetHashCode()
        //{
        //    unchecked
        //    {

        //    }


        //    return base.GetHashCode();
        //}

        protected override IEnumerable<object> GetEqualityComponents()
        {
            // Using a yield return statement to return each element one at a time
            yield return Name;
            yield return WalletNumber;
            yield return NickName;
        }
    }
}
