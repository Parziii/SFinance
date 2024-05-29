namespace SFinanceAPI.Services.Models
{
	public class OpenAiResponse
	{
		public Choice[]? Choices { get; set; }
	}

	public class Choice
	{
		public Message? Message { get; set; }
	}

	public class Message
	{
		public string? Content { get; set; }
	}
}
