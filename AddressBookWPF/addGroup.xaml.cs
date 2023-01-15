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
    public partial class addGroup : Window
    {
        public ContactGroup cg { get; set; }
        public addGroup(ContactGroup cg)
        {
            InitializeComponent();
            this.cg = cg;
            setGroupDetails();
        }
        public void buttonsEnabled()
        {
            addG.Visibility = Visibility.Hidden;
            updG.Visibility = Visibility.Hidden;

        }
        public void setGroupDetails()
        {
            tbGroupID.Text = cg.groupId;
            tbGroupName.Text = cg.groupName;
            tbGroupDescription.Text = cg.groupDescription;
        }
        public ContactGroup getDetails()
        {
            
            cg.groupId = tbGroupID.Text;
            cg.groupName = tbGroupName.Text;
            cg.groupDescription = tbGroupDescription.Text;

            return cg;
        }
        private void addG_Click(object sender, RoutedEventArgs e)
        {
            //cDash cd = new cDash();
            //cd.addGroup(cg);
            databaseSQL.saveGroup(getDetails());
            this.Close();
        }
        
        private void updG_Click(object sender, RoutedEventArgs e)
        {

            databaseSQL.updateGroup(getDetails());
            this.Close();
        }

        private void cancelC_Click(object sender, RoutedEventArgs e)
        {
            databaseSQL.deleteGroup(cg);
            this.Close();
        }
    }
}
