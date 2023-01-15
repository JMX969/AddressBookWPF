using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace AddressBookWPF
{
    public class cDash : INotifyPropertyChanged
    {
        private List<ContactClass> selLstContacts;
        private List<ContactGroup> selLstGroup;
        private List<ContactGroup> selSelectedContactGr;
        private List<ContactClass> selSelectedGroupCn;

        public List<ContactClass> lstContacts { get => selLstContacts; set { selLstContacts = value; OnPropertyChanged("lstContacts"); } }
        public List<ContactGroup> lstGroup { get => selLstGroup; set { selLstGroup = value; OnPropertyChanged("lstGroup"); } }
        public List<ContactGroup> selectedContactGr { get => selSelectedContactGr; set { selSelectedContactGr = value; OnPropertyChanged("selectedContactGr"); } }
        public List<ContactClass> selectedGroupCn { get => selSelectedGroupCn; set { selSelectedGroupCn = value; OnPropertyChanged("selectedGroupCn"); } }

        public void setLists()
        {            
            lstContacts = databaseSQL.loadContacts();
            lstGroup = databaseSQL.loadGroups();
        }
        public void setSelectedContactGr(int contactId)
        {
            selectedContactGr = databaseSQL.loadGroupsFromContact(contactId);
        }
        public void setSelectedGroupCn(int groupId)
        {
            selectedGroupCn = databaseSQL.loadContactsFromGroup(groupId);
        }
        public void addContact(ContactClass newContact)
        {
            databaseSQL.saveContact(newContact);
            setLists();
        }
        public void deleteContact(ContactClass delContact)
        {
            databaseSQL.deleteContact(delContact);
            setLists();
        }
        public void updateContact(ContactClass upContact)
        {
            databaseSQL.updateContact(upContact);
            setLists();
        }

        public void addGroup(ContactGroup addGroup)
        {
            databaseSQL.saveGroup(addGroup);
            setLists();
        }
        public void updateGroup(ContactGroup upGroup)
        {
            databaseSQL.updateGroup(upGroup);
            setLists();
        }
        public void deleteGroup(ContactGroup delGroup)
        {
            databaseSQL.deleteGroup(delGroup);
            setLists();
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
