namespace Contacts.Maui.Views;

using Contacts.Maui.Models;
//using Contact = Contacts.Maui.Models.Contact;

public partial class AddContactPage : ContentPage
{
    public AddContactPage()
	{
		InitializeComponent();
	}

    private void btnCancel_Clicked(object sender, EventArgs e)
    {
		Shell.Current.GoToAsync($"//{nameof(ContactMenue)}");
    }

    private void btnCreate_Clicked(object sender, EventArgs e)
    {
        if (nameValidator.IsNotValid)
        {
            DisplayAlert("Error", "Name is required", "OK");
            return;
        }

        if (emailValidator.IsNotValid)
        {
            foreach (var error in emailValidator.Errors)
            {
                DisplayAlert("Error", error.ToString(), "OK");
            }

            return;
        }

        Contact contact = new Contact();

        contact.Name = txtName.Text;
        contact.Email = txtEmail.Text;
        contact.Address = txtAddress.Text;
        contact.Phone = txtPhone.Text;
        contact.IsActive = true;

        ContactRepository.CreateContact(contact);
        Shell.Current.GoToAsync($"//{nameof(ContactMenue)}");
    }
}