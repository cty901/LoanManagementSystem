﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Threading;

namespace Doerit.SMSLib
{
    class DoerSMSDeviceCommander
    {
        public static string SMSDevice_Status(string comPort)
        {
            SerialPort port = new SerialPort();
            port.PortName = comPort;
            if (!port.IsOpen)
            {
                port.Open();
            }
            port.WriteLine("AT+CREG?\r");
            Thread.Sleep(2000);
            String operatorString = port.ReadExisting();
            return operatorString;
        }
        public static string Find_Operator_Name(string comPorts)
        {
            string operator_name;
            SerialPort port = new SerialPort();
            try
            {
                port.PortName = comPorts;
                if (!port.IsOpen)
                {
                    port.Open();
                }
                port.WriteLine("AT+COPS?\r");
                Thread.Sleep(2000);
                String operatorString = port.ReadExisting();

                string[] sub = operatorString.Split('\"');
                if (sub[1] == "41301")
                {
                    //port.WriteLine("AT+CUSD=1,\"AA180C3652281A\",15\r");
                    //System.Threading.Thread.Sleep(5000);
                    //operator_name=port.ReadExisting();
                    operator_name = "Mobitel";
                }

                else if (sub[1] == "41302")
                {
                    //    Console.WriteLine("dialog");
                    //    port.WriteLine("AT+CUSD=1,\"AA11AD661B291A\",15\r");
                    //    System.Threading.Thread.Sleep(5000);
                    //    port.WriteLine("AT+CMGF=1\r");
                    //    port.WriteLine("ATZ\r");
                    //    operator_name=port.ReadExisting();
                    operator_name = "Dialog";
                }
                else
                {
                    operator_name = "Unknown";
                }
            }
            catch
            {
                operator_name = "Error";
            }
            finally
            {

                port.Close();
            }

            return operator_name;
        }

        public static int getSignalStrength(string comport)
        {

            SerialPort port = new SerialPort();
            try
            {
                port.PortName = comport;
                if (!port.IsOpen)
                {
                    port.Open();
                }
                port.WriteLine("AT+CSQ\r");
                System.Threading.Thread.Sleep(2000);
                String operatorString = port.ReadExisting();
                string sub = operatorString.Substring(7, 3);

                return Int32.Parse(sub);
            }
            catch
            {
                return -1;
            }
            finally
            {
                port.Close();
            }
        }

        //public static string get_Credit_Limit(SerialPort comPort)
        //{
        //    string operator_name = Find_Operator_Name(comPort);

        //    if (operator_name == "Mobitel")
        //    {
        //        comPort.WriteLine("AT+CUSD=1,\"AA180C3652281A\",15\r");
        //        System.Threading.Thread.Sleep(5000);
        //        operator_name = port.ReadExisting();
        //    }

        //    if (sub[1] == "41302")
        //    {
        //        Console.WriteLine("dialog");
        //        port.WriteLine("AT+CUSD=1,\"AA11AD661B291A\",15\r");
        //        System.Threading.Thread.Sleep(5000);
        //        operator_name = port.ReadExisting();
        //    }
        //    else
        //    {
        //        operator_name = "Unknown";
        //    }

        //    return operator_name;
        //}
    }
}