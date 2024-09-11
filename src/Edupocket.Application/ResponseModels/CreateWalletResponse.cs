using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edupocket.Application.ResponseModels
{
    public class CreateWalletResponse: BaseResponse
    {
        public CreateWalletResponse(): base()
        {            
        }


        public CreateWalletDto Result { get; set; }
    }

    public class CreateWalletDto
    {
        public string WalletNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string OtherName { get; set; }
        public string EmailAddress { get; set; }
        public string MobileNumber { get; set; }
    }
}
