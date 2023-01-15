using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AddressBookWPF
{
    public class ContactGroup
    {

        public string groupId { get; set; }
        public string groupName { get; set; }
        public string groupDescription { get; set; }



        List<ContactClass> contacts = new List<ContactClass>();

    }
}
