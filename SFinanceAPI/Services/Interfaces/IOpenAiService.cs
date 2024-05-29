using SFinanceAPI.DbContext.Models;

namespace SFinanceAPI.Services.Interfaces
{
	public interface IOpenAiService
	{
		Task<Receipt?> ProcessReceiptAsync(IFormFile file);
	}
}
