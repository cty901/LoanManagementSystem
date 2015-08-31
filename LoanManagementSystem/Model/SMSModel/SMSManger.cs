using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Doerit.SMSLib;

namespace LoanManagementSystem.Model.SMSModel
{
    public class SMSManager
    {
        private static SMSManager _instance;
        Dictionary<string, HSPAModem> modems;
        List<SMSSender> senders = new List<SMSSender>();
        HSPAModem WorkingModem;
        SMS s;
        Boolean _SMSMangerStatus=false;

        private SMSManager()
        {

        }

        public static SMSManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SMSManager();
                    _instance.getModems();
                    _instance.setModem();
                }
                return _instance;
            }
      
        }

        public void getModems()
        {
            Doerit.SMSLib.DoerSMSDeviceManager DeviceManager = new Doerit.SMSLib.DoerSMSDeviceManager();
            this.modems = DeviceManager.getData();
        }

        public void setModem()
        {
            _SMSMangerStatus = false;

            foreach (var modem in modems)
            {
                if (modem.Value.Network == "Dialog")
                {
                    WorkingModem = modem.Value;
                    _SMSMangerStatus = true;
                }
                else if(modem.Value.Network == "Mobitel")
                {
                    WorkingModem = modem.Value;
                    _SMSMangerStatus = true;
                }
            }

        }

        public int SendASMS(string _phonenumber,string _message)
        {
            int result=-97;
            if (_instance._SMSMangerStatus)
            {
                result = DoerSMSDeviceCommander.SendSMS(_phonenumber,_message, _instance.WorkingModem.AttachedTo);
            }
            return result;
        }
    }
}
