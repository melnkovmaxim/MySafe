using Fody;
using MediatR;
using RestSharp;
using RestSharp.Authenticators;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MySafe.Business.Extensions;
using MySafe.Business.Mediator.Abstractions;
using MySafe.Core.Entities.Responses;

namespace MySafe.Business.Mediator.Folders.FolderInfoQuery
{
    public class FolderInfoQueryHandler : RequestHandlerBase<FolderInfoQuery, Folder>
    {
        public FolderInfoQueryHandler(IRestClient restClient) : base(restClient)
        {
        }
    }
}
