using SFinanceAPI.DbContext;
using SFinanceAPI.DbContext.Models;
using SFinanceAPI.Services.Interfaces;

namespace SFinanceAPI.Services;

public class FileStorageService : IFileStorageService
{
	private readonly SFinanceContext _context;
	private readonly string _imagesFolderPath;

	public FileStorageService(IWebHostEnvironment webHostEnvironment, SFinanceContext context)
	{
		_context = context;
		_imagesFolderPath = Path.Combine(webHostEnvironment.WebRootPath, "images");
		if (!Directory.Exists(_imagesFolderPath))
		{
			Directory.CreateDirectory(_imagesFolderPath);
		}
	}

	public async Task<ImageMetadata> StoreFileAsync(IFormFile file)
	{
		var filePath = Path.Combine(_imagesFolderPath, file.FileName);
		await using (var stream = new FileStream(filePath, FileMode.Create))
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

		_context.ImageMetadata.Add(imageMetadata);
		await _context.SaveChangesAsync();

		return imageMetadata;
	}
}