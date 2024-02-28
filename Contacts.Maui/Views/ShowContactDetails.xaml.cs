using CommunityToolkit.Maui.Views;
using Contact = Contacts.Maui.Models.Contact;

namespace Contacts.Maui.Views;

public partial class ShowContactDetails : Popup
{
	public ShowContactDetails(Contact contact)
	{
		InitializeComponent();
		ShowDetails(contact);

	}

	private void ShowDetails(Contact contact)
	{
		txtName.Text = "Name : " + contact.Name;
		txtPhone.Text = "Phone : " + contact.Phone;
		txtEmail.Text = "Email : " + contact.Email;
		txtAddress.Text = "Address : " + contact.Address;
	}
}