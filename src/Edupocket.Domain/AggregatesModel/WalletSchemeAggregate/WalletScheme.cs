using Edupocket.Domain.Contracts;
using Edupocket.Domain.SeedWork;

namespace Edupocket.Domain.AggregatesModel.WalletSchemeAggregate
{
    public class WalletScheme: BaseEntity<Guid>, IAggregateRoot
    {
        public WalletScheme(Guid id): base(id) { }
         
        private WalletScheme(): base(Guid.NewGuid()) { }

        public string Name { get; set; }
        public string Code { get; set; }
        public bool IsActive { get; set; }
        
        public WalletSchemeType WalletSchemeType { get; set; }


        public static WalletScheme Create(string name, string code, WalletSchemeType schemeType) 
        {
            return new WalletScheme
            {
                IsActive = true,
                Code = code,
                Name = name,
                WalletSchemeType = schemeType
            };
        }

        

    }
}
