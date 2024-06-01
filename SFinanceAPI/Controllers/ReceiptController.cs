using Microsoft.AspNetCore.Mvc;
using SFinanceAPI.DbContext.Models;
using SFinanceAPI;
using SFinanceAPI.Services;
using SFinanceAPI.Services.Interfaces;

namespace SFinanceAPI.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class ReceiptController : Controller
	{
		private readonly IFileStorageService _fileStorageService;
		private readonly IOpenAiService _openAiService;

		public ReceiptController(IFileStorageService fileStorageService, IOpenAiService openAiService)
		{
			_fileStorageService = fileStorageService;
			_openAiService = openAiService;
		}

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			await Task.Delay(0);
			return Ok("true");
		}

		[HttpPost]
		[Route("upload")]
		public async Task<IActionResult> UploadImage(IFormFile file)
		{
			if (file == null || file.Length == 0)
				return BadRequest("No file uploaded.");

			try
			{
			var imageMetadata = await _fileStorageService.StoreFileAsync(file);
			if (imageMetadata == null)
				return BadRequest("Failed to store file.");

			var receipt = await _openAiService.ProcessReceiptAsync(file);
			if (receipt == null)
				return NotFound("Failed to process receipt.");

			return Ok(receipt?.Id);

			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}

		}


	}
}
