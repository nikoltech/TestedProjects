namespace ProxyServer
{
    using System;
    using System.IO;
    using System.Net;
    using System.Net.Sockets;
    using System.Text.RegularExpressions;

    // TODO: Preview. Need improovement & refactoring
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start!");

            TcpListener myTCP = new TcpListener(IPAddress.Parse("127.0.0.1"), 8887);
            myTCP.Start();

            // TODO: CPU do not overloaded. Is it?
            while (true)
            {
                // Check exists request
                if (myTCP.Pending())
                {
                    // запрос есть
                    using (Socket myClient = myTCP.AcceptSocket())
                    {
                        if (myClient.Connected)
                        {
                            // get client request content
                            byte[] httpRequest = ReadToEnd(myClient);

                            // get host & port
                            Regex myReg = new Regex(@"Host: (((?<host>.+?):(?<port>\d+?))|(?<host>.+?))\s+", RegexOptions.Multiline | RegexOptions.IgnoreCase);
                            Match m = myReg.Match(System.Text.Encoding.ASCII.GetString(httpRequest));
                            string host = m.Groups["host"].Value;
                            int port = 0;
                            if (!int.TryParse(m.Groups["port"].Value, out port)) { port = 80; }

                            // get IP
                            IPHostEntry myIPHostEntry = Dns.GetHostEntry(host);

                            // create outgoing network endpoint
                            IPEndPoint myIPEndPoint = new IPEndPoint(myIPHostEntry.AddressList[0], port);

                            // send request to remote server
                            using (Socket myRerouting = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                            {
                                myRerouting.Connect(myIPEndPoint);
                                if (myRerouting.Send(httpRequest, httpRequest.Length, SocketFlags.None) != httpRequest.Length)
                                {
                                    Console.WriteLine("При отправке данных удаленному серверу произошла ошибка...");
                                }
                                else
                                {
                                    // здесь будет код получения ответа от удаленного сервера
                                    byte[] httpResponse = ReadToEnd(myRerouting);
                                    myClient.Send(httpResponse, httpResponse.Length, SocketFlags.None);
                                }
                            }
                        }
                    }
                }
            }
        }

        private static byte[] ReadToEnd(Socket mySocket)
        {
            byte[] b = new byte[mySocket.ReceiveBufferSize];
            int len = 0;
            using (MemoryStream m = new MemoryStream())
            {
                while (mySocket.Poll(1000000, SelectMode.SelectRead) && (len = mySocket.Receive(b, mySocket.ReceiveBufferSize, SocketFlags.None)) > 0)
                {
                    m.Write(b, 0, len);
                }
                return m.ToArray();
            }
        }
    }
}
