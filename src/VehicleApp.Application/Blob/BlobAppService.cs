using System.Threading.Tasks;
using Volo.Abp.BlobStoring;
using VehicleApp.Application.Contracts.Blob;
using VehicleApp.Application.Contracts.Blob.Dtos;
using Volo.Abp.Application.Services;
using System.IO;
using System;
using VehicleApp.Domain.Shared;
using System.Linq;
using Volo.Abp;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Timing;
using System.Threading;

namespace VehicleApp.Application.Blob;

public class BlobAppService(
    IBlobContainer blobContainer) : ApplicationService, IBlobAppService
{
    private void ValidateFile(SaveBlobInput input)
    {
        // 验证文件大小
        if (input.Content.Length > FileValidationConsts.MaxFileSize)
        {
            throw new UserFriendlyException($"文件大小不能超过 {FileValidationConsts.MaxFileSize / 1024 / 1024}MB");
        }

        // 验证文件类型
        var extension = Path.GetExtension(input.Name).ToLower();
        if (!FileValidationConsts.AllowedImageExtensions.Contains(extension) &&
            !FileValidationConsts.AllowedDocumentExtensions.Contains(extension))
        {
            throw new UserFriendlyException($"不支持的文件类型: {extension}");
        }

        // 验证 MIME 类型
        if (input.ContentType != null &&
            !FileValidationConsts.AllowedImageMimeTypes.Contains(input.ContentType.ToLower()))
        {
            if (!input.ContentType.StartsWith("application/"))  // 对于文档类型的特殊处理
            {
                throw new UserFriendlyException($"不支持的文件类型: {input.ContentType}");
            }
        }
    }

    /// <summary>
    /// 保存图片
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public virtual async Task<BlobDto> SaveAsync(SaveBlobInput input)
    {
        ValidateFile(input);

        // 生成唯一文件名
        var extension = Path.GetExtension(input.Name);
        var fileName = $"{Guid.NewGuid()}{extension}";

        await blobContainer.SaveAsync(fileName, input.Content, overrideExisting: true);

        var content = await blobContainer.GetAllBytesAsync(fileName);
        return new BlobDto
        {
            Name = fileName,
            ContentType = input.ContentType,
            Size = input.Content.Length,
            Content = content,
            Url = $"https://localhost:9090/files/{fileName}"
        };
    }

    public virtual async Task<byte[]> GetBlobAsync(string path, CancellationToken cancellationToken)
    {
        try
        {
            var flie = await blobContainer.GetAllBytesAsync(path, cancellationToken);

            return flie;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public virtual async Task<BlobDto> GetAsync(string name)
    {
        try
        {
            var blob = await blobContainer.GetAllBytesAsync(name);
            var contentType = GetContentType(name);

            return new BlobDto
            {
                Name = Path.GetFileName(name),
                ContentType = contentType,
                Size = blob.Length,
            };
        }
        catch (Exception)
        {
            throw new UserFriendlyException($"找不到文件: {name}");
        }
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

    public async Task DeleteAsync(string name)
    {
        await blobContainer.DeleteAsync(name);
    }

    public async Task<SaveBlobsResult> SaveMultipleAsync(SaveBlobsInput input)
    {
        var result = new SaveBlobsResult();

        foreach (var file in input.Files)
        {
            try
            {
                ValidateFile(file);
                var savedFile = await SaveAsync(file);
                result.SuccessFiles.Add(savedFile);
            }
            catch (Exception ex)
            {
                result.FailedFiles.Add(new FailedFileInfo
                {
                    FileName = file.Name,
                    Error = ex.Message
                });
            }
        }

        return result;
    }
}
