using DBManageSystem.App.ViewModels;

namespace DBManageSystem.App.Views;

public partial class AddConnectionPage : ContentPage
{
	public AddConnectionPage()
	{
		InitializeComponent();
		BindingContext = new AddConnectionViewModel();

    }
}