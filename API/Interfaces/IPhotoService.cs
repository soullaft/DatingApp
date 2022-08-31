using CloudinaryDotNet.Actions;

namespace API.Interfaces
{
    /// <summary>
    /// Represent base photo service contract
    /// </summary>
    public interface IPhotoService
    {
        /// <summary>
        /// Add photo async
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        Task<ImageUploadResult> AddPhotoAsync(IFormFile file);

        /// <summary>
        /// Delete photo async
        /// </summary>
        /// <param name="publicId"></param>
        /// <returns></returns>
        Task<DeletionResult> DeletePhotoAsync(string publicId);
    }
}
