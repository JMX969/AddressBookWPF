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
using static System.Data.Entity.Infrastructure.Design.Executor;

namespace AddressBookWPF
{
    /// <summary>
    /// Interaction logic for AddContactToGroups.xaml
    /// </summary>
    public partial class AddContactToGroups : Window
    {
        private ContactGroup contactG;
        public AddContactToGroups(ContactGroup contactG)
        {
            InitializeComponent();
            this.contactG = contactG;
            grdList.ItemsSource = databaseSQL.loadContacts();
            
        }
        public void loadContactsInGroup(ContactGroup cg)
        {
            contactList.ItemsSource = databaseSQL.loadContactsFromGroup(int.Parse(cg.groupId));
        }

        public void loadContacts()
        {
            contactList.ItemsSource = databaseSQL.loadContactsFromGroup(int.Parse(contactG.groupId));
        }
        public void setGroupDetails(ContactGroup cg)
        {
            tbGroupID.Text = cg.groupId;
            tbGroupName.Text = cg.groupName;
            tbGroupDescription.Text = cg.groupDescription;
        }

        private void addC_Click_1(object sender, RoutedEventArgs e)
        {
            ContactClass contact1 = new ContactClass();
            GroupList gl = new GroupList();
            gl.contactID = tbContactId.Text;
            gl.groupID = tbGroupID.Text;
            databaseSQL.addContactsToGroup(gl);
            loadContacts();
        }

        private void removeContact_Click(object sender, RoutedEventArgs e)
        {
            GroupList gl = new GroupList();
            gl.contactID = tbContactId.Text;
            gl.groupID = tbGroupID.Text;
            databaseSQL.delContactFromGroup(gl);
            loadContacts();
        }

        private void cancelC_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
