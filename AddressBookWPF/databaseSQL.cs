
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using AddressBookWPF;
using System.Configuration;
using System.Reflection;


namespace AddressBookWPF
{
    internal class databaseSQL
    {      
        public static List<ContactClass> loadContacts()
        {
            //Microsoft.Data.Sqlite mysql = new Microsoft.Data.Sqlite;

            using (IDbConnection conn = new SQLiteConnection(loadConnectionString()))
            {
                var output = conn.Query<ContactClass>("Select * from CONTACT");
                return output.ToList();
            }
        }
        public static List<ContactGroup> loadGroups()
        {
            using (IDbConnection conn = new SQLiteConnection(loadConnectionString()))
            {
                var output = conn.Query<ContactGroup>("Select * from ADDRESSGROUP");
                return output.ToList();
            }
        }
        public static void saveContact(ContactClass contact)
        {
            using (IDbConnection conn = new SQLiteConnection(loadConnectionString()))
            {
                conn.Execute("Insert into CONTACT (firstName, lastName, number, email, city, province, street, zipCode) values (@firstName, @lastName, @number, @email, @city, @province, @street, @zipCode)", contact);
            }
        }
        public static void saveGroup(ContactGroup group)
        {
            using (IDbConnection conn = new SQLiteConnection(loadConnectionString()))
            {
                conn.Execute("Insert into ADDRESSGROUP (groupName, groupDescription) values (@groupName, @groupDescription)", group);
            }
        }
        public static void updateContact(ContactClass contact)
        {
            using (IDbConnection conn = new SQLiteConnection(loadConnectionString()))
            {
                conn.Execute("Update CONTACT Set firstName = @firstName, lastName = @lastName, number = @number, email = @email, city = @city, province = @province, street = @street, zipCode = @zipCode where contactID = @ContactId", contact);
            }
        }
        public static void updateGroup(ContactGroup group)
        {
            using (IDbConnection conn = new SQLiteConnection(loadConnectionString()))
            {
                conn.Execute("Update ADDRESSGROUP Set groupName = @groupName, groupDescription = @groupDescription where groupID = @groupId", group);
            }
        }

        public static List<GroupList> loadContactsInGroups(string id)
        {
            using (IDbConnection conn = new SQLiteConnection(loadConnectionString()))
            {
                var output = conn.Query<GroupList>("Select * from GROUPCONTACT where groupId = " + id + "");
                return output.ToList();
            }
        }
        public static List<ContactGroup> loadGroupsFromContact(int id)
        {
            using (IDbConnection conn = new SQLiteConnection(loadConnectionString()))
            {
                var output = conn.Query<ContactGroup>("SELECT ADDRESSGROUP.groupID,ADDRESSGROUP.groupName, "+
                    "ADDRESSGROUP.groupDescription" +
                    " FROM ADDRESSGROUP " + 
                    "INNER JOIN GROUPCONTACT ON ADDRESSGROUP.groupID = GROUPCONTACT.groupID " + 
                    $"INNER JOIN CONTACT ON GROUPCONTACT.contactID = CONTACT.contactID WHERE(CONTACT.contactID = {id})");
                return output.ToList();
            }
        }
        public static List<ContactClass> loadContactsFromGroup(int id)
        {
            using (IDbConnection conn = new SQLiteConnection(loadConnectionString()))
            {
                var output = conn.Query<ContactClass>($"SELECT CONTACT.contactID, CONTACT.firstName, CONTACT.lastName, CONTACT.number, CONTACT.email, CONTACT.city, CONTACT.province, CONTACT.street, CONTACT.zipCode FROM CONTACT INNER JOIN GROUPCONTACT ON CONTACT.contactID = GROUPCONTACT.contactID INNER JOIN ADDRESSGROUP ON GROUPCONTACT.groupID = ADDRESSGROUP.groupID WHERE(ADDRESSGROUP.groupID = {id})");
                return output.ToList();
            }
        }

        public static void addContactsToGroup(GroupList group)
        {
            using (IDbConnection conn = new SQLiteConnection(loadConnectionString()))
            {
                //conn.Execute("Insert into GROUPCONTACT (groupId, contactID) values (@groupName, @groupDescription)", group);
                conn.Execute("Insert into GROUPCONTACT (groupId, contactID) values (@groupID, @contactID)", group);
            }
        }
        public static void delContactFromGroup(GroupList gl)
        {
            using (IDbConnection conn = new SQLiteConnection(loadConnectionString()))
            {
                conn.Execute("Delete from GROUPCONTACT where contactID = " + gl.contactID + " AND groupID = "+ gl.groupID +"");
            }
        }
        public static void deleteContact(ContactClass cc)
        {
            using (IDbConnection conn = new SQLiteConnection(loadConnectionString()))
            {
                conn.Execute("Delete from CONTACT where contactID = " + cc.ContactId + "");
            }
        }
        public static void deleteGroup(ContactGroup group)
        {
            using (IDbConnection conn = new SQLiteConnection(loadConnectionString()))
            {
                conn.Execute("Delete from ADDRESSGROUP where groupID = " + group.groupId + "");
                conn.Execute("Delete from GROUPCONTACT where groupID = " + group.groupId + "");
            }
        }
        public static List<ContactClass> viewContactsInGroup(GroupList gl)
        {
            using (IDbConnection conn = new SQLiteConnection(loadConnectionString()))
            {
                List<ContactClass> cc = new List<ContactClass>();
                var output = conn.Query<GroupList>("Select * from GROUPCONTACT where groupId = " + gl.groupID + "");
                var output2 = output;
                foreach (var contactID in output)
                {
                    //var output3 = conn.Query<ContactClass>("Select * from CONTACT where contactID = " + contactID + "");
                    //cc.Add(output3);
                    //cc.Add(conn.Query<ContactClass>("Select * from CONTACT where contactID = " + contactID + ""));
                }
                
                return null;
            }
        }

        private static string loadConnectionString()
        {
            
            return ConfigurationManager.ConnectionStrings["DefaultConn"].ConnectionString;
        }
    }
}
