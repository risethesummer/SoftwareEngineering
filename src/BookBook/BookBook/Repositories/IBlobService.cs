using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BookBook.Repositories
{
    public interface IBlobService
    {
        public Task<byte[]> GetBlobAsync(string name);
        public Task<Guid> CreateBlobAsyns(IFormFile file);
    }
}
