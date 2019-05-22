using FHIStorage.API.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FHIStorage.API.Services;

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

            return Ok(imageUri);
        }
        [HttpPost("furniture/image/{furnitureId}")]
        public async Task<IActionResult> UploadFile(IFormFile image)
        {
            int furnitureId = Convert.ToInt32(RouteData.Values["furnitureId"]);
            var furnitureImage = new FurnitureImage();
            if (image.ContentType != "image/jpg")
            {
                return BadRequest("This endpoint only excepts jpg file type..");
            }
            if (ModelState.IsValid)
            {
                if (image != null && image.Length > 0)
                {
                    using (Stream stream = image.OpenReadStream())
                    {
                        var imageId = await _imageInfoRepository.SaveImage(stream).ConfigureAwait(false);
                        furnitureImage = new FurnitureImage()
                        {
                            PictureInfo = imageId,
                            FurnitureId = furnitureId
                        };
                    }
                }
            }
            _imageInfoRepository.AddNewFurnitureImage(furnitureImage);

            return Ok(furnitureImage);
        }
    }
}
