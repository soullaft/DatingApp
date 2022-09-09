using API.Helpers;
using API.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using Throw;

namespace API.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly Cloudinary _cloudinary;

        /// <summary>
        /// Default constuctor
        /// </summary>
        /// <param name="config">Option config of <see cref="CloudinarySettings"/></param>
        public PhotoService(IOptions<CloudinarySettings> config)
        {
            // initialize cloudinary account from config file
            var account = new Account(
                cloud: config.Value.CloudName,
                apiKey: config.Value.ApiKey,
                apiSecret: config.Value.ApiSecret);

            _cloudinary = new Cloudinary(account);
        }

        /// <summary>
        /// Add photo assync to a current user profile
        /// </summary>
        /// <param name="file"></param>
        /// <returns>Returns <see cref="ImageUploadResult"/> of cloudinary service</returns>
        /// <exception cref="ArgumentNullException">throws if file length was 0 </exception>
        public async Task<ImageUploadResult> AddPhotoAsync(IFormFile file)
        {
            file.Length.Throw().IfEquals(0);

            using var stream = file.OpenReadStream();

            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                Transformation = new Transformation()
                    .Height(MsgConst.IMAGE_DEFAULT_HEIGHT).Width(MsgConst.IMAGE_DEFAULT_WIDTH)
                    .Crop("fill").Gravity("face")
            };

            return await _cloudinary.UploadAsync(uploadParams);
        }

        /// <summary>
        /// Delete photo async
        /// </summary>
        /// <param name="publicId">public id of an image</param>
        /// <returns></returns>
        public async Task<DeletionResult> DeletePhotoAsync(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);

            return await _cloudinary.DestroyAsync(deleteParams);
        }
    }
}
