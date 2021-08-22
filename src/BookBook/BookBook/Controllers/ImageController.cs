using BookBook.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookBook.Controllers
{
    [ApiController]
    [Route("api/image")]
    public class ImageController : ControllerBase
    {
        private readonly IBlobService blobService;

        public ImageController(IBlobService blobService)
        {
            this.blobService = blobService;
        }

        [HttpPost]
        public ActionResult UploadImage(IFormFile file)
        {
            try
            {
                var create = blobService.CreateBlobAsyns(file);
                create.Wait();
                return Ok(create.Result);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public IEnumerable<FileContentResult> GetImages(IEnumerable<Guid> ids)
        {
            foreach (var id in ids)
            {
                var stream = blobService.GetBlobAsync(id.ToString());
                stream.Wait();
                if (stream.IsCompletedSuccessfully)
                    yield return new FileContentResult(stream.Result, "image/png");
            }
        }

        [HttpGet("{id}")]
        public ActionResult GetImage(Guid id)
        {
            var stream = blobService.GetBlobAsync(id.ToString());
            stream.Wait();
            if (stream.IsCompletedSuccessfully)
                return new FileContentResult(stream.Result, "image/png");
            return BadRequest();
        }
    }

}
