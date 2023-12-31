﻿using Ardalis.Result;
using Contracts.Dto;
using Domain.Entities;
using Domain.Repo;
using FileSignatures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore;
using Services.Services.Interfaces;
using System.IO;
using Domain.Errors;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Services.Services
{
    public class ImageService : IImageService
    {
        private readonly string baseFilePath;
        private readonly IImageRepository imgRepo;
        private readonly IUserProfileRepository userProfileRepo;
        private readonly IFriendshipRequestRepository friendshipRequestRepository;
        private readonly IFileFormatInspector fileFormatInspector;

        public ImageService(string baseFilePath,
                            IImageRepository imgRepo,
                            IUserProfileRepository userProfileRepo,
                            IFileFormatInspector fileFormatInspector,
                            IFriendshipRequestRepository friendshipRequestRepository)
        {
            if (!Directory.Exists(baseFilePath))
                throw new DirectoryNotFoundException($"{baseFilePath} does not exist, please provide valid directory path in appsettings.json file");
            this.baseFilePath=baseFilePath;
            this.imgRepo=imgRepo;
            this.userProfileRepo=userProfileRepo;
            this.fileFormatInspector=fileFormatInspector;
            this.friendshipRequestRepository=friendshipRequestRepository;
        }
        /// <inheritdoc/>

        public async Task<Result<List<(ImageId imgId, string key)>>> GetUserImageUrlsQueryData(string otherUserName, string myIdentityId)
        {
            var otherResult = await userProfileRepo.GetByUserNameAsync(otherUserName);
            if (!otherResult.IsSuccess) return DomainErrors.User.NotFound;
            var userResult = await userProfileRepo.GetByIdentityAsync(myIdentityId);
            if (!userResult.IsSuccess) return DomainErrors.User.NotFound;
            return await GetImageUrlsQueryData(otherResult.Value, userResult.Value);

        }
        /// <inheritdoc/>

        public async Task<Result<List<(ImageId imgId, string key)>>> GetUserImageUrlsQueryData(string myIdentityId)
        {
            var userResult = await userProfileRepo.GetByIdentityAsync(myIdentityId);
            if (!userResult.IsSuccess) return DomainErrors.User.NotFound;
            return await GetImageUrlsQueryData(userResult.Value, userResult.Value);
        }
        /// <inheritdoc/>

        public async Task<Result<string>> GetImageFilePath(ImageId id, string key)
        {
            Result<Image> imageResult = await imgRepo.GetByIdAsync(id);
            if (!imageResult.IsSuccess) return DomainErrors.Image.NotFound;
            if (imageResult.Value.Key!=key) return Result.Forbidden();
            string path = Path.Combine(baseFilePath, $"{imageResult.Value.FileName}");
            return path;
        }
        /// <inheritdoc/>

        public async Task<Result> UploadImage(string identityId, UploadedFile imageToUpload)
        {
            Result<User> userResult = await userProfileRepo.GetByIdentityAsync(identityId);
            if (userResult.IsSuccess==false)
                return DomainErrors.User.NotFound;
            ImageId imgId = new(Guid.NewGuid());
            Result<string> imageFileSaveResult = await SaveImageFile(imgId, imageToUpload.FileContent);
            if (!imageFileSaveResult.IsSuccess)
                return DomainErrors.Image.CouldNotSaveFile;
            string fileName = imageFileSaveResult.Value;
            Result<string> imgFileNameInsertResult = await InsertImagePath(userResult.Value.Id, imgId, fileName);
            if (!imgFileNameInsertResult.IsSuccess)
                return DomainErrors.Image.CouldNotSaveFilePath;
            return Result.Success();
        }

        private async Task<Result<List<(ImageId imgId, string key)>>> GetImageUrlsQueryData(User other, User me)
        {
            if (!(await IsAllowedToGetUserImages(other.Id, me))) return Result.Forbidden();
            List<(ImageId imgId, string key)> queryData = other.Images.ToList().ConvertAll(x => (x.Id, x.Key));
            return Result.Success(queryData);
        }
        private async Task<Result<string>> SaveImageFile(ImageId id, byte[] content)
        {
            using (MemoryStream stream = new(content))
            {
                FileFormat? format = fileFormatInspector.DetermineFileFormat(stream);
                if (format==null||!(format is FileSignatures.Formats.Image))
                    return DomainErrors.Image.WrongFormat;
                string fNameWithExtension = $"{id.Value}.{format.Extension}";

                using (FileStream fs = File.Create($"{baseFilePath}\\{fNameWithExtension}"))
                {
                    await stream.CopyToAsync(fs);
                    fs.Close();
                }
                stream.Close();
                return Result.Success(fNameWithExtension);
            }
        }

        private async Task<Result<string>> InsertImagePath(UserId userId, ImageId imageId, string fileName)
        {
            Image image = new(imageId, userId, fileName);
            Result result = await imgRepo.InsertAsync(image);
            if (result.IsSuccess)
                return Result.Success(fileName);
            else
                return DomainErrors.Image.CouldNotSaveFilePath;
        }

        private async Task<Result<bool>> IsAllowedToGetUserImages(UserId otherId, User user)
        {
            if (otherId==user.Id) 
                return Result.Success(true);

            Result<bool> isInFriendListResult = await userProfileRepo.IsInFriendlist(user.Id, otherId);
            if (!isInFriendListResult.IsSuccess) return DomainErrors.Friendship.CheckFriendlist;
            if (isInFriendListResult.Value)
                return Result.Success(true);

            Result<bool> pendingFriendshipReqResult = await friendshipRequestRepository.CheckForPendingFriendshipRequestAsync(otherId,
                                                                                                                     user.Id);
            if (!pendingFriendshipReqResult.IsSuccess) return DomainErrors.Friendship.CheckPendingRequest;
            if (pendingFriendshipReqResult.Value) 
                return Result.Success(true);
            return Result.Success(false);
        }

    }


}
