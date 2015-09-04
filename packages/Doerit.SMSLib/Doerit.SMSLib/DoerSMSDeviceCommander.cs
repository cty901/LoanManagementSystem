using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Threading;

namespace Doerit.SMSLib
{
    public class DoerSMSDeviceCommander
    {
        public static string SMSDevice_Status(string comPort)
        {
            SerialPort port = new SerialPort();
            String operatorString = "Error";
            try
            {
                port.PortName = comPort;
                if (!port.IsOpen)
                {
                    port.Open();
                }
                port.WriteLine("AT+CREG?\r");
                Thread.Sleep(2000);
                operatorString = port.ReadExisting();
                return operatorString;
            }
            catch
            {
                return operatorString;
            }
            finally
            {
                port.Close();
            }
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
                Thread.Sleep(100);
                port.WriteLine("AT+COPS?\r");
                Thread.Sleep(500);
                String operatorString = port.ReadExisting();

                string[] sub = operatorString.Split('\"');
                if (sub[1] == "41301")
                {
                    //port.WriteLine("AT+CUSD=1,\"AA180C3652281A\",15\r");
                    //System.Threading.Thread.Sleep(5000);
                    //operator_name=port.ReadExisting();
                    operator_name = "Mobitel";
                }

                else if (sub[1] == "41302" || sub[1]=="SRI DIALOG")
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

        public static int SendSMS(string CellNumber,string SMSMessage,string comport)
        {
            SerialPort port = new SerialPort();
            int result=-98;
            try
            {
                port.PortName = comport;
                if (!port.IsOpen)
                {
                    port.Open();
                }
                string MyMessage = null;
                //Check if Message Length <= 160
                if (SMSMessage.Length <= 160)
                    MyMessage = SMSMessage;
                else
                    MyMessage = SMSMessage.Substring(0, 160);

                if (port.IsOpen == true)
                {
                    Thread.Sleep(100);
                    port.Write("AT+CMGF=1\r");
                    Thread.Sleep(100);
                    port.Write("AT+CMGS=\"" + CellNumber + "\"\r\n");
                    Thread.Sleep(100);
                    port.Write(MyMessage + "\x1A");
                    Thread.Sleep(100);
                    string msg=port.ReadExisting();
                    var firstIndex = msg.IndexOf("OK");

                    if (firstIndex != msg.LastIndexOf("OK") && firstIndex != -1)
                    {
                        result=1;
                    }
                    else
                    {
                        result=0;
                    }
                }
                return result;
            }
            catch
            {
                return -99;
            }
            finally
            {
                port.Close();
            }
        }

        public static string get_Credit_Limit(string comPort)
        {
            string operator_name = Find_Operator_Name(comPort);
            SerialPort port = new SerialPort();
            string msg = "";

            try
            {
                port.PortName = comPort;
                if (!port.IsOpen)
                {
                    port.Open();
                }
                if (port.IsOpen == true)
                {
                    if (operator_name == "Mobitel")
                    {
                        port.Write("AT+CMGF=1\r");
                        Thread.Sleep(100);
                        port.WriteLine("AT+CUSD=1,\"235ACD3602\",15\r");
                        Thread.Sleep(100);
                        msg = port.ReadExisting();
                    }
                    else if (operator_name == "Dialog")
                    {
                        port.Write("AT+CMGF=1\r");
                        Thread.Sleep(100);
                        port.WriteLine("AT+CUSD=1,\"235ACD3602\",15\r");
                        Thread.Sleep(100);
                        msg = port.ReadExisting();
                        byte[] packedBytes = PduBitPacker.ConvertHexToBytes(msg);
                        byte[] unpackedBytes = PduBitPacker.UnpackBytes(packedBytes);
                        string s=PduBitPacker.ConvertBytesToHex(unpackedBytes);
                        string result = HexadecimalEncoding.HexToString(s);
                        
                    }
                }
                return msg;
            }
            catch
            {
                return "Code_Error";
            }
            finally
            {
                port.Close();
            }

        }
    }
}
