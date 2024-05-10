using JoaoDiasDev.ProductList.Data.VO;

namespace JoaoDiasDev.ProductList.Business.Interfaces
{
    public interface IFileBusiness
    {
        public byte[] GetFile(string fileName);
        public Task<FileDetailVO> SaveFileToDisk(IFormFile file);
        public Task<List<FileDetailVO>> SaveFilesToDisk(IList<IFormFile> file);
    }
}
