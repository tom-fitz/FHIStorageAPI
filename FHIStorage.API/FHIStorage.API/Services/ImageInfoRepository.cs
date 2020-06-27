using FHIStorage.API.Entities;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Protocols;
using Microsoft.WindowsAzure.Storage;
using Remotion.Linq.Clauses;
using Microsoft.Extensions.Configuration;
using SixLabors.ImageSharp;

namespace FHIStorage.API.Services
{
    public class ImageInfoRepository : IImageInfoRepository
    {
        private HouseInfoContext _ctx;
        public ImageInfoRepository(HouseInfoContext ctx)
        {
            _ctx = ctx;
        }

        static string storageConn = Environment.GetEnvironmentVariable("StorageAccountConnectionString");
        static string storageCred = Environment.GetEnvironmentVariable("StorageCredentials");

        CloudBlobClient blobClient;
        string baseUri = "https://fhistorage.blob.core.windows.net/";

        CloudStorageAccount storageAccount = CloudStorageAccount.Parse(storageConn);
        
        public ImageInfoRepository()
        {
            var credentials = new StorageCredentials("fhistorage", storageCred);
            blobClient = new CloudBlobClient(new Uri(baseUri), credentials);
        }
        public void AddNewFurnitureImage(FurnitureImage newImage)
        {
            _ctx.FurnitureImages.Add(newImage);
            _ctx.SaveChanges();
        }
        public void AddBulkFurnitureImages(List<FurnitureImage> bulkImages)
        {
            foreach(FurnitureImage newImage in bulkImages)
            {
                _ctx.FurnitureImages.Add(newImage);
            }
            _ctx.SaveChanges();
        }
        public async Task<string> SaveImage(Stream imageStream)
        {
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            var imageId = Guid.NewGuid().ToString();
            var container = blobClient.GetContainerReference("furnitureimages");
            var blob = container.GetBlockBlobReference(imageId);
            blob.Properties.ContentType = "image/jpg";
            await blob.UploadFromStreamAsync(imageStream);
            return imageId;
        }

        public void DeleteImage(string guid, FurnitureImage furnImageToDelete)
        {
            // delete image from blob based on guid
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference("furnitureimages");
            var blob = container.GetBlockBlobReference(guid);
            blob.DeleteIfExistsAsync();
            DeleteImageFromDB(furnImageToDelete);
        }

        private void DeleteImageFromDB(FurnitureImage furnImageToDelete)
        {
            _ctx.FurnitureImages.Remove(furnImageToDelete);
            _ctx.SaveChanges();
        }

        public FurnitureImage GetImageByFurnitureId(int furnitureId)
                                      {
            var checkFurnitureImage = _ctx.FurnitureImages.FirstOrDefault(f => f.FurnitureId == furnitureId);

            return checkFurnitureImage;
        }

        public string GetImageByImageId(int imageId)
        {
            var getImageUri = _ctx.FurnitureImages.FirstOrDefault(f => f.FurnitureImageId == imageId);

            var imageUri = UriFor(getImageUri.PictureInfo);

            return imageUri;
        }

        public string UriFor(string imageId)
        {
            var sasPolicy = new SharedAccessBlobPolicy
            {
                Permissions = SharedAccessBlobPermissions.Read,
                SharedAccessStartTime = DateTime.UtcNow.AddMinutes(-15),
                SharedAccessExpiryTime = DateTime.UtcNow.AddMinutes(15)
            };

            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference("furnitureimages");
            var blob = container.GetBlockBlobReference(imageId);
            // sas is for secure back and for. Will need this once authentication is put in place.
            //var sas = blob.GetSharedAccessSignature(sasPolicy);
            return $"{baseUri}furnitureimages/{imageId}";
        }
    }
}
