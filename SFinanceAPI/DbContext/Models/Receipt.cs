using System.ComponentModel.DataAnnotations;
using Mindee.Product.Receipt;

namespace SFinanceAPI.DbContext.Models
{
	public class Receipt : ReceiptV5Document
	{
		[Key]
		public Guid Id { get; set; }

	}
}
