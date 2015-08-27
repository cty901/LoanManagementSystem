using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doerit.SMSLib
{
    public class ContactManager
    {
        List<int> contact_list = new List<int>();

        public List<int> getContactList()
        {
            contact_list.Add(0712732233);
            contact_list.Add(0717410955);

            return contact_list;
        }
    }
}
