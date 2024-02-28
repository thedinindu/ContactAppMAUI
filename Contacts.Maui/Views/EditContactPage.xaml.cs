using Contacts.Maui.Models;
using Contact = Contacts.Maui.Models.Contact;

namespace Contacts.Maui.Views;

[QueryProperty(nameof(ContactId), "Id")]
public partial class EditContactPage : ContentPage
{
	private Contact contact;
	public EditContactPage()
	{
		InitializeComponent();
	}

    private void btnCancel_Clicked(object sender, EventArgs e)
    {
		Shell.Current.GoToAsync($"//{nameof(ContactMenue)}");
    }

	public string ContactId
	{
		set
		{ 
			contact = ContactRepository.GetContactByContactId(Convert.ToInt32(value));
			
			if (contact != null)
			{
				txtName.Text = contact.Name;
				txtEmail.Text = contact.Email;
				txtPhone.Text = contact.Phone;
				txtAddress.Text = contact.Address;
			}
		}
	}

    private void btnUpdate_Clicked(object sender, EventArgs e)
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

		contact.Name = txtName.Text;
		contact.Email = txtEmail.Text;
		contact.Address = txtAddress.Text;
		contact.Phone = txtPhone.Text;
		contact.IsActive = true;

		ContactRepository.UpdateContact(contact.ContactId, contact);
		Shell.Current.GoToAsync($"//{nameof(ContactMenue)}");

		//if (result)
		//{
  //      }
    }
}