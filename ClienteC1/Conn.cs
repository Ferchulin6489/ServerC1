using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using System.Net.Sockets;

namespace ClienteC1
{
    public class Conn
    {
        public Boolean Logeado = false;
        public Socket Sock_ = null;

        public const short AF_INET = 2;
        public const short AF_INET6 = 23;
        public String response = String.Empty;

        private string ip = "127.0.0.1", serverPort = "7666";
        private Thread Th_Rec;
        private Task d;
        public void Config_Server()
        {
            Sock_ = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            

            Th_Rec = new Thread(new ThreadStart(Th_Receiv));
            Th_Rec.SetApartmentState(ApartmentState.STA);
            Th_Rec.IsBackground = true;
        }
        public void Add_Consola(String texto)
        {
                Console.WriteLine(texto +"\n");
        }
        private void Th_Receiv()
        {
            Byte[] bytes = new Byte[1024 * 2];
            while (this.Sock_.Connected && Logeado)
            {
                try
                {
                    bytes = new Byte[1024 * 2];
                    int bytesRec = Sock_.Receive(bytes, SocketFlags.None);
                    String Resp = Declas.Enco.GetString(bytes, 0, bytesRec);
                    //MessageBoxEx.Show(Resp);
                    if (bytesRec > 0)
                        HandleData.Handle_Data(Resp, bytes);
                }
                catch
                {

                }
            }
        }

        public void Connect()
        {

            if (!State_Connection())
            {
                try
                {
                    Sock_.Connect( ip,Convert.ToInt32(serverPort));
                    Logeado = true;
                    Th_Rec.Start();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                Finality_Connection();
                try
                {
                    Sock_.Connect(ip, Convert.ToInt32(serverPort));
                    Th_Rec.Start();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
        public void Finality_Connection()
        {
            try
            {
                Sock_.Disconnect(true);
                Sock_.Close();
            }
            catch
            {

            }
            try
            {
                Th_Rec.Join();
            }
            catch
            {

            }
        }

        internal void Disconnect()
        {
            //throw new NotImplementedException();
            if (State_Connection())
            {
                Sock_.Disconnect(true);
            }

        }

        public Boolean State_Connection()
        {
            try
            {
                return Sock_.Connected;
            }
            catch
            {
                return false;
            }
        }
    }
}
