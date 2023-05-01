using CloudinaryDotNet.Actions;

namespace API.Interfaces
{
    public interface IAnalysisResultFileService
    {
        Task<RawUploadResult> AddDocumentAsync(Stream document);
        Task<DeletionResult> DeleteDocumentAsync(string publicId);
    }
}