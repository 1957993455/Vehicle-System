using System;
using System.Collections.Generic;

namespace VehicleApp.Application.Contracts.Blob.Dtos;

public class BlobDto
{
    public string Name { get; set; }
    public string ContentType { get; set; }
    public long Size { get; set; }
    public byte[] Content { get; set; }
    public string Url { get; set; }
}

public class SaveBlobInput
{
    public string Name { get; set; }
    public string ContentType { get; set; }
    public byte[] Content { get; set; }
}

public class SaveBlobsInput
{
    public List<SaveBlobInput> Files { get; set; }
}

public class SaveBlobsResult
{
    public List<BlobDto> SuccessFiles { get; set; } = new();
    public List<FailedFileInfo> FailedFiles { get; set; } = new();
}

public class FailedFileInfo
{
    public string FileName { get; set; }
    public string Error { get; set; }
}
