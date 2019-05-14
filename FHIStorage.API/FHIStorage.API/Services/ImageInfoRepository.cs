﻿using FHIStorage.API.Entities;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Remotion.Linq.Clauses;

namespace FHIStorage.API.Services
{
    public class ImageInfoRepository : IImageInfoRepository
    {
        private HouseInfoContext _ctx;
        public ImageInfoRepository(HouseInfoContext ctx)
        {
            _ctx = ctx;
        }

        CloudBlobClient blobClient;
        string baseUri = "https://fhistorage.blob.core.windows.net/";

        CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=fhistorage;AccountKey=iYZjHO8U3IDj0eRc+KKmuncGA5G+C4KASPheQZMvOvsZ5y3lf3OFqit89P7bZU2bVD6R9/5qIUPGivFHoR83iA==;EndpointSuffix=core.windows.net");
        
        public ImageInfoRepository()
        {
            var credentials = new StorageCredentials("fhistorage", "iYZjHO8U3IDj0eRc+KKmuncGA5G+C4KASPheQZMvOvsZ5y3lf3OFqit89P7bZU2bVD6R9/5qIUPGivFHoR83iA==");
            blobClient = new CloudBlobClient(new Uri(baseUri), credentials);
        }
        public void AddNewFurnitureImage(FurnitureImage newImage)
        {
            _ctx.FurnitureImages.Add(newImage);
            _ctx.SaveChanges();
        }

        public async Task<string> SaveImage(Stream imageStream)
        {
            //CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=fhistorage;AccountKey=iYZjHO8U3IDj0eRc+KKmuncGA5G+C4KASPheQZMvOvsZ5y3lf3OFqit89P7bZU2bVD6R9/5qIUPGivFHoR83iA==;EndpointSuffix=core.windows.net");
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            var imageId = Guid.NewGuid().ToString();
            var container = blobClient.GetContainerReference("furnitureimages");
            var blob = container.GetBlockBlobReference(imageId);
            await blob.UploadFromStreamAsync(imageStream);
            return imageId;
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
            var sas = blob.GetSharedAccessSignature(sasPolicy);
            return $"{baseUri}fhistorage/{imageId}{sas}";
        }
    }
}
