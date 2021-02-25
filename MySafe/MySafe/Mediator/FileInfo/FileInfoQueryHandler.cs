using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MySafe.Extensions;
using MySafe.Mediator.FolderInfo;
using MySafe.Models.Responses;
using RestSharp;
using RestSharp.Authenticators;

namespace MySafe.Mediator.FileInfo
{
    public class FileInfoQueryHandler : IRequestHandler<FileInfoQuery, FileResponse>
    {
        private readonly IRestClient _restClient;
        private readonly IMapper _mapper;
        
        public FileInfoQueryHandler(IRestClient restClient, IMapper mapper)
        {
            _restClient = restClient;
            _mapper = mapper;
        }

        public async Task<FileResponse> Handle(FileInfoQuery request, CancellationToken cancellationToken)
        {
            
            _restClient.Authenticator = new JwtAuthenticator(request.JwtToken.RawData);
            var httpRequest = new RestRequest($"api/v1/documents/{request.FileId}", Method.GET);
            var cmdResponse = await _restClient.GetResponseAsync<FileResponse>(httpRequest, cancellationToken);

            return cmdResponse;
        }
    }
}
