﻿using System;
using System.IO;
using Smod2.Commands;

namespace BugReport
{
	class UnbanCommand : ICommandHandler
	{
		public string GetCommandDescription()
		{
			return "Unbans a player from using the report system.";
		}

		public string GetUsage()
		{
			return "(RUBAN / REPORTUNBAN) (STEAMID)";
		}

		public string[] OnCall(ICommandSender sender, string[] args)
		{
			if (Directory.Exists(Plugin.ConfigFolerFilePath))
			{
				if (long.TryParse(args[0], out long steamid))
				{
					string file = $"{Plugin.ConfigFolerFilePath}{Path.DirectorySeparatorChar}{steamid}.txt";
					if (File.Exists(file))
					{
						File.Delete(file);
						return new string[]
						{
							"Player successfully unbanned."
						};
					}
					else
					{
						return new string[]
						{
							"Player is not banned."
						};
					}
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
