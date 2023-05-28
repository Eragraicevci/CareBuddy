using CloudinaryDotNet.Actions;

namespace Core.Interfaces
{
    public interface IAnalysisResultFileService
    {
        Task<RawUploadResult> AddDocumentAsync(Stream document);
        Task<DeletionResult> DeleteDocumentAsync(string publicId);
    }
}