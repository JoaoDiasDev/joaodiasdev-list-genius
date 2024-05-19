using ListGenius.Shared.VO.File;
using Microsoft.AspNetCore.Http;

namespace ListGenius.Domain.Business.Interfaces
{
    public interface IFileBusiness
    {
        public byte[] GetFile(string fileName);
        public Task<FileDetailVO> SaveFileToDisk(IFormFile file);
        public Task<List<FileDetailVO>> SaveFilesToDisk(IList<IFormFile> file);
    }
}
