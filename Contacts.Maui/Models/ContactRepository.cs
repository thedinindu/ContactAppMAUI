using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Maui.Models
{
    public static class ContactRepository
    {
        //public static List<Contact> contacts = new List<Contact>()
        //{
        //    new Contact { ContactId = 1, Name = "Thedinindu Dilmin", Email = "thedinindu@gmail.com" },
        //    new Contact { ContactId = 2, Name = "Joh Doe", Email = "johndoe@gmail.com" },
        //    new Contact { ContactId = 3, Name = "Chamod Devinda", Email = "chamoddevinda@gmail.com" },
        //    new Contact { ContactId = 4, Name = "Ashfan NDM", Email = "sajandm@gmail.com" },
        //    new Contact { ContactId = 5, Name = "Dilum Chamara", Email = "dilumchamara@gmail.com" }
        //};

        //public static List<Contact> GetContacts => contacts;

        //public static Contact GetContactById(int contactId)
        //{
        //    return contacts.FirstOrDefault(x => x.ContactId == contactId);
        //}

        public static List<Contact> GetAllContacts()
        {
            HttpClient client = new HttpClient();

            List<Contact> contacts = new List<Contact>();

            string baseUrl = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5191" : "http://localhost:5191";
            try
            {
                HttpResponseMessage response = client.GetAsync($"{baseUrl}/api/Contact").Result;
                if (response.IsSuccessStatusCode)
                {
                    string content = response.Content.ReadAsStringAsync().Result;
                    contacts = JsonConvert.DeserializeObject<List<Contact>>(content);
                }
            }
            catch (Exception ex)
            {
                //Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return contacts.OrderBy(x => x.Name).ToList();
        }

        public static Contact GetContactByContactId(int contactId)
        {
            HttpClient client = new HttpClient();

            Contact contact = new Contact();

            string baseUrl = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5191" : "http://localhost:5191";
            try
            {
                HttpResponseMessage response = client.GetAsync($"{baseUrl}/api/Contact/{contactId}").Result;
                if (response.IsSuccessStatusCode)
                {
                    string content = response.Content.ReadAsStringAsync().Result;
                    contact = JsonConvert.DeserializeObject<Contact>(content);
                }
            }
            catch (Exception ex)
            {
                //Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return contact;

        }

        public async static void UpdateContact(int contactId, Contact contact)
        {
            HttpClient client = new HttpClient();
            string baseUrl = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5191" : "http://localhost:5191";
            bool success = false;

            //if (contactId != contact.ContactId) return success;

            Contact contactToUpdate = GetContactByContactId(contactId);

            if (contactToUpdate != null)
            {
                try
                {
                    string json = JsonConvert.SerializeObject(contact);

                    HttpContent contactContent = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PutAsync($"{baseUrl}/api/Contact/{contactId}", contactContent);

                    success = response.IsSuccessStatusCode;
                }
                catch (Exception ex)
                {
                    //Debug.WriteLine(@"\tERROR {0}", ex.Message);
                }    
            }

            //return success;
        }

        public async static void CreateContact(Contact contact)
        {
            HttpClient client = new HttpClient();
            string baseUrl = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5191" : "http://localhost:5191";
            bool success = false;

            try
            {
                string json = JsonConvert.SerializeObject(contact);
                HttpContent contactContent = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync($"{baseUrl}/api/Contact", contactContent);

                success = response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                // Handle exception
                // Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            // Notify user or handle success/failure as needed
        }

        public async static Task<bool> DeleteContact(int contactId)
        {
            HttpClient client = new HttpClient();
            string baseUrl = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5191" : "http://localhost:5191";

            try
            {
                HttpResponseMessage response = await client.DeleteAsync($"{baseUrl}/api/Contact/{contactId}");

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                // Handle exception
                // Debug.WriteLine(@"\tERROR {0}", ex.Message);
                return false;
            }
        }

    }
}
