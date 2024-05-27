using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFinance.Services;

namespace SFinance
{
    public partial class TimePage : ContentPage
	{

		private readonly ReceiptService _receiptService;
		public TimePage(ReceiptService receiptService)
		{
			_receiptService = receiptService;
			InitializeComponent();
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			DisplayCurrentTime();
		}

		private void DisplayCurrentTime()
		{
			currentTimeLabel.Text = DateTime.Now.ToString("T");
		}

		private async Task<string> DisplayReceiptStatus()
		{
			var recepitStatus = await _receiptService.GetReceiptStatusAsync();
			return recepitStatus;
		}

		private async void OnCheckReceiptStatusButtonClicked(object sender, EventArgs e)
		{
			var receiptStatus = await _receiptService.GetReceiptStatusAsync();
			currentRecepitLabel.Text = receiptStatus;
		}

	}
}
