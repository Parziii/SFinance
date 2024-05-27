using Microsoft.AspNetCore.Mvc;
using SFinanceAPI.DbContext.Models;

namespace SFinanceAPI.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class ReceiptController : Controller
	{
		private readonly IWebHostEnvironment _env;
		private readonly string _imagesFolderPath;

		public ReceiptController(IWebHostEnvironment env)
		{
			_env = env;
			_imagesFolderPath = Path.Combine(_env.WebRootPath, "images");
			if (!Directory.Exists(_imagesFolderPath))
			{
				Directory.CreateDirectory(_imagesFolderPath);
			}
		}

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			await Task.Delay(0); // Placeholder for non-blocking API calls or CPU-bound work
			return Ok("true");
		}

		[HttpPost]
		[Route("upload")]
		public async Task<IActionResult> UploadImage(IFormFile file)
		{
			if (file == null || file.Length == 0)
				return BadRequest("No file uploaded.");

			var filePath = Path.Combine(_imagesFolderPath, file.FileName);

			using (var stream = new FileStream(filePath, FileMode.Create))
			{
				await file.CopyToAsync(stream);
			}

			var imageMetadata = new ImageMetadata
			{
				FileName = file.FileName,
				ContentType = file.ContentType,
				Size = file.Length,
				FilePath = filePath,
				UploadedAt = DateTime.UtcNow
			};

			// Save imageMetadata to the database (code not shown)

			return Ok(new { imageMetadata.Id, imageMetadata.FileName, imageMetadata.ContentType, imageMetadata.Size });
		}


	}
}
