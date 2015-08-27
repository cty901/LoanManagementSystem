using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Doerit.SMSLib;

namespace Doerit.SMSLib
{
    public class SMSManager
    {
            Dictionary<string, HSPAModem> modems;
            List<SMSSender> senders=new List<SMSSender>();
            SMS s;
            List<int> contacts = new List<int>();

            public SMSManager()
            {
                setModems();
            }

            public void setModems()
            {
                Doerit.SMSLib.DoerSMSDeviceManager DeviceManager = new Doerit.SMSLib.DoerSMSDeviceManager();
                this.modems = DeviceManager.getData();
            }

            public void setSenders()
            {
                ContactManager cm = new ContactManager();
                contacts = cm.getContactList();
                
                foreach (var modem in modems)
                {
                    if (modem.Value.Network=="Dialog")
                    {
                        HSPAModem hm = modem.Value;
                        SMSSender ss = new SMSSender(contacts, hm, s);
                        this.senders.Add(ss);
                        ss.set("Dialog", 12000, new byte[] { 127, 0, 0, 2 },"127.0.0.2");
                    }
                }

            }

            public void setSMS(DateTime dt,string content)
            {
                this.s = new SMS();
                s.CreatedDataTime=dt;
                s.Content=content;
            }
    }
}
