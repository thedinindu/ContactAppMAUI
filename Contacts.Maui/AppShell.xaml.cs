﻿using Contacts.Maui.Views;

namespace Contacts.Maui;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(ContactMenue), typeof(ContactMenue));
		Routing.RegisterRoute(nameof(AddContactPage), typeof(AddContactPage));
		Routing.RegisterRoute(nameof(EditContactPage), typeof(EditContactPage));
	}
}
