using System.Linq;
using System.IO;
using Smod2.API;
using Smod2.EventHandlers;
using Smod2.Events;

namespace BugReport
{
	class EventHandler : IEventHandlerCallCommand
	{
		private Plugin instance;
		private Tcp tcp;

		public EventHandler(Plugin plugin)
		{
			instance = plugin;
			tcp = new Tcp(plugin, "127.0.0.1", 9090);
		}

		private bool isPlayerBanned(Player player)
		{
			if (Directory.Exists(Plugin.ConfigFolerFilePath))
			{
				foreach (string file in Directory.GetFiles(Plugin.ConfigFolerFilePath))
				{
					if (file.Replace($"{Plugin.ConfigFolerFilePath}{Path.DirectorySeparatorChar}", "").Replace(".txt", "").Trim() == player.SteamId)
					{
						return true;
					}
				}
			}
			return false;
		}

		public void OnCallCommand(PlayerCallCommandEvent ev)
		{
			if (ev.Command.ToLower().StartsWith("bug"))
			{
				if (tcp.isConnected)
				{
					if (ev.Command.Length > 4)
					{
						if (!isPlayerBanned(ev.Player))
						{
							tcp.SendData($"player{ev.Player.Name} ({ev.Player.SteamId})");
							tcp.SendData($"bug{ev.Command.Substring(4)}");
							ev.ReturnMessage = "Thank you for your report, it has been sent to the developer. " +
								"Reminder that troll reports will result in a ban from using this system.";
						}
						else
						{
							ev.ReturnMessage = "You have been banned from using the bug report system due to abuse.";
						}
					}
					else
					{
						ev.ReturnMessage = "USAGE:\n.bug your report here";
					}
				}
				else
				{
					ev.ReturnMessage = "Error connecting to report server. Try again later.";
				}
			}
		}
	}
}
