﻿namespace MAUIBlazorApp.Views;

public partial class MainPage : ContentPage
{
	public MainPage(TimesheetsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}

