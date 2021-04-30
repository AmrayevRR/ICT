using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Example2
{
    public partial class ShowContactForm : Form
    {
        public bool deleteMode = false;
        public string contactId;
        public Form1 generalFormDelegate;
        public ShowContactForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            generalFormDelegate.DeleteContactById(contactId);
            DialogResult = DialogResult.Cancel;
        }

        EditContactForm editContactForm = new EditContactForm();
        private void button3_Click(object sender, EventArgs e)
        {
            editContactForm.generalFormDelegate = generalFormDelegate;
            editContactForm.contactId = contactId;
            editContactForm.nameTxtBx.Text = nameLabel.Text;
            editContactForm.phoneTxtBx.Text = phoneLabel.Text;
            editContactForm.addressTxtBx.Text = addressLabel.Text;

            if (editContactForm.ShowDialog() == DialogResult.OK)
            {
                nameLabel.Text = editContactForm.nameTxtBx.Text;
                phoneLabel.Text = editContactForm.phoneTxtBx.Text;
                addressLabel.Text = editContactForm.addressTxtBx.Text;
            }
        }
    }
}
