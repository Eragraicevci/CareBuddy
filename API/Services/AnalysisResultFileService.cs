using API.Helpers;
using API.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;

namespace API.Services
{
    public class AnalysisResultFileService : IAnalysisResultFileService
    {
        private readonly Cloudinary _cloudinary;

        public AnalysisResultFileService(IOptions<CloudinarySettings> config)
        {
            var acc = new Account(
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(acc);
        }

        public async Task<RawUploadResult> AddDocumentAsync(Stream document)
        {
            var uploadParams = new RawUploadParams
            {
                File = new FileDescription("document", document),
                PublicId = Guid.NewGuid().ToString(),
                Tags = "pdf_upload"
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            return uploadResult;
        }



        public async Task<DeletionResult> DeleteDocumentAsync(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);

            return await _cloudinary.DestroyAsync(deleteParams);
        }
    }
}
