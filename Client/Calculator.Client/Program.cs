using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Calculator.Shared;
using System.IO;

namespace Calculator {
    
    class Program
    {
        static void Main(string[] args)
        {
            SendMessage("127.0.0.1",9998).Wait();
        }

		private static async Task SendMessage(string serverIp, int port) 
        {
			
            using (var client = new TcpClient()) {
                Console.WriteLine("Trying to connect on the server...");
                await client.ConnectAsync(serverIp, port);
				Console.WriteLine("Connected on " + serverIp + ":" + port);

                var request = new Request {
                    Operator = Operator.Sum,
                    OperandA = 1,
                    OperandB = 2
                };

                var serializer = new CustomTextSerializer();
                var requestBytes = serializer.Serialize(request);

                Console.WriteLine("Sent message...", 
                Convert.ToBase64String(requestBytes));

                var networkStream = client.GetStream();

                networkStream.Write(requestBytes, 0, requestBytes.Length);

				var buffer = new byte [1024];
				var bytesRead = networkStream.Read (buffer, 0, buffer.Length);

				var messageBytes = buffer.Take (bytesRead).ToArray ();;

				//string response = new StreamReader(networkStream).ReadToEnd();

				var value = serializer.DeserializeResponse (messageBytes);

				if (value.Success) {
					Console.WriteLine ("Resultado :" + value.Result);
				}
					
				networkStream.Close();
		
            }
        }
    }


}