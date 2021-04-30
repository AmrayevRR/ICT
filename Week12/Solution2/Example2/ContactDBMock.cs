using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example2
{
    class ContactDBMock : DataAccessLayer
    {
        List<ContactDTO> contacts = new List<ContactDTO>();
        public string CreateContact(ContactDTO contact)
        {
            contacts.Add(contact);
            return contact.Id;
        }

        public bool DelecteContactById(string id)
        {
            ContactDTO contact = contacts.Find(x => x.Id == id);
            if (contact != null)
            {
                contacts.Remove(contact);
                return true;
            }
            return false;
        }

        public void EditContactById(string id, string name, string phone, string address)
        {
            throw new NotImplementedException();
        }

        public List<ContactDTO> GetAllContacts()
        {
            return contacts;
        }

        public List<ContactDTO> GetAllContacts(string name)
        {
            throw new NotImplementedException();
        }

        public List<ContactDTO> GetAllContacts(bool sorted = false)
        {
            throw new NotImplementedException();
        }

        public ContactDTO GetContactById(string id)
        {
            return contacts.Find(x => x.Id == id);
        }

        public ContactDTO GetContactByRowIndex(int rowIndex)
        {
            return contacts.ElementAt(rowIndex);
        }
    }
}