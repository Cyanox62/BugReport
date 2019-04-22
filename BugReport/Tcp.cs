using System;
using Smod2;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace BugReport
{
	class Tcp
	{
		private Plugin plugin;
		private TcpClient client = null;

		public bool isConnected = false;

		public Tcp(Plugin plugin, string server, int port)
		{
			this.plugin = plugin;
			try
			{
				client = new TcpClient(server, port);
				plugin.Info("Successfully connected to server.");
				isConnected = true;
			}
			catch
			{
				plugin.Info("Error connecting to server");
				isConnected = false;
			}
		}

		public void SendData(string message)
		{
			if (client == null)
			{
				isConnected = false;
			}
			try
			{
				byte[] data = Encoding.ASCII.GetBytes(message);
				NetworkStream stream = client.GetStream();
				stream.Write(data, 0, data.Length);
				Console.WriteLine($"Sent: {message}");
			}
			catch (Exception x)
			{
				Console.WriteLine($"Socket error: {x.ToString()}");
			}
		}
	}
}
