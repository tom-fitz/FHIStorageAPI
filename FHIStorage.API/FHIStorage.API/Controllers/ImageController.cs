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
        [HttpPost("furniture/image/{furnitureId}")]
        public async Task<IActionResult> UploadFile(IFormFile image)
        {
            int furnitureId = Convert.ToInt32(RouteData.Values["furnitureId"]);

            var furnitureImageCheck = _imageInfoRepository.GetImageByFurnitureId(furnitureId);

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
                            FurnitureId = furnitureId
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
