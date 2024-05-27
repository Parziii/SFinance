using SFinance.Services;

namespace SFinance
{
    public partial class MainPage : ContentPage
	{
		int count = 0;
		private readonly ReceiptService _receiptService;

		public MainPage(ReceiptService receiptService)
		{
			_receiptService = receiptService;
			InitializeComponent();
		}

		private void OnCounterClicked(object sender, EventArgs e)
		{
			count++;

			if (count == 1)
				CounterBtn.Text = $"Clicked {count} time";
			else
				CounterBtn.Text = $"Clicked {count} times";

			SemanticScreenReader.Announce(CounterBtn.Text);
		}

		private void OnTimePageButtonClicked(object sender, EventArgs e)
		{
			Navigation.PushAsync(new TimePage(_receiptService));
		}

	}

}
