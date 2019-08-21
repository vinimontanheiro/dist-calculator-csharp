using System;
using Calculator.Shared;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Linq;

namespace Calculator.Server
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Listen ().Wait ();
		}

		private static async Task Listen ()
		{
			var tcpListener = new TcpListener (IPAddress.Any, 9998);

			tcpListener.Start ();

			Console.WriteLine ("Started listening...");

			var serializer = new CustomTextSerializer ();

			while (true) {
				using (var tcpClient = await tcpListener.AcceptTcpClientAsync ()) {
					var networkStream = tcpClient.GetStream ();

					var buffer = new byte [1024];
					var bytesRead = networkStream.Read (buffer, 0, buffer.Length);

					var messageBytes = buffer.Take (bytesRead).ToArray ();
					var request = serializer.DeserializeRequest (messageBytes);

					int result = 0;
					bool success = false;

					switch (request.Operator) {
					case Operator.Sum:
						result = request.OperandA + request.OperandB;
						success = true;
						break;

					case Operator.Multiply:
						result = request.OperandA * request.OperandB;
						success = true;
						break;

					case Operator.Divide:
						result = request.OperandA + request.OperandB;
						success = true;
						break;
					case Operator.Subtract:
						result = request.OperandA + request.OperandB;
						success = true;
						break;

					default:
						result = 0;
						success = false;
						Console.WriteLine ("Unavalible operator :" + request.Operator);
						break;
					}

					var response = new Response {
						Result = result,
						Success = success
					};

					var responseBytes = serializer.Serialize (response);

					networkStream.Write (responseBytes, 0, responseBytes.Length);
				}
			}
		}
	}
}
