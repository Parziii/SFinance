using SFinanceAPI.DbContext.Models;

namespace SFinanceAPI.Services.Interfaces
{
	public interface IFileStorageService
	{
		Task<ImageMetadata> StoreFileAsync(IFormFile file);
	}
}
