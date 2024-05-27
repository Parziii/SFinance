using SFinance.Services;

namespace SFinance
{
    public partial class App : Application
	{

		public App(ReceiptService receiptService)
		{
			InitializeComponent();

			MainPage = new NavigationPage(new MainPage(receiptService));
		}
	}
}
