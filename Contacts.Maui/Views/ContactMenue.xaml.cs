using CommunityToolkit.Maui.Views;
using Contacts.Maui.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using Contact = Contacts.Maui.Models.Contact;
namespace Contacts.Maui.Views;

public partial class ContactMenue : ContentPage
{
	public ContactMenue()
	{
		InitializeComponent();
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();

        LoadContacts();
    }

    private void contactList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
		if (contactList.SelectedItem != null)
		{
            this.ShowPopup(new ShowContactDetails((Contact)contactList.SelectedItem));
			//Shell.Current.GoToAsync($"{nameof(EditContactPage)}/?Id={((Contact)contactList.SelectedItem).ContactId}");
		}
    }

    private void contactList_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        contactList.SelectedItem = null;
    }

    private void btnAddContact_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync($"{nameof(AddContactPage)}");
    }

    private void btnDeleteContact_Clicked(object sender, EventArgs e)
    {
        var menuItem = sender as MenuItem;
        var contact = menuItem.CommandParameter as Contact;
        ContactRepository.DeleteContact(contact.ContactId);

        LoadContacts();
    }

    private void LoadContacts()
    {
        // ObservableCollection is used to notify the listview that the data has been updated, otherwise it will keep the updated data in the memory but doesn't notify the listview 
        var contacts = new ObservableCollection<Contact>(ContactRepository.GetAllContacts());
        contactList.ItemsSource = contacts;
    }

    private void btnEditContact_Clicked(object sender, EventArgs e)
    {
        var menuItem = sender as MenuItem;
        var contact = menuItem.CommandParameter as Contact;

        if (contact != null)
        {
            Shell.Current.GoToAsync($"{nameof(EditContactPage)}/?Id={contact.ContactId}");
        }
    }
}