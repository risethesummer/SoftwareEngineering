using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using BookBook.Repositories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BookBook.Data
{
    public class BlobService : IBlobService
    {
        private readonly BlobServiceClient blobServiceClient;

        public BlobService(BlobServiceClient blobServiceClient)
        {
            this.blobServiceClient = blobServiceClient;
        }

        public async Task<Guid> CreateBlobAsyns(IFormFile file)
        {
            try
            {
                var containerClient = blobServiceClient.GetBlobContainerClient("images");
                using Stream fileStream = file.OpenReadStream();
                var imgID = Guid.NewGuid();
                var uploadInfor = await containerClient.UploadBlobAsync(imgID.ToString(), fileStream);
                return imgID;
            }
            catch
            {
                return Guid.Empty;
            }
        }

        public async Task<byte[]> GetBlobAsync(string name)
        {
            try
            {
                var containerClient = blobServiceClient.GetBlobContainerClient("images");
                var blobClient = containerClient.GetBlobClient(name);
                var downloadInfor = await blobClient.DownloadAsync();
                using MemoryStream stream = new();
                await downloadInfor.Value.Content.CopyToAsync(stream);
                return stream.ToArray();
            }
            catch
            {
                return null;
            }
        }
    }
}
