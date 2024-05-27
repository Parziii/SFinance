using Microsoft.EntityFrameworkCore;
using SFinanceAPI.DbContext.Models;
using Mindee;
using Mindee.Input;
using Mindee.Product.Receipt;

namespace SFinanceAPI.DbContext
{
	public class SFinanceContext : Microsoft.EntityFrameworkCore.DbContext
	{
		public SFinanceContext(DbContextOptions<SFinanceContext> options)
			: base(options)
		{
		}

		public DbSet<ReceiptV5Document> Receipts { get; set; }

		public DbSet<ImageMetadata> ImageMetadata { get; set; }
	}
}
