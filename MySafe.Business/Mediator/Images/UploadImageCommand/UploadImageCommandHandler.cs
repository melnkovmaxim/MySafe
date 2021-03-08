using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MySafe.Business.Mediator.Abstractions;
using MySafe.Core.Entities.Responses;
using RestSharp;
using RestSharp.Authenticators;

namespace MySafe.Business.Mediator.Images.UploadImageCommand
{
    public class UploadImageCommandHandler: RequestHandlerBase<UploadImageCommand, Image>
    {
        public UploadImageCommandHandler(IRestClient restClient, IMapper mapper) : base(restClient, mapper)
        {
        }
    }
}
