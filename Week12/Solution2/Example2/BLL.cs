using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;

namespace Example2
{
    interface DataAccessLayer
    {
        ContactDTO GetContactById(string id);
        string CreateContact(ContactDTO contact);
        bool DelecteContactById(string id);
        List<ContactDTO> GetAllContacts(bool sorted = false);
        List<ContactDTO> GetAllContacts(string name);
        ContactDTO GetContactByRowIndex(int rowIndex);
        void EditContactById(string id, string name, string phone, string address);
    }

    abstract class BaseContact
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }

    class CreateContactCommand : BaseContact
    {

    }

    class ContactDTO : BaseContact
    {
        public string Id { get; set; }
    }

    class BLL
    {
        DataAccessLayer dal = default(DataAccessLayer);
        public BLL(DataAccessLayer dal)
        {
            this.dal = dal;
        }
        public ContactDTO GetContact(string id)
        {
            return dal.GetContactById(id);
        }
        public string CreateContact(CreateContactCommand contact)
        {
            ContactDTO contact1 = new ContactDTO();
            contact1.Id = Guid.NewGuid().ToString();
            contact1.Name = contact.Name;
            contact1.Address = contact.Address;
            contact1.Phone = contact.Phone;
            return dal.CreateContact(contact1);
        }
        public bool DelecteContact(string id)
        {
            return dal.DelecteContactById(id);
        }

        public List<ContactDTO> GetContacts(bool sorted = false)
        {
            return dal.GetAllContacts(sorted);
        }
        public List<ContactDTO> GetContacts(string name)
        {
            return dal.GetAllContacts(name);
        }

        public void EditContact(string id, string name, string phone, string address)
        {
            dal.EditContactById(id, name, phone, address);
        }

        public ContactDTO GetContactByRowIndex(int rowIndex)
        {
            return dal.GetContactByRowIndex(rowIndex);
        }
    }
}