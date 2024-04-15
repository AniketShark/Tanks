using System;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

namespace Game.Runtime.Network
{
	public class RecoilClient : MonoBehaviour
	{
		[SerializeField]
		private string _serverIp;
		[SerializeField]
		private int _port;
		private TcpClient _tcpClient;
		private bool _connected;
		private string _dataReceived;

		void Start()
		{
			_tcpClient = new TcpClient();
			_tcpClient.Connect(_serverIp, _port);
			Console.WriteLine("Connected to the server.");
		}

		private void ConnectionCallback(IAsyncResult ar)
		{
		}

		void Update()
		{
			byte[] buffer = Encoding.ASCII.GetBytes(_dataReceived);

			_tcpClient.GetStream().Write(buffer, 0, buffer.Length);
			Console.WriteLine("Disconnected from server.");

		}

		public void OnDestroy()
		{
			_tcpClient.Client.Shutdown(SocketShutdown.Send);
			_connected = false;
			_tcpClient.Close();
		}


		void ReceiveData(TcpClient client)
		{
			try
			{
				NetworkStream stream = client.GetStream();
				byte[] receivedBytes = new byte[1024];
				int byte_count;

				while ((byte_count = stream.Read(receivedBytes, 0, receivedBytes.Length)) > 0)
				{
					Console.WriteLine(Encoding.ASCII.GetString(receivedBytes, 0, byte_count));
				}
			}
			catch
			{
				// Handle any exceptions here, such as a server disconnect
			}
			finally
			{
				client.Close();
			}
		}
	}
}


