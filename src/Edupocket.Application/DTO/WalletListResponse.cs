using Azure;
using Edupocket.Application.ResponseModels;
using Edupocket.Domain.Enums;
using Edupocket.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edupocket.Application.DTO
{
    public class WalletListResponse: BaseResponse
    {
        public WalletListResponse(): base()
        {            
        }

        public PagedListModel<WalletListQueryVm> Result { get; set; } = default!;
    }

    public class WalletListQueryVm
    {
        public Guid Id { get; set; }
        public string WalletNumber { get; set; }
        public decimal Balance { get; set; }
        public Guid ProfileId { get; set; }
        public Status Status { get; set; }
        public bool IsPndActive { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string OtherName { get; set; }
        public string EmailAddress { get; set; }
        public string MobileNumber { get; set; }
        public string Gender { get; set; }
    }
}
