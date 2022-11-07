namespace DBManageSystem.App.Views;

public partial class ConnectionMangePage : ContentPage
{
	public ConnectionMangePage(ViewModels.ConnectionManageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

    protected override async void OnAppearing()
    {
		var vm = BindingContext as ViewModels.ConnectionManageViewModel;
		await vm.Initial();
    }
}