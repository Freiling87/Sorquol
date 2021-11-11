using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RogueLibsCore;
using HarmonyLib;
using BepInEx;
using BepInEx.Logging;
using System.Runtime.CompilerServices;
using System.Reflection;
using System.Diagnostics;

namespace Sorquol
{
	[BepInPlugin(pluginGUID, pluginName, pluginVersion)]
	[BepInProcess("StreetsOfRogue.exe")]
	[BepInDependency(RogueLibs.GUID, RogueLibs.CompiledVersion)]
	public class Core : BaseUnityPlugin
	{
		public const string pluginGUID = "Freiling87.streetsofrogue.Sorquol";
		public const string pluginName = "SORQUOL - Streets of Rogue Quality of Life";
		public const string pluginVersion = "0.1.0";

		public static readonly ManualLogSource logger = SorquolLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		public void Awake()
		{
			LogMethodCall();

			new Harmony(pluginGUID).PatchAll();
			RogueLibs.LoadFromAssembly();
		}
		public static void LogCheckpoint(string note, [CallerMemberName] string callerName = "") =>
			logger.LogInfo(callerName + ": " + note);
		public static void LogMethodCall([CallerMemberName] string callerName = "") =>
			logger.LogInfo(callerName + ": Method Call");
	}
	public static class CoreTools
	{
		public static T GetMethodWithoutOverrides<T>(this MethodInfo method, object callFrom)
				where T : Delegate
		{
			IntPtr ptr = method.MethodHandle.GetFunctionPointer();
			return (T)Activator.CreateInstance(typeof(T), callFrom, ptr);
		}
	}
	public static class SorquolLogger
	{
		private static string GetLoggerName(Type containingClass)
		{
			return $"CCU_{containingClass.Name}";
		}

		public static ManualLogSource GetLogger()
		{
			Type containingClass = new StackFrame(1, false).GetMethod().ReflectedType;
			return Logger.CreateLogSource(GetLoggerName(containingClass));
		}
	}
}
