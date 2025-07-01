﻿using Asana.Maui.ViewModel;
namespace Asana.Maui;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        BindingContext = new MainPageViewModel();
    }

    private void AddNewClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//ToDoDetails");
    }

    private void ManageProjectsClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//ProjectPage");
    }
    private void EditClicked(object sender, EventArgs e)
    {
        var selectedId = (BindingContext as MainPageViewModel)?.SelectedToDo.Id ?? 0;
            Shell.Current.GoToAsync($"//ToDoDetails?toDoId={selectedId}");
    }
    private void DeleteClicked(object sender, EventArgs e)
    {
        (BindingContext as MainPageViewModel)?.DeleteToDo();
    }
    private void ContentPage_NavigatedFrom(object sender, NavigatedFromEventArgs e)
    {
        
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        (BindingContext as MainPageViewModel)?.RefreshPage();
    }

    
}
