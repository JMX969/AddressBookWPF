using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AddressBookWPF
{
    public partial class AddContact : Window
    {
        private ContactClass contact;
        public AddContact(ContactClass contact)
        {
            InitializeComponent();
            this.contact = contact;
        }

        private void addC_Click(object sender, RoutedEventArgs e)
        {
            ContactClass contact1  = new ContactClass(); 
            contact1.ContactId = "0";
            contact1.firstName = tbFirstName.Text.ToString();
            contact1.lastName = tbLastName.Text;
            contact1.email = tbEmail.Text;
            contact1.city = tbCity.Text;
            contact1.street = tbStreet.Text;
            contact1.province = tbProvince.Text;
            contact1.zipCode= tbZipCode.Text;
            contact1.number = tbNumber.Text;
            databaseSQL.saveContact(contact1);
            
            this.Close();
        }

        private void cancelC_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void updateC_Click(object sender, RoutedEventArgs e)
        {
            //ContactClass contact1 = new ContactClass();
            //contact.ContactId = contact.id
            contact.firstName = tbFirstName.Text.ToString();
            contact.lastName = tbLastName.Text;
            contact.email = tbEmail.Text;
            contact.city = tbCity.Text;
            contact.street = tbStreet.Text;
            contact.province = tbProvince.Text;
            contact.zipCode = tbZipCode.Text;
            contact.number = tbNumber.Text;
            databaseSQL.updateContact(contact);

            this.Close();
        }
        public void buttonsEnabled()
        {
            addC.Visibility = Visibility.Hidden;
            updateC.Visibility = Visibility.Hidden;

        }
        public void setUpdateContact(ContactClass cc)
        {
            tbFirstName.Text = cc.firstName;
            tbLastName.Text = cc.lastName;
            tbEmail.Text = cc.email;
            tbNumber.Text = cc.number;
            tbCity.Text = cc.city;
            tbProvince.Text = cc.province;
            tbStreet.Text = cc.street;
            tbZipCode.Text = cc.zipCode;
        }
    }
}
