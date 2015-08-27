using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Threading;
using org.smslib;
using org.smslib.modem;
using System.IO;

namespace Doerit.SMSLib
{
    public class SMSSender
    {
        List<int> contact_list = new List<int>();
        HSPAModem modem;
        SMS sms;

        Service srv;
        Comm2IP.Comm2IP com1;

        public SMSSender(List<int> cl, HSPAModem m, SMS s)
        {
            this.contact_list = cl;
            this.modem = m;
            this.sms = s;
        }        

        public void set(string gateway_name,int port,byte[] b_ip,string s_ip)
        {     
            srv = Service.getInstance();
            com1 = new Comm2IP.Comm2IP(b_ip, port, modem.AttachedTo , 115200); //modem comport

            try
            {
                new Thread(new ThreadStart(com1.Run)).Start();

                IPModemGateway gateway = new IPModemGateway(gateway_name, s_ip, port, "Huawei", "");

                gateway.setIpProtocol(ModemGateway.IPProtocols.BINARY);
                gateway.setProtocol(AGateway.Protocols.PDU);
                gateway.setInbound(true);
                gateway.setOutbound(true);
                gateway.setSimPin("0000");

                srv.addGateway(gateway);

                // Start! (i.e. connect to all defined Gateways)
                srv.startService();

                // Send more than one message at once.
                OutboundMessage[] msgArray = new OutboundMessage[contact_list.Count];
                int count = 0;

                foreach (int number in contact_list)
                {
                    msgArray[count++] = new OutboundMessage(number.ToString(), sms.Content);
                }

                srv.sendMessages(msgArray);

                //Console.WriteLine(msgArray[0]);
                //Console.WriteLine(msgArray[1]);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                Console.Read();
            }
            finally
            {
                com1.Stop();
                srv.stopService();
            }
        }

        public void stop()
        {
            if (srv != null)
                srv.stopService();
            if (com1 != null)
                com1.Stop();
        }

        public class CallNotification : ICallNotification
        {
            public void process(AGateway gateway, String callerId)
            {
                Console.WriteLine(">>> New call detected from Gateway: " + gateway.getGatewayId() + " : " + callerId);
            }
        }

        public class InboundNotification : IInboundMessageNotification
        {
            public void process(AGateway gateway, org.smslib.Message.MessageTypes msgType, InboundMessage msg)
            {
                if (msgType == org.smslib.Message.MessageTypes.INBOUND) Console.WriteLine(">>> New Inbound message detected from Gateway: " + gateway.getGatewayId());
                else if (msgType == org.smslib.Message.MessageTypes.STATUSREPORT) Console.WriteLine(">>> New Inbound Status Report message detected from Gateway: " + gateway.getGatewayId());
                Console.WriteLine(msg);
                try
                {
                    // Uncomment following line if you wish to delete the message upon arrival.
                    gateway.deleteMessage(msg);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Oops!!! Something gone bad...");
                    Console.WriteLine(e.Message);
                    Console.WriteLine(e.StackTrace);
                }
            }
        }

        public class GatewayStatusNotification : IGatewayStatusNotification
        {
            public void process(AGateway gateway, org.smslib.AGateway.GatewayStatuses oldStatus, org.smslib.AGateway.GatewayStatuses newStatus)
            {
                Console.WriteLine(">>> Gateway Status change for " + gateway.getGatewayId() + ", OLD: " + oldStatus + " -> NEW: " + newStatus);
            }
        }

    }
}
