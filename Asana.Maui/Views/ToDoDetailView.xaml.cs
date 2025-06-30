using Asana.Library.Models;
using Asana.Maui.ViewModel;
namespace Asana.Maui.Views;

public partial class ToDoDetailView : ContentPage
{
	public ToDoDetailView()
	{
		InitializeComponent();
		BindingContext = new ToDoDetailViewModel();
	}

	private void CancelClicked(object sender, EventArgs e)
	{
		Shell.Current.GoToAsync("//MainPage");
	}

	private void OkClicked(object sender, EventArgs e)
	{
		(BindingContext as ToDoDetailViewModel)?.AddOrUpdateToDo();
        Shell.Current.GoToAsync("//MainPage");
    }
}