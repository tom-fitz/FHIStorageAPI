using FHIStorage.API.Entities;
using FHIStorage.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Mime;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FHIStorage.API.Services;
using Microsoft.IdentityModel.Logging;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Remotion.Linq.Clauses.ResultOperators;

namespace FHIStorage.API.Controllers
{
    [Route("api")]
    public class ImageController : Controller
    {
        private IImageInfoRepository _imageInfoRepository;

        public ImageController(IImageInfoRepository imageInfoRepository)
        {
            _imageInfoRepository = imageInfoRepository;
        }

        [HttpGet("image/{imageId}")]
        public IActionResult GetImageByImageId(int imageId)
        {
            var imageUri = _imageInfoRepository.GetImageByImageId(imageId);

            var imageSrc = new FurnitureUriModel()
            {
                src = Convert.ToString(imageUri)
            };

            return Ok(imageSrc);
        }
        [HttpPost("image/bulk")]
        public async Task<IActionResult> BulkImageUpload(IFormCollection images)
        {
             if (images == null)
            {
                return BadRequest("no pictures were attached");
            };

            List<BulkImageModel> bulkObj = new List<BulkImageModel>();

            List<string> furnIdList = new List<string>();

            foreach( string k in images.Keys )
            {
                var v = images[k];
                var stringItems = v.ToString().Split(',');
                foreach(var stringItem in stringItems)
                {
                    furnIdList.Add(stringItem);
                }
            }

            for(var x = 0; x < images.Files.Count; x++)
            {
                var obj = new BulkImageModel
                {
                    FurnitureId = furnIdList[x],
                    image = images.Files[x]
                };
                bulkObj.Add(obj);
            }

            List<FurnitureImage> FurnitureImageList = new List<FurnitureImage>();

            foreach (var a in bulkObj)
            {
                var furnitureImage = new FurnitureImage();

                if (ModelState.IsValid)
                {
                    if (a.image != null && a.image.Length > 0)
                    {
                        using (Stream stream = a.image.OpenReadStream())
                        using (var outputStream = new MemoryStream())
                        using (var newImage = Image.Load(stream, new JpegDecoder()))
                        {
                            newImage.Mutate(x => x.AutoOrient());
                            newImage.SaveAsJpeg(outputStream);
                            outputStream.Position = 0;
                            var imageId = await _imageInfoRepository.SaveImage(outputStream).ConfigureAwait(false);
                            furnitureImage = new FurnitureImage()
                            {
                                PictureInfo = "https://fhistorage.blob.core.windows.net/furnitureimages/" + imageId,
                                FurnitureId = Convert.ToInt32(a.FurnitureId)
                            };
                        }
                        FurnitureImageList.Add(furnitureImage);
                    }
                    else
                    {
                        return BadRequest($"Image is: {a.image}. Image Length is: {a.image.Length}");
                    }
                }
                else
                {
                    return BadRequest($"ModelState is bad: {ModelState}");
                }
            }

            _imageInfoRepository.AddBulkFurnitureImages(FurnitureImageList);

            return Ok(FurnitureImageList);
        }
        [HttpPost("furniture/image")]
        public async Task<IActionResult> UploadFile(IFormFile image, int furnId)
        {
            int actFurnId;
            int furnitureId = Convert.ToInt32(RouteData.Values["furnitureId"]);

            if(furnitureId != 0)
            {
                actFurnId = furnitureId;   
            }
            else if(furnId != 0)
            {
                actFurnId = furnId;
            }
            else
            {
                return BadRequest("No furniture Id being passed. Please include furnitureId");
            }

            var furnitureImageCheck = _imageInfoRepository.GetImageByFurnitureId(actFurnId);

            if (furnitureImageCheck != null)
            {
                var newReg = new Regex(@"([^/]+$)");
                string strMatch = furnitureImageCheck.PictureInfo;
                string guid = newReg.Matches(strMatch)[0].Value;
                try
                {
                    _imageInfoRepository.DeleteImage(guid, furnitureImageCheck);
                }
                catch (Exception ex)
                {
                    return BadRequest("Error in image deletion: " + $"{ex}");
                }
            }

            var furnitureImage = new FurnitureImage();

            if (ModelState.IsValid)
            {
                if (image != null && image.Length > 0)
                {
                    using (Stream stream = image.OpenReadStream())
                    using (var outputStream = new MemoryStream())
                    using (var newImage = Image.Load(stream, new JpegDecoder()))
                    {
                        newImage.Mutate(x => x.AutoOrient());
                        newImage.SaveAsJpeg(outputStream);
                        outputStream.Position = 0;
                        var imageId = await _imageInfoRepository.SaveImage(outputStream).ConfigureAwait(false);
                        furnitureImage = new FurnitureImage()
                        {
                            PictureInfo = "https://fhistorage.blob.core.windows.net/furnitureimages/" + imageId,
                            FurnitureId = actFurnId
                        };
                    }
                    _imageInfoRepository.AddNewFurnitureImage(furnitureImage);
                }
                else
                {
                    return BadRequest($"Image is: {image}. Image Length is: {image.Length}");
                }
            }
            else
            {
                return BadRequest($"ModelState is bad: {ModelState}");
            }
            return Ok(furnitureImage);
        }
    }
}
