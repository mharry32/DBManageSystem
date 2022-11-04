using DBManageSystem.App.ViewModels;

namespace DBManageSystem.App.Views;

public partial class AddConnectionPage : ContentPage
{
	public AddConnectionPage()
	{
		InitializeComponent();
		BindingContext = new AddConnectionViewModel();

    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        var vm = BindingContext as AddConnectionViewModel;
        Console.WriteLine(vm.SelectedDBType.Name);
    }
}