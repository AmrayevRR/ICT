using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PagedList;

namespace Example2
{
    interface IDelete
    {
        bool DeleteContactById(string id);
    }
    interface IEdit
    {
        void EditContactById(string id, string name, string phone, string address);
    }
    public partial class Form1 : Form, IDelete, IEdit
    {
        int pageNumber = 1;
        IPagedList<ContactDTO> contacts;
        public Form1()
        {

            InitializeComponent();

            LoadContacts();
        }

        BLL bll = default(BLL);

        private void LoadContacts()
        {

            ContactDBMock contacts = new ContactDBMock();
            ContactDB contacts2 = new ContactDB();

            bll = new BLL(contacts2);

            bindingSource1.DataSource = bll.GetContacts();


            bindingNavigator1.BindingSource = bindingSource1;
            dataGridView1.DataSource = bindingSource1;

            toolStripStatusLabel1.Text = bll.GetContacts().Count.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = bll.GetContacts().Count.ToString();
            bindingSource1.DataSource = bll.GetContacts();
        }

        CreateContactForm createContactForm = new CreateContactForm();

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (createContactForm.ShowDialog() == DialogResult.OK)
            {
                CreateContactCommand command = new CreateContactCommand();
                command.Name = createContactForm.nameTxtBx.Text;
                command.Phone = createContactForm.phoneTxtBx.Text;
                command.Address = createContactForm.addressTxtBx.Text;
                bll.CreateContact(command);
                bindingSource1.DataSource = bll.GetContacts();

                createContactForm.nameTxtBx.Text = null;
                createContactForm.phoneTxtBx.Text = null;
                createContactForm.addressTxtBx.Text = null;
            }
        }

        ShowContactForm showContactForm = new ShowContactForm();
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            showContactForm.generalFormDelegate = this;
            showContactForm.contactId = bll.GetContactByRowIndex(e.RowIndex).Id;
            showContactForm.nameLabel.Text = bll.GetContactByRowIndex(e.RowIndex).Name;
            showContactForm.phoneLabel.Text = bll.GetContactByRowIndex(e.RowIndex).Phone;
            showContactForm.addressLabel.Text = bll.GetContactByRowIndex(e.RowIndex).Address;

            showContactForm.ShowDialog();
        }

        public bool DeleteContactById(string id)
        {
            bll.DelecteContact(id);
            bindingSource1.DataSource = bll.GetContacts();
            return false;
        }

        public void EditContactById(string id, string name, string phone, string address)
        {
            bll.EditContact(id, name, phone, address);
            bindingSource1.DataSource = bll.GetContacts();
        }

        private void toolStripTextBox1_TextChanged(object sender, EventArgs e)
        {
            bindingSource1.DataSource = bll.GetContacts(toolStripTextBox1.Text);
        }
    }
}
