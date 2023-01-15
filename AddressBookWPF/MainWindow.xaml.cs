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
using System.Windows.Navigation;
using System.Windows.Shapes;
//
//
//Test AddressBookWPF version1
//This form is incomplete and still under development
//Startup project = wDash.xaml

namespace AddressBookWPF
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            clearCrudSelection();
        }

        private void ViewAll_Click(object sender, RoutedEventArgs e)
        {

            ContactList.ItemsSource = databaseSQL.loadContacts();
            GroupList.ItemsSource = databaseSQL.loadContactsInGroups("2");
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            ContactClass cc = ContactList.SelectedItem as ContactClass;
            try
            {
                if (cc.ContactId != null)
                databaseSQL.updateContact(cc);
                ContactList.ItemsSource = databaseSQL.loadContacts();
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }

        private void AddContact_Click(object sender, RoutedEventArgs e)
        {
            ContactClass cc = new ContactClass();
            try
            {
                cc.firstName= tbFirstName.Text;
                cc.lastName= tbLastName.Text;
                cc.number= tbNumber.Text;
                cc.email= tbEmail.Text;
                cc.city= tbCity.Text;
                cc.street = "";
                cc.province = "";
                cc.zipCode = "";

                databaseSQL.saveContact(cc);
                ContactList.ItemsSource = databaseSQL.loadContacts();
            }
            catch { }
        }

        private void DeleteContact_Click(object sender, RoutedEventArgs e)
        {
            ContactClass cc = ContactList.SelectedItem as ContactClass;
            try
            {
                if (cc.ContactId != null)
                    databaseSQL.deleteContact(cc);
                ContactList.ItemsSource = databaseSQL.loadContacts();
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }

        private void tbContactId_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void searchFName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void searchLName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void radUpdate_Click(object sender, RoutedEventArgs e)
        {
            clearCrudSelection();
            radUpdate.IsChecked= true;
            Update.IsEnabled= true;
            ContactList.IsEnabled = true;
        }

        private void radAdd_Click(object sender, RoutedEventArgs e)
        {
            clearCrudSelection();
            radAdd.IsChecked= true;
            AddContact.IsEnabled= true;
            ContactList.IsEnabled= false;
        }

        private void radDelete_Click(object sender, RoutedEventArgs e)
        {
            clearCrudSelection();
            radDelete.IsChecked= true;
            DeleteContact.IsEnabled= true;
            ContactList.IsEnabled = true;
        }

        private void clearCrudSelection()
        {
            radAdd.IsChecked = false;
            radUpdate.IsChecked = false;
            radDelete.IsChecked = false;
            Update.IsEnabled= false;
            AddContact.IsEnabled= false;
            DeleteContact.IsEnabled= false;
            ContactList.IsEnabled = true;
        }
        private void clearGroupSelection()
        {
            radGroupAdd.IsChecked= false;
            radGroupUpdate.IsChecked= false;
            radGroupDelete.IsChecked= false;
            AddGroup.IsEnabled= false;
            DeleteGroups.IsEnabled= false;
            UpdateGroup.IsEnabled= false;
            GroupList.IsEnabled= true;
        }

        private void DeleteGroups_Click(object sender, RoutedEventArgs e)
        {
            ContactGroup cg = GroupList.SelectedItem as ContactGroup;
            try
            {
                if (cg.groupId != null)
                    databaseSQL.deleteGroup(cg);
                ContactList.ItemsSource = databaseSQL.loadContacts();
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }

        private void UpdateGroup_Click(object sender, RoutedEventArgs e)
        {
            ContactGroup cg = GroupList.SelectedItem as ContactGroup;
            try
            {
                if (cg.groupId != null)
                    databaseSQL.updateGroup(cg);
                ContactList.ItemsSource = databaseSQL.loadContacts();
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }

        private void AddGroup_Click(object sender, RoutedEventArgs e)
        {
            ContactGroup cg = new ContactGroup();
            try
            {
                cg.groupName = tbGroupName.Text;
                cg.groupDescription = tbGroupdesc.Text;

                databaseSQL.saveGroup(cg);
                ContactList.ItemsSource = databaseSQL.loadContacts();
            }
            catch { }
        }

        private void ViewAllGroups_Click(object sender, RoutedEventArgs e)
        {
            GroupList.ItemsSource = databaseSQL.loadGroups();
        }

        private void radGroupDelete_Click(object sender, RoutedEventArgs e)
        {
            clearGroupSelection();
            DeleteGroups.IsEnabled= true;
            radGroupDelete.IsChecked = true;
        }

        private void radGroupAdd_Click(object sender, RoutedEventArgs e)
        {
            clearGroupSelection();
            GroupList.IsEnabled = false;
            AddGroup.IsEnabled = true;
            radGroupAdd.IsChecked = true;
        }

        private void radGroupUpdate_Click(object sender, RoutedEventArgs e)
        {
            clearGroupSelection();
            UpdateGroup.IsEnabled = true;
            radGroupUpdate.IsChecked = true;
        }

        private void ContactList_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var item = sender as ListViewItem;
            if (item != null && item.IsSelected)
            {
                List<GroupList> gl = new List<GroupList>();
                gl = databaseSQL.loadContactsInGroups("0");

                GroupList.ItemsSource = gl;

            }
            
        }

        private void GroupList_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var item = sender as ListViewItem;
            if (item != null && item.IsSelected)
            {

            }
        }

        private void addContactToGroup_Click(object sender, RoutedEventArgs e)
        {
            ContactClass cc = ContactList.SelectedItem as ContactClass;
            ContactGroup cg = GroupList.SelectedItem as ContactGroup;
            GroupList gl = new GroupList();
            try
            {
                if(cc.ContactId != null && cg.groupId!= null)
                {
                    gl.groupID = cg.groupId;
                    gl.contactID = cc.ContactId;
                    databaseSQL.addContactsToGroup(gl);
                    databaseSQL.loadContacts();
                }
            }catch (Exception ex) { MessageBox.Show("Make sure both contact and group are selected"); }
        }

    }
}
