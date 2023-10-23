using Ardalis.Result;
using Contracts.Dto;
using Domain.Entities;
using Domain.Repo;
using FileSignatures;
using Microsoft.AspNetCore.Mvc;
using Services.Services.Interfaces;
using System.IO;

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

        public async Task<Result<List<(ImageId imgId, string key)>>> GetUserImageUrlsQueryData(string otherUserName, string myIdentityId)
        {
            var userResult = await userProfileRepo.GetByUserNameAsync(otherUserName);
            if (!userResult.IsSuccess) return Result.NotFound("User not found");
            UserId otherId = userResult.Value.Id;
            if (!(await IsAllowedToGetUserImages(otherId, myIdentityId))) return Result.Forbidden();
            Result<List<(ImageId imgId, string key)>> result = await imgRepo.GetUserImageUrlsQueryData(otherId);
            if (!result.IsSuccess) return Result.NotFound();
            return result.Value;
        }

        public async Task<Result<string>> GetImageFilePath(ImageId id, string key)
        {
            Result<Image> imageResult = await imgRepo.GetByIdAsync(id);
            if (!imageResult.IsSuccess) return Result.Forbidden();
            if (imageResult.Value.Key!=key) return Result.Forbidden();
            string path = Path.Combine(baseFilePath, $"{imageResult.Value.FileName}");
            return path;
        }

        public async Task<Result> UploadImage(string identityId, UploadedFile imageToUpload)
        {
            Result<User> userResult = await userProfileRepo.GetByIdentityAsync(identityId);
            if (userResult.IsSuccess==false)
                return Result.NotFound($"User with identity {identityId} not found");
            ImageId imgId = new(Guid.NewGuid());
            Result<string> imageFileSaveResult = await SaveImageFile(imgId, imageToUpload.FileContent);
            if (!imageFileSaveResult.IsSuccess)
                return Result.Error("Image file could not be saved");
            string fileName = imageFileSaveResult.Value;
            Result<string> imgFileNameInsertResult = await InsertImagePath(userResult.Value.Id, imgId, fileName);
            if (!imgFileNameInsertResult.IsSuccess)
                return Result.Error("Image path could not be saved");
            return Result.Success();
        }

        private async Task<Result<string>> SaveImageFile(ImageId id, byte[] content)
        {
            using (MemoryStream stream = new(content))
            {
                FileFormat? format = fileFormatInspector.DetermineFileFormat(stream);
                if (format==null||!(format is FileSignatures.Formats.Image))
                    return Result.Error("Wrong file format");
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
                return Result.Error("Could not insert image path to the db");
        }

        private async Task<Result<bool>> IsAllowedToGetUserImages(UserId otherId, string myIdentityId)
        {
            Result<User> userResult = await userProfileRepo.GetByIdentityAsync(myIdentityId);
            if (!userResult.IsSuccess) return Result.NotFound("User with provided identity id not found");
            if (otherId==userResult.Value.Id) return Result.Success(true);

            Result<bool> isInFriendListResult = await userProfileRepo.IsInFriendlist(userResult.Value.Id, otherId);
            if (!isInFriendListResult.IsSuccess) return Result.Error("Couldnt check user existense in friendlist");
            if (isInFriendListResult.Value) return Result.Success(true);

            Result<bool> pendingFriendshipReqResult = await friendshipRequestRepository.CheckForPendingFriendshipRequestAsync(otherId,
                                                                                                                     userResult.Value.Id);
            if (!pendingFriendshipReqResult.IsSuccess) return Result.Error("Couldnt check for pending friendship request");
            if (pendingFriendshipReqResult.Value) return Result.Success(true);
            return Result.Success(true);
        }

    }


}
