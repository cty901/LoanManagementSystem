using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using System.Collections;

namespace Doerit.SMSLib
{
    /// <summary>
    ///     Provides automated detection and initiation of DoerSMS devices. This class cannot be inherited.
    /// </summary>
    public sealed class DoerSMSDeviceManager : IDisposable, INotifyPropertyChanged
    {
        private static int count = 0;

        /// <summary>
        ///     A System Watcher to hook events from the WMI tree.
        /// </summary>
        private readonly ManagementEventWatcher _deviceWatcher = new ManagementEventWatcher(new WqlEventQuery(
            "SELECT * FROM Win32_DeviceChangeEvent"));

        /// <summary>
        ///     A list of all dynamically found SerialPorts.
        /// </summary>
        private Dictionary<string, HSPAModem> _SMSDevices = new Dictionary<string, HSPAModem>();

        /// <summary>
        ///     Initialises a new instance of the <see cref="DoerSMSDeviceManager"/> class.
        /// </summary>
        /// 
        private static DoerSMSDeviceManager _instance;

        public static DoerSMSDeviceManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DoerSMSDeviceManager();
                }
                return _instance;
            }
        }

        private DoerSMSDeviceManager()
        {
            // Attach an event listener to the device watcher.
            _deviceWatcher.EventArrived += _deviceWatcher_EventArrived;

            // Start monitoring the WMI tree for changes in SerialPort devices.
            _deviceWatcher.Start();

            // Initially populate the devices list.
            DiscoverDoerSMSDevices();
        }

        /// <summary>
        ///     Gets a list of all dynamically found SerialPorts.
        /// </summary>
        /// <value>A list of all dynamically found SerialPorts.</value>
        public Dictionary<string, HSPAModem> SMSDevices
        {
            get { return _SMSDevices; }
            private set
            {
                _SMSDevices = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            // Stop the WMI monitors when this instance is disposed.
            _deviceWatcher.Stop();
        }
        
        //  public delegate void PropertyChangedEventHandler(object source, EventArgs e);
        /// <summary>
        ///     Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Handles the EventArrived event of the _deviceWatcher control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArrivedEventArgs"/> instance containing the event data.</param>
        private void _deviceWatcher_EventArrived(object sender, EventArrivedEventArgs e)
        {
            if(count == 5)
            {
            DiscoverDoerSMSDevices();
                count++;
            }
            else
            {
                count++;
                if (count > 5)
                {
                    count = 0;
                }
            }
        }

        /// <summary>
        ///     Dynamically populates the SerialPorts property with relevant devices discovered from the WMI Win32_SerialPorts class.
        /// </summary>
        private void DiscoverDoerSMSDevices()
        {
            // Create a temporary dictionary to superimpose onto the SerialPorts property.;

            var dict = new Dictionary<string, HSPAModem>();
            try
            {
                // Scan through each SerialPort registered in the WMI.
                ManagementObjectCollection mReturn;
                ManagementObjectSearcher mSearch;
                mSearch = new ManagementObjectSearcher("Select * from Win32_POTSModem");

                mReturn = mSearch.Get();

                foreach (ManagementObject mObj in mReturn)
                {
                    HSPAModem hm = new HSPAModem(mObj["Name"].ToString(), mObj["DeviceID"].ToString(), mObj["Caption"].ToString(), mObj["Description"].ToString(), mObj["Model"].ToString(), mObj["ProviderName"].ToString(), mObj["AttachedTo"].ToString());
                    
                    if (mObj["Status"].ToString().Trim() == "OK")
                    {
                        dict.Add(mObj["AttachedTo"].ToString(), hm);
                    }
                }
                // Return the dictionary.
                //Dispose();

                foreach (var hm in dict)
                {
                    hm.Value.setNetwork(hm.Value.AttachedTo.ToString());
                }

                foreach (var hm in dict)
                {
                    hm.Value.setSignalStrength(hm.Value.AttachedTo.ToString());
                }
                
                SMSDevices = dict;
            }
            catch (ManagementException mex)
            {
                // Send a message to debug.
                Debug.WriteLine(@"An error occurred while querying for WMI data: " + mex.Message);
            }
        }

        /// <summary>
        ///     Called when a property is set.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            //   if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
            if (handler != null) 
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public static void setFlag()
        {
            count = 0;
        }

        public Dictionary<string, HSPAModem> getData()
        {
            return _SMSDevices;
        }
    }
   
}
