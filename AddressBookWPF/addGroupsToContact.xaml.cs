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
    /// <summary>
    /// Interaction logic for addGroupsToContact.xaml
    /// </summary>
    public partial class addGroupsToContact : Window
    {
        private ContactClass contactC;
        public addGroupsToContact(ContactClass contactC)
        {
            InitializeComponent();
            this.contactC = contactC;
            loadGroups();
            grdList.ItemsSource = databaseSQL.loadGroups();
            setContactDetails();

        }
        public void setContactDetails()
        {
            tbContactId.Text = contactC.ContactId;
            tbFirstName.Text = contactC.firstName;
            tbLastName.Text = contactC.lastName;
        }

        private void addG_Click(object sender, RoutedEventArgs e)
        {
            GroupList gl = new GroupList();
            gl.contactID = tbContactId.Text;
            gl.groupID = tbGroupID.Text;
            databaseSQL.addContactsToGroup(gl);
            loadGroups();
        }

        private void removeGroup_Click(object sender, RoutedEventArgs e)
        {
            GroupList gl = new GroupList();
            gl.contactID = tbContactId.Text;
            gl.groupID = tbGroupID.Text;
            databaseSQL.delContactFromGroup(gl);
            loadGroups();
            
        }
        public void loadGroups()
        {
            grdGroupList.ItemsSource = databaseSQL.loadGroupsFromContact(int.Parse(contactC.ContactId));
        }
        private void cancelC_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
