using System.ComponentModel.DataAnnotations;

namespace SFinanceAPI.DbContext.Models
{
	public class ImageMetadata
	{
		[Key]
		public Guid Id { get; set; }
		public string FileName { get; set; }
		public string ContentType { get; set; }
		public long Size { get; set; }
		public string FilePath { get; set; }
		public DateTime UploadedAt { get; set; }
	}
}
