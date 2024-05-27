namespace SFinance.Services;

public class ReceiptService
{
	public static string BaseAddress =
		DeviceInfo.Platform == DevicePlatform.Android ? "https://192.168.0.60:5252" : "https://localhost:5252";

	private readonly HttpClient _client;


	public ReceiptService()
	{
#if DEBUG
		var handler = new HttpsClientHandlerService();
		_client = new HttpClient(handler.GetPlatformMessageHandler());
#else
            _client = new HttpClient();
#endif
	}

	public async Task<string> GetReceiptStatusAsync()
	{
		try
		{
			var response = await _client.GetAsync(BaseAddress + "/Receipt");
			return await response.Content.ReadAsStringAsync();
		}
		catch (Exception ex)
		{
			return ex.Message + ex.InnerException.Message;
		}
	}
}
