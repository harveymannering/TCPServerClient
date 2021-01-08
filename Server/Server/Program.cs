using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    class MainClass
    {
        private const int portNum = 15;

        public static void Main(string[] args)
        {
            bool done = false;

            var listener = new TcpListener(IPAddress.Any, portNum);

            listener.Start();

            while (!done)
            {
                //Establish the connection
                Console.Write("Waiting for connection...");
                TcpClient client = listener.AcceptTcpClient();
                Console.WriteLine("Connection accepted.");
                NetworkStream ns = client.GetStream();

                //acknowledge the receipt to the sending application
                byte[] bytes = new byte[1024];
                int bytesRead = ns.Read(bytes, 0, bytes.Length);
                byte[] byteSend = Encoding.ASCII.GetBytes("String received");
                try
                {
                    ns.Write(byteSend, 0, byteSend.Length);
                    ns.Close();
                    client.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }

                //display the strings as items in a list.
                string[] subs = Encoding.ASCII.GetString(bytes, 0, bytesRead).Split('|');
                foreach (string s in subs)
                {
                    Console.WriteLine(s);
                }

            }

            listener.Stop();

        }
    }
}
