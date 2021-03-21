using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySafe.Business.Mediator.Abstractions;
using MySafe.Core.Entities.Responses;
using RestSharp;

namespace MySafe.Business.Mediator.Images.ChangeImageCommand
{
    public class ChangeImageCommandHandler: RequestHandlerBase<ChangeImageCommand, Image>
    {
        public ChangeImageCommandHandler(IRestClient restClient) : base(restClient)
        {
        }
    }
}
