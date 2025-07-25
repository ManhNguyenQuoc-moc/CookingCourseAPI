﻿using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace CookingCourseAPI.Services
{
    public class PhotoService
    {
        private readonly Cloudinary _cloudinary;

        public PhotoService(Cloudinary cloudinary)
        {
            _cloudinary = cloudinary;
        }
        public async Task<string> UploadImageAsync(IFormFile file)
        {
            if (file.Length <= 0) return null;

            await using var stream = file.OpenReadStream();
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                Transformation = new Transformation().Crop("fill").Gravity("face")
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);
            return uploadResult.SecureUrl.AbsoluteUri;  // Trả về URL ảnh
        }
    }
}
