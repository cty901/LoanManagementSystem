using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Doerit.SMSLib;
using System.Diagnostics;
using LoanManagementSystem.View.WpfWindow;
using System.Runtime.CompilerServices;
using System.ComponentModel;

namespace LoanManagementSystem.Model.SMSModel
{
    public class SMSManager
    {
        private static SMSManager _instance;
        public Dictionary<string, HSPAModem> modems { get; set; }
        List<SMSSender> senders = new List<SMSSender>();
        private HSPAModem _workingModem;

        public HSPAModem WorkingModem 
        {
            get
            {
                return _workingModem;
            }
            set
            {
                _workingModem = value;
                OnPropertyChanged();
            }
        }
        SMS s;
        Boolean _SMSMangerStatus=false;
        Doerit.SMSLib.DoerSMSDeviceManager DeviceManager = DoerSMSDeviceManager.Instance;

        private SMSManager()
        {
            DeviceManager.PropertyChanged += getModems;
        }

        public void stopWacher()
        {
            DeviceManager.Dispose();
        }

        public static SMSManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SMSManager();
                    _instance.getModems(_instance,EventArgs.Empty);
                }
                return _instance;
            }
      
        }

        public void getModems(object source,EventArgs e)
        {
            _instance.modems = DeviceManager.getData();
            _instance.setModem();
        }

        public void setModem()
        {
            _SMSMangerStatus = false;

            if (modems.Count <= 0)
            {
                WorkingModem = new HSPAModem();
            }
            else
            {
                foreach (var modem in modems)
                {
                    if (modem.Value.Network == "Dialog")
                    {
                        WorkingModem = modem.Value;
                        _SMSMangerStatus = true;
                    }
                    else if (modem.Value.Network == "Mobitel")
                    {
                        WorkingModem = modem.Value;
                        _SMSMangerStatus = true;
                    }
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

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            //   if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
