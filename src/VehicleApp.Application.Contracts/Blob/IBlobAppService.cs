using System.IO;
using System.Threading;
using System.Threading.Tasks;
using VehicleApp.Application.Contracts.Blob.Dtos;

namespace VehicleApp.Application.Contracts.Blob;

public interface IBlobAppService
{
    Task<BlobDto> SaveAsync(SaveBlobInput input);

    Task<byte[]> GetBlobAsync(string path, CancellationToken cancellationToken = default);

    Task DeleteAsync(string name);

    Task<SaveBlobsResult> SaveMultipleAsync(SaveBlobsInput input);

    Task<Stream> GetBlobStreamAsync(string path, CancellationToken cancellationToken = default);
}
