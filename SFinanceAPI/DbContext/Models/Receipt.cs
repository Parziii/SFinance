using System.ComponentModel.DataAnnotations;
using Mindee.Product.Receipt;

namespace SFinanceAPI.DbContext.Models
{
	public class Receipt
	{
		[Key]
		public Guid Id { get; set; }
		public string? StoreName { get; set; }
		public string? StoreAddress { get; set; }
		public string? StoreNIP { get; set; }
		public DateTime Date { get; set; }
		public string? Cashier { get; set; }
		public List<ReceiptItem> Items { get; set; }
		public decimal? TotalAmount { get; set; }
		public string? PaymentMethod { get; set; }
		public string? ReceiptNumber { get; set; }

		public Receipt()
		{
			Id = Guid.NewGuid();
			Items = new List<ReceiptItem>();
		}
	}

	public class ReceiptItem
	{
		[Key]
		public Guid Id { get; set; }
		public string? Name { get; set; }
		public string? Unit { get; set; }
		public decimal? Quantity { get; set; }
		public decimal? UnitPrice { get; set; }
		public decimal? TotalPrice { get; set; }
		public string? TaxCode { get; set; }

		ReceiptItem()
		{
			Id = Guid.NewGuid();
		}
	}
}
