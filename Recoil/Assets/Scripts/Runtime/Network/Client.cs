using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Game.Runtime.Network
{
	public class Client : MonoBehaviour
	{
		public static Client Instance;
		public static int DATA_BUFFER_SIZE = 4096;
		public string ip = "127.0.0.1";
		public int port = 5000;
		private TCP _tcp = null;

		void Awake()
		{
			if (Instance == null)
				Instance = this;
		}

		private void Start()
		{
			_tcp = new TCP();
		}


		public void ConnectToServer()
		{
			_tcp.Connect();
		}

		public void SendInputDataToServer(NetworkMessegeType messegeType)
		{
			Packet packet = new Packet();
			packet.WriteString(messegeType.ToString());
			_tcp.Send(packet);
		}

		private void OnDestroy()
		{
			_tcp.Disconnect();

		}

	}

	public class TCP
	{
		public TcpClient client;
		private NetworkStream _networkStream;
		private byte[] _dataBuffer;

		public void Connect()
		{
			client = new TcpClient()
			{
				ReceiveBufferSize = Client.DATA_BUFFER_SIZE,
				SendBufferSize = Client.DATA_BUFFER_SIZE
			};

			_dataBuffer = new byte[Client.DATA_BUFFER_SIZE];
			client.BeginConnect(Client.Instance.ip, Client.Instance.port, ConnectCallback, client);

		}

		public void Send(Packet message)
		{
			
			try
			{
				if (client.Connected)
				{
					_networkStream.Write(message.ToArray(), 0, message.Count());
				}
			}
			catch (Exception ex)
			{
				Debug.Log(ex.Message);
				//TODO: Disconnect the client
				client.Close();
			}

		}

		public async void Receive()
		{
			NetworkStream stream = client.GetStream();
			int byte_count = await stream.ReadAsync(_dataBuffer, 0, _dataBuffer.Length);
			if (byte_count > 0)
			{
				Packet packet = new Packet();
				packet.WriteBytes(_dataBuffer);
				Debug.Log(packet.ReadString());
			}
		}

		private void SendCallback(IAsyncResult ar)
		{
			try
			{
				Debug.Log("Data Sent");
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				//TODO: Disconnect the client
				client.Close();
			}
		}


		public void Disconnect()
		{
			_networkStream.Close();
			client.Close();
		}


		private void ConnectCallback(IAsyncResult ar)
		{
			client.EndConnect(ar);

			if (!client.Connected)
				return;

			_networkStream = client.GetStream();
			_networkStream.BeginRead(_dataBuffer, 0, Client.DATA_BUFFER_SIZE, ReceiveCallback, null);
		}

		private void ReceiveCallback(IAsyncResult ar)
		{
			try
			{
				int byteLength = _networkStream.EndRead(ar);

				if (byteLength > 0)
				{
					return;
				}
				byte[] bytes = new byte[byteLength];
				Array.Copy(_dataBuffer, bytes, bytes.Length);
				//TODO : handle data
				Packet packet = new Packet();
				packet.WriteBytes(bytes);
				Debug.Log(packet.ReadString());

				_networkStream.BeginRead(_dataBuffer, 0, bytes.Length, ReceiveCallback, null);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				//TODO: Disconnect the client
				client.Close();
			}
		}
	}
}