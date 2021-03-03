﻿using MediatR;
using RestSharp;
using System.IdentityModel.Tokens.Jwt;

namespace MySafe.Business.Mediator.Sheets.UploadFile
{
    public class UploadFileCommand: IRequest<IRestResponse>
    {
        public JwtSecurityToken JwtToken { get; set; }
        public int DocumentId { get; set; }
        public string FileName { get; }
        public string ContentType { get; }
        public byte[] FileBytes { get; }

        public UploadFileCommand(JwtSecurityToken jwtToken, int documentId, string fileName, string contentType, byte[] fileBytes)
        {
            JwtToken = jwtToken;
            DocumentId = documentId;
            FileName = fileName;
            ContentType = contentType;
            FileBytes = fileBytes;
        }
    }
}