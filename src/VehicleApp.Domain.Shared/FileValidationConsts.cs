namespace VehicleApp.Domain.Shared;

public static class FileValidationConsts
{
    public static readonly string[] AllowedImageExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
    public static readonly string[] AllowedDocumentExtensions = { ".pdf", ".doc", ".docx", ".xls", ".xlsx" };

    public static readonly string[] AllowedImageMimeTypes =
    {
        "image/jpeg",
        "image/png",
        "image/gif"
    };

    public const int MaxFileSize = 5 * 1024 * 1024; // 5MB
}