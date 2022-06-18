using IMgzavri.FileStore.Commands.Commands;
using IMgzavri.FileStore.Commands.Models;
using IMgzavri.FileStore.Queries.Queries;
using IMgzavri.Shared.Constants;
using IMgzavri.Shared.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleSoft.Mediator;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace IMgzavri.FileStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        protected readonly IMediator Mediator;

        public FileController(IMediator mediator)
        {
            Mediator = mediator;
        }

        [HttpPost("upload-files-bytes")]
        public async Task<IActionResult> UploadFilesBytesAsync(List<FileSavingModel> files, CancellationToken ct)
        {
            var result = await Mediator.SendAsync(new UploadFilesCommand(files), ct);

            return Ok(result);
        }

        [HttpPost("upload-file-bytes")]
        public async Task<IActionResult> UploadFileBytesAsync(FileSavingModel file, CancellationToken ct)
        {
            var result = await Mediator.SendAsync(new UploadFileCommand(file), ct);

            return Ok(result);
        }

        [HttpGet("load-file-bytes/{id}")]
        public async Task<IActionResult> LoadFileBytesAsync(Guid id, CancellationToken ct)
        {
            var result = await Mediator.FetchAsync(new GetFileQuery(id), ct);

            return Ok(result);
        }

        [HttpGet("load-file-physical-path/{id}")]
        public async Task<IActionResult> LoadFilePhysicalPathAsync(Guid id, CancellationToken ct)
        {
            var result = await Mediator.FetchAsync(new GetFilePhysicalPathQuery(id), ct);

            return Ok(result);
        }

        [HttpPost("load-files-physical-paths")]
        public async Task<IActionResult> LoadFilesPhysicalPathsAsync(GetFilesPhysicalPathsQuery query, CancellationToken ct)
        {
            var result = await Mediator.FetchAsync(query, ct);

            return Ok(result);
        }

        [HttpGet("download/{id}")]
        public async Task<IActionResult> DownloadFileAsync(Guid id, CancellationToken ct)
        {
            var result = await Mediator.FetchAsync(new DownloadFileQuery(id), ct);

            if (result.Status != ResultStatus.Success)
                return Ok(Result.Error("Unknown error"));

            return File(result.Parameters[FileStorageConstants.DownloadFileStreamParameterName] as MemoryStream,
                result.Parameters[FileStorageConstants.DownloadFileTypeParameterName].ToString(),
                result.Parameters[FileStorageConstants.DownloadFileNameParameterName].ToString());
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteFileAsync(Guid id, CancellationToken ct)
        {
            var result = await Mediator.SendAsync(new DeleteFileCommand(id), ct);

            return Ok(result);
        }
    }
}
