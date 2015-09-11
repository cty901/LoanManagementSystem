using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doerit.SMSLib
{
    public class HSPAModem
    {
        public string Name { get; set; }
        public string DeviceID { get; set; }
        public string Caption { get; set; }
        public string Description { get; set; }
        public string Model { get; set; }
        public string ProviderName { get; set; }
        public string AttachedTo { get; set; }
        public string Network { get; set; }
        public string SignalStrength { get; set; }

        public HSPAModem(string name, string ID, string caption, string desc, string model, string proname, string at)
        {
            this.Name = name;
            this.DeviceID = ID;
            this.Caption = caption;
            this.Description = desc;
            this.Model = model;
            this.ProviderName = proname;
            this.AttachedTo = at;
        }
        public HSPAModem()
        {
            this.Name = "No Device";
            this.DeviceID = "No Device";
            this.Caption = "No Device";
            this.Description = "No Device";
            this.Model = "No Device";
            this.ProviderName = "No Device";
            this.AttachedTo = "No Device";
            this.SignalStrength = "0";
            this.Network = "No Service";
        }
        int _number = 3;
        public int Test
        {
            get
            {
                return this._number;
            }
            set
            {
                this._number = value;
            }
        }
        public void setNetwork(string comport)
        {
            this.Network = DoerSMSDeviceCommander.Find_Operator_Name(comport);
        }
        public void setSignalStrength(string comport)
        {
            int _SignalStrength = DoerSMSDeviceCommander.getSignalStrength(comport);
            if (_SignalStrength >= 0 && _SignalStrength <= 6)
            {
                this.SignalStrength = "1";
            }
            else if (_SignalStrength >= 7 && _SignalStrength <= 12)
            {
                this.SignalStrength = "2";
            }
            else if (_SignalStrength >= 13 && _SignalStrength <= 18)
            {
                this.SignalStrength = "3";
            }
            else if (_SignalStrength >= 19 && _SignalStrength <= 24)
            {
                this.SignalStrength = "4";
            }
            else if (_SignalStrength >= 25 && _SignalStrength <= 31)
            {
                this.SignalStrength = "5";
            }
            else
            {
                this.SignalStrength = "0";
            }

        }
    }
}
