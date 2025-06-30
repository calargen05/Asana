using Asana.Maui.ViewModel;
namespace Asana.Maui.Views;

public partial class ProjectsView : ContentPage
{
	public ProjectsView()
	{
		InitializeComponent();
		BindingContext = new ProjectsPageViewModel();
	}

	private void CancelClicked(object sender, EventArgs e)
	{
		Shell.Current.GoToAsync("//MainPage");
	}
}