namespace Edupocket.Domain.AggregatesModel.WalletSchemeAggregate
{
    public class WalletSchemeAccount
    {
        public Guid WalletSchemeId { get; set; }
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public string CheckSum { get; set; }
    }
}
