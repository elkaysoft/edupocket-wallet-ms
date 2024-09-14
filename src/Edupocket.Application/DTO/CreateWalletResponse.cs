using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Edupocket.Application.ResponseModels;

namespace Edupocket.Application.DTO
{
    public class CreateWalletResponse : BaseResponse
    {
        public CreateWalletResponse() : base()
        {
        }


        public CreateWalletDetails Result { get; set; } = default!;
    }

    public class CreateWalletDetails
    {
        public string WalletNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string OtherName { get; set; }
        public string EmailAddress { get; set; }
        public string MobileNumber { get; set; }
    }
}
