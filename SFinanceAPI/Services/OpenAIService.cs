using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using SFinanceAPI.DbContext;
using SFinanceAPI.DbContext.Models;
using SFinanceAPI.Services.Interfaces;
using SFinanceAPI.Services.Models;

namespace SFinanceAPI.Services;

public class OpenAiService : IOpenAiService
{
	private readonly HttpClient _client;
	private readonly string? _apiKey;
	private readonly string? _apiUrl;
	private readonly SFinanceContext _context;

	public OpenAiService(IConfiguration configuration, SFinanceContext context)
	{
		_client = new HttpClient();
		_apiKey = configuration["OpenAI:ApiKey"];
		_apiUrl = configuration["OpenAI:Url"];
		_context = context;
	}

	public async Task<Receipt?> ProcessReceiptAsync(IFormFile file)
	{
		using var memoryStream = new MemoryStream();
		await file.CopyToAsync(memoryStream);
		var bytes = memoryStream.ToArray();
		var base64Image = Convert.ToBase64String(bytes);

		var payload = new
		{
			model = "gpt-4o",
			response_format = new { type = "json_object" },
			messages = new[]
			{
				new
				{
					role = "user",
					content = new object[]
					{
						new
						{
							type = "text",
							text = "Where was the receipt made and what are the items with prices?"
						},
						new
						{
							type = "image_url",
							image_url = new
							{
								url = $"data:image/jpeg;base64,{base64Image}",
								detail = "high"
							}
						}
					}
				},
				new
				{
					role = "system",
					content = new object[]
					{
						new
						{
							type = "text",
							text =
								"You are a helpful assistant designed to output JSON. You need to analyze Receipt that are send as images and the response you send should look like this model:"
								+
								"public class Receipt\r\n\t{\r\n\t\tpublic string? StoreName { get; set; }\r\n\t\tpublic string? StoreAddress { get; set; }\r\n\t\tpublic string? StoreNIP { get; set; }\r\n\t\tpublic DateTime Date { get; set; }\r\n\t\tpublic string? Cashier { get; set; }\r\n\t\tpublic List<ReceiptItem> Items { get; set; }\r\n\t\tpublic decimal? TotalAmount { get; set; }\r\n\t\tpublic string? PaymentMethod { get; set; }\r\n\t\tpublic string? ReceiptNumber { get; set; }\r\n\r\n\t\tpublic Receipt()\r\n\t\t{\r\n\t\t\tId = Guid.NewGuid();\r\n\t\t\tItems = new List<ReceiptItem>();\r\n\t\t}\r\n\t}\r\n\r\n\tpublic class ReceiptItem\r\n\t{\r\n\t\tpublic string? Name { get; set; }\r\n\t\tpublic string? Unit { get; set; }\r\n\t\tpublic decimal? Quantity { get; set; }\r\n\t\tpublic decimal? UnitPrice { get; set; }\r\n\t\tpublic decimal? TotalPrice { get; set; }\r\n\t\tpublic string? TaxCode { get; set; }\r\n\r\n\t\tReceiptItem()\r\n\t\t{\r\n\t\t\tId = Guid.NewGuid();\r\n\t\t}\r\n\t}"
						}
					}
				}
			},
			max_tokens = 4000
		};

		var jsonPayload = JsonConvert.SerializeObject(payload);

		_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);

		var request = new HttpRequestMessage(HttpMethod.Post, _apiUrl);
		request.Content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

		var response = await _client.SendAsync(request);
		response.EnsureSuccessStatusCode();

		var responseContent = await response.Content.ReadAsStringAsync();

		var responseObject = JsonConvert.DeserializeObject<OpenAiResponse>(responseContent);
		var receiptJson = responseObject?.Choices?[0].Message?.Content;

		if (receiptJson == null) return new Receipt();

		var receipt = JsonConvert.DeserializeObject<Receipt>(receiptJson);
		Console.WriteLine(responseContent);

		if (receipt == null) return new Receipt();

		_context.Receipts.Add(receipt);
		await _context.SaveChangesAsync();

		return receipt;
	}
}