using Asana.Library.Models;
using Asana.Maui.ViewModel;
namespace Asana.Maui.Views;

[QueryProperty(nameof(ToDoId), "toDoId")]
public partial class ToDoDetailView : ContentPage
{
	public ToDoDetailView()
	{
		InitializeComponent();
		BindingContext = new ToDoDetailViewModel();
	}

	public int ToDoId { get; set; }

	private void CancelClicked(object sender, EventArgs e)
	{
		Shell.Current.GoToAsync("//MainPage");
	}

	private void OkClicked(object sender, EventArgs e)
	{
		(BindingContext as ToDoDetailViewModel)?.AddOrUpdateToDo();
        Shell.Current.GoToAsync("//MainPage");
    }

    private void ContentPage_NavigatedFrom(object sender, NavigatedFromEventArgs e)
    {

    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
		BindingContext = new ToDoDetailViewModel(ToDoId);
    }
}