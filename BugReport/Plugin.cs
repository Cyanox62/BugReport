using Smod2.Attributes;
using System.IO;

namespace BugReport
{
	[PluginDetails(
	author = "Cyanox",
	name = "Bug Report",
	description = "A bug report plugin.",
	id = "cyan.bugreport",
	version = "1.0.0",
	SmodMajor = 3,
	SmodMinor = 0,
	SmodRevision = 0
	)]
	public class Plugin : Smod2.Plugin
    {
		public static bool isEnabled = false;
		public static string ConfigFolerFilePath = FileManager.GetAppFolder() + "BugReport";

		public override void OnEnable()
		{
			if (!Directory.Exists(ConfigFolerFilePath))
			{
				Directory.CreateDirectory(ConfigFolerFilePath);
			}
		}

		public override void OnDisable(){}

		public override void Register()
		{
			AddConfig(new Smod2.Config.ConfigSetting("br_enabled", true, true, ""));

			AddCommands(new string[] { "reportban", "rban" }, new BanCommand());
			AddCommands(new string[] { "reportunban", "ruban" }, new UnbanCommand());

			AddEventHandlers(new EventHandler(this));
		}
    }
}
