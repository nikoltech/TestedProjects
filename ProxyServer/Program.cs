namespace ProxyServer
{
    using ProxyServer.HTTP;
    using System;
    using System.IO;
    using System.Net;
    using System.Net.Sockets;
    using System.Text.RegularExpressions;
    using System.Threading;

    // TODO: Preview. Need improovement & refactoring
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start!");

            TcpListener myTCP = new TcpListener(IPAddress.Parse("127.0.0.1"), 8887);
            myTCP.Start();

            while (true)
            {
                if (myTCP.Pending())
                {
                    Thread t = new Thread(ExecuteRequest);
                    t.IsBackground = true;
                    t.Start(myTCP.AcceptSocket());
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

        private static void ExecuteRequest(object arg)
        {
            using (Socket myClient = (Socket)arg)
            {
                if (myClient.Connected)
                {
                    byte[] httpRequest = ReadToEnd(myClient);
                    Parser http = new Parser(httpRequest);
                    if (http.Items == null || http.Items.Count <= 0 || !http.Items.ContainsKey("Host"))
                    {
                        Console.WriteLine("Получен запрос {0} байт, заголовки не найдены.", httpRequest.Length);
                    }
                    else
                    {
                        Console.WriteLine("Получен запрос {0} байт, метод {1}, хост {2}:{3}", httpRequest.Length, http.Method, http.Host, http.Port);
                        IPHostEntry myIPHostEntry = Dns.GetHostEntry(http.Host);
                        if (myIPHostEntry == null || myIPHostEntry.AddressList == null || myIPHostEntry.AddressList.Length <= 0)
                        {
                            Console.WriteLine("Не удалось определить IP-адрес по хосту {0}.", http.Host);
                        }
                        else
                        {
                            IPEndPoint myIPEndPoint = new IPEndPoint(myIPHostEntry.AddressList[0], http.Port);
                            using (Socket myRerouting = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                            {
                                myRerouting.Connect(myIPEndPoint);
                                if (myRerouting.Send(httpRequest, httpRequest.Length, SocketFlags.None) != httpRequest.Length)
                                {
                                    Console.WriteLine("При отправке данных удаленному серверу произошла ошибка...");
                                }
                                else
                                {
                                    byte[] httpResponse = ReadToEnd(myRerouting);
                                    myClient.Send(httpResponse, httpResponse.Length, SocketFlags.None);
                                }
                            }
                        }
                    }
                    myClient.Close();
                }
            }
        }
    }
}
