using System;
using System.Net.Sockets;
using System.Text;

namespace Client2
{
    class MainClass
    {
        private const int portNum = 15;
        private const string hostName = "localhost";

        public static int Main(String[] args)
        {
            Console.WriteLine("Enter String:");
            string user_input = Console.ReadLine();

            try
            {
                //establish connection
                var client = new TcpClient(hostName, portNum);
                NetworkStream ns = client.GetStream();

                //send users string
                byte[] byteSend = Encoding.ASCII.GetBytes(user_input);
                try
                {
                    ns.Write(byteSend, 0, byteSend.Length);
                                    }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }

                //Read back response
                byte[] bytes = new byte[1024];
                int bytesRead = ns.Read(bytes, 0, bytes.Length);

                Console.WriteLine(Encoding.ASCII.GetString(bytes, 0, bytesRead));

                ns.Close();
                client.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return 0;
        }
    }
}
