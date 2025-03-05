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

    public class SmsTokenExtensionGrantConsts
    {
        public const string GrantType = "phone_verify";

        public const string ParamName = "phone_number";

        public const string TokenName = "phone_verify_code";

        public const string Purpose = "phone_verify";

        public const string SecurityCodeFailed = "SecurityCodeFailed";
    }
}
