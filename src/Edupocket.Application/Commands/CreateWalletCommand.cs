using Edupocket.Application.ResponseModels;
using Edupocket.Domain.AggregatesModel.WalletAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edupocket.Application.Commands
{
    public class CreateWalletCommand: IRequest<CreateWalletResponse>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? OtherName { get; set; }
        public string? EmailAddress { get; set; }
        public string? MobileNumber { get; set; }
        public ProfileType ProfileType { get; set; }
        public string? Gender { get; set; }
    }
}
