using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VehicleApp.Application.Contracts.Blob;
using VehicleApp.Application.Contracts.Blob.Dtos;
using Volo.Abp;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;

namespace VehicleApp.HttpApi.Controllers;

[RemoteService]
[Area("Blob")]
[Route("api/blob")]
public class BlobController : VehicleAppController
{
    private readonly IBlobAppService _blobAppService;

    public BlobController(IBlobAppService blobAppService)
    {
        _blobAppService = blobAppService;
    }

    [HttpPost("upload")]
    [AllowAnonymous]
    [RequestSizeLimit(5 * 1024 * 1024)] // 5MB 限制
    public async Task<BlobDto> UploadAsync([Required] IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            throw new UserFriendlyException("文件不能为空");
        }

        using var ms = new MemoryStream();

        await file.CopyToAsync(ms);

        var result = await _blobAppService.SaveAsync(new SaveBlobInput
        {
            Name = file.FileName,
            ContentType = file.ContentType,
            Content = ms.ToArray()
        });
        return result;
    }

    [AllowAnonymous]
    [HttpGet("download/{*path}")]
    public async Task<IActionResult> DownloadAsync(string path)
    {
        if (string.IsNullOrEmpty(path))
        {
            return BadRequest("文件路径不能为空");
        }

        var fileBytes = await _blobAppService.GetBlobAsync(path);
        var contentType = GetContentType(path);
        var fileName = Path.GetFileName(path);

        return File(fileBytes, contentType, fileName);
    }

    private string GetContentType(string fileName)
    {
        var extension = Path.GetExtension(fileName).ToLower();
        return extension switch
        {
            ".jpg" or ".jpeg" => "image/jpeg",
            ".png" => "image/png",
            ".gif" => "image/gif",
            ".pdf" => "application/pdf",
            _ => "application/octet-stream"
        };
    }

    [AllowAnonymous]
    [HttpGet("{name}")]
    public async Task<IActionResult> GetAsync(string name)
    {
        var blob = await _blobAppService.GetBlobAsync(name);
        return Ok(blob);
    }

    [HttpDelete("{name}")]
    public Task DeleteAsync(string name)
    {
        return _blobAppService.DeleteAsync(name);
    }

    [AllowAnonymous]
    [HttpPost("upload-multiple")]
    [RequestSizeLimit(20 * 1024 * 1024)] // 20MB 总限制
    public async Task<SaveBlobsResult> UploadMultipleAsync(IFormFileCollection files)
    {
        if (files == null || !files.Any())
        {
            throw new UserFriendlyException("文件不能为空");
        }

        var input = new SaveBlobsInput
        {
            Files = new List<SaveBlobInput>()
        };

        foreach (var file in files)
        {
            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);

            input.Files.Add(new SaveBlobInput
            {
                Name = file.FileName,
                ContentType = file.ContentType,
                Content = ms.ToArray()
            });
        }

        return await _blobAppService.SaveMultipleAsync(input);
    }

    [AllowAnonymous]
    [HttpGet("upload-progress/{id}")]
    public async Task<IActionResult> GetUploadProgressAsync(string id)
    {
        return Ok(new { progress = 100 });
    }
}
