﻿using MySafe.Core.Models.Responses;
using MySafe.Services.Mediator.Abstractions;
using RestSharp;

namespace MySafe.Services.Mediator.Images.OriginalImageQuery
{
    /// <summary>
    ///     Получить оригинальное изображение
    /// </summary>
    public class OriginalImageQuery : BearerRequestBase<ImageEntity>
    {
        public OriginalImageQuery(int imageId)
        {
            ImageId = imageId;
        }

        public int ImageId { get; set; }

        public override Method RequestMethod => Method.GET;
        public override string RequestResource => $"/api/v1/images/{ImageId}";
    }
}