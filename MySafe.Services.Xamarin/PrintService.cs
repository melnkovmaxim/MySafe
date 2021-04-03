using MediatR;
using MySafe.Domain.Services;

namespace MySafe.Services.Xamarin
{
    public class PrintService : IPrintService
    {
        private readonly IFileService _fileService;
        private readonly IMediator _mediator;
        private readonly IPermissionService _permissionService;

        public PrintService(IPermissionService permissionService, IFileService fileService, IMediator mediator)
        {
            _permissionService = permissionService;
            _fileService = fileService;
            _mediator = mediator;
        }

        //public void Print()
        //{
        //    var isPermissionGranted = await _permissionService.TryGetStorageWritePermissionAsync();

        //    if (!isPermissionGranted) return;

        //    var path = _fileService.GetFullPathFileOnDevice(attachment.Name, attachment.FileExtension);

        //    if (!File.Exists(path)) await _downloadFileCommand.ExecuteAsync(attachment);

        //    if (attachment.IsImage)
        //    {
        //        var bytes = await File.ReadAllBytesAsync(path);

        //        await CrossPrinting.Current.PrintImageFromByteArrayAsync(bytes,
        //            new PrintJobConfiguration($"Printing {attachment.Name + attachment.FileExtension}"));

        //        return;
        //    }

        //    Stream stream;

        //    if (attachment.FileExtension == ".pdf")
        //    {
        //        stream = File.OpenRead(path);
        //    }
        //    else
        //    {
        //        var result = await _mediator.Send(new SheetPdfFormatQuery(attachment.Id));
        //        stream = new MemoryStream(result.FileBytes);
        //    }

        //    _ = await CrossPrinting.Current.PrintPdfFromStreamAsync(stream,
        //        new PrintJobConfiguration($"Printing {attachment.Name + attachment.FileExtension}"));

        //    await stream.DisposeAsync();
        //}
    }
}