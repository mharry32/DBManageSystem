using DBManageSystem.App.ViewModels;
using DBManageSystem.Core.Interfaces;

namespace DBManageSystem.App.Views;

public partial class AddConnectionPage : ContentPage
{
	public AddConnectionPage(AddConnectionViewModel vm)
    { 
        InitializeComponent();
        BindingContext = vm;
    }
}