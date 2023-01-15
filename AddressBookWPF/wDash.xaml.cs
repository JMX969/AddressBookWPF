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
    /// Interaction logic for wDash.xaml
    /// </summary>
    public partial class wDash : Window
    {
        private cDash dash;
        public wDash()
        {
            InitializeComponent();
            dash= new cDash();
            dash.setLists();
            grdName.ItemsSource = dash.lstContacts;
            
            DataContext= dash;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void grdName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (grdName.SelectedItem == null)
                return;
            ContactClass selContact = grdName.SelectedItem as ContactClass;
            dash.setSelectedContactGr(int.Parse(selContact.ContactId));
            grdList.ItemsSource = null;
            grdList.ItemsSource = dash.selectedContactGr;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            gridContact.Visibility = Visibility.Visible;
            clearGridSource();
            dash.setLists();
            grdName.ItemsSource= dash.lstContacts;
            grdList.ItemsSource= dash.lstGroup;
        }

        private void grdList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (grdList.SelectedItem == null)
                return;
            ContactGroup selGroup= grdList.SelectedItem as ContactGroup;
            dash.setSelectedGroupCn(int.Parse(selGroup.groupId));
            grdName.ItemsSource = null;
           grdName.ItemsSource= dash.selectedGroupCn;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            gridContact.Visibility = Visibility.Visible;
            clearGridSource();
            dash.setLists();
            grdName.ItemsSource = dash.lstContacts;
            grdList.ItemsSource = dash.lstGroup;
        }
        public void clearGridSource()
        {
            grdName.ItemsSource = null;
            grdList.ItemsSource = null;
        }

        private void imgAdd_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            
        }
        private void refreshData()
        {

            dash.setLists();
            grdName.ItemsSource = null;
            grdName.ItemsSource = dash.lstContacts;
            grdList.ItemsSource = null;           
            grdList.ItemsSource = dash.lstGroup;
        }
        private void imgAdd_MouseUp(object sender, MouseButtonEventArgs e)
        {
            AddContact ac = new AddContact(grdName.SelectedItem as ContactClass);
            ac.crudContact.Text = "Add Contact";
            ac.buttonsEnabled();
            ac.addC.Visibility = Visibility.Visible;
            ac.ShowDialog();
            refreshData();
        }

        private void imgEdit_MouseUp(object sender, MouseButtonEventArgs e)
        {
            AddContact ac = new AddContact(grdName.SelectedItem as ContactClass);
            ac.crudContact.Text = "Update Contact";
            ContactClass cc = grdName.SelectedItem as ContactClass;
            ac.setUpdateContact(cc);
            ac.buttonsEnabled();
            ac.updateC.Visibility = Visibility.Visible;
            ac.ShowDialog();
            refreshData();
        }

        private void imgDel_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ContactClass cc = grdName.SelectedItem as ContactClass;
            try
            {
                if (cc.ContactId != null)
                    dash.deleteContact(cc);
                refreshData();
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }

        private void imgGroup_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //ContactGroup cg = new ContactGroup();
            //cg = grdName.SelectedItem as ContactGroup;
            //AddContactToGroups actg = new AddContactToGroups(cg);
            //actg.crudContact.Text = "Add Contact to Groups";

            //actg.ShowDialog();
            //refreshData();
            ContactClass cc = new ContactClass();
            //ContactGroup cg = grdName.SelectedItem as ContactGroup;
            cc = grdName.SelectedItem as ContactClass;
            addGroupsToContact agtc = new addGroupsToContact(cc);

            agtc.ShowDialog();
            refreshData();
        }


        private void imgEditGroup_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ContactGroup cg = grdList.SelectedItem as ContactGroup;
            addGroup ag = new addGroup(cg);
            ag.crudContact.Text = "Update Group";
            ag.buttonsEnabled();
            ag.updG.Visibility = Visibility.Visible;
            ag.ShowDialog();
            refreshData();
        }

        private void imgAddGroup_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ContactGroup cg = new ContactGroup();
            addGroup ag = new addGroup(cg);
            ag.crudContact.Text = "Add Group";
            ag.buttonsEnabled();
            ag.addG.Visibility = Visibility.Visible;
            ag.textGroupId.IsEnabled = false;
            ag.tbGroupID.IsEnabled= false;
            ag.ShowDialog();
            refreshData();
        }

        private void imgDelGroup_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //if(MessageBox.Show())
            MessageBox.Show("Are you sure you want to delete?");
            ContactGroup cg = new ContactGroup();
            cg = grdList.SelectedItem as ContactGroup;
            databaseSQL.deleteGroup(cg);

        }

        private void imgContactGroup_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ContactGroup cg = new ContactGroup();
            cg = grdList.SelectedItem as ContactGroup;
            AddContactToGroups actg = new AddContactToGroups(cg);
            actg.crudContact.Text = "Add Contacts to Group";
            actg.loadContactsInGroup(cg);
            actg.setGroupDetails(cg);
            actg.ShowDialog();
            refreshData();
        }
    }
}
