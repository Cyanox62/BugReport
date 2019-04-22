using System;
using System.IO;
using Smod2.Commands;

namespace BugReport
{
	class BanCommand : ICommandHandler
	{
		public string GetCommandDescription()
		{
			return "Bans a player from using the report system.";
		}

		public string GetUsage()
		{
			return "(RBAN / REPORTBAN) (STEAMID)";
		}

		public string[] OnCall(ICommandSender sender, string[] args)
		{
			if (Directory.Exists(Plugin.ConfigFolerFilePath))
			{
				if (long.TryParse(args[0], out long steamid))
				{
					File.Create($"{Plugin.ConfigFolerFilePath}{Path.DirectorySeparatorChar}{steamid}.txt");
					return new string[]
					{
						"Player successfully banned."
					};
				}
				else
				{
					return new string[]
					{
						"Error parsing SteamID."
					};
				}
			}
			else
			{
				return new string[]
				{
					"Error locating config folder."
				};
			}
		}
	}
}
