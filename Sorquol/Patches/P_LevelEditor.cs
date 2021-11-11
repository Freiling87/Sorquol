using RogueLibsCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;
using BepInEx.Logging;
using Sorquol;
using Sorquol.Content;
using UnityEngine.UI;

namespace CCU.Patches.Interface
{
	[HarmonyPatch(declaringType: typeof(LevelEditor))]
	public static class P_LevelEditor
	{
		private static readonly ManualLogSource logger = SorquolLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPrefix, HarmonyPatch(methodName: "FixedUpdate", argumentTypes: new Type[0] { })]
		public static bool FixedUpdate_Prefix(LevelEditor __instance, GameObject ___helpScreen, GameObject ___initialSelection, GameObject ___workshopSubmission, GameObject ___longDescription, ButtonHelper ___yesNoButtonHelper, InputField ___chunkNameField, GameObject ___yesNoSelection)
		{
			if (!GC.loadCompleteReally || GC.loadLevel.restartingGame)
				return false;

			if (__instance.loadMenu.activeSelf || ___helpScreen.activeSelf || ___initialSelection.activeSelf || ___workshopSubmission.activeSelf || GC.menuGUI.onMenu || ___longDescription.activeSelf)
				return false;

			string currentInterface = __instance.currentInterface;
			bool alt = Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt);
			bool ctrl = Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl);
			bool shift = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
			bool fieldFocused = __instance.InputFieldFocused();

			if (___yesNoSelection.activeSelf)
			{
				if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.Space))
					__instance.PressedYesButton();
				else if (Input.GetKeyDown(KeyCode.Escape))
					__instance.PressedNoButton();
			}
			if (alt)
			{
				if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
				{
					if (ctrl)
						__instance.PressedChunksButton2();
					else
						__instance.PressedChunksButton();
				}
				else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
				{
					if (ctrl)
						__instance.PressedChunkPacksButton2();
					else
						__instance.PressedChunkPacksButton();
				}
				else if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
				{
					if (ctrl)
						__instance.PressedLevelsButton2();
					else
						__instance.PressedLevelsButton();
				}
				else if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))
				{
					if (ctrl)
						__instance.PressedCampaignsButton2();
					else
						__instance.PressedCampaignsButton();
				}
			}
			else if (ctrl && shift)
			{
				if (Input.GetKeyDown(KeyCode.A))
					LevelEditorUtilities.ToggleSelectAll(__instance, true);
			}
			else if (ctrl)
			{
				if (Input.GetKeyDown(KeyCode.A))
					LevelEditorUtilities.ToggleSelectAll(__instance, false);
				if (Input.GetKeyDown(KeyCode.E))
				{
					if (currentInterface == LevelEditorUtilities.LEInterfaces_Agents || currentInterface == LevelEditorUtilities.LEInterfaces_Floors || currentInterface == LevelEditorUtilities.LEInterfaces_Objects)
						LevelEditorUtilities.SetDirection(__instance, KeyCode.E);
					else if (currentInterface == LevelEditorUtilities.LEInterfaces_PatrolPoints)
						LevelEditorUtilities.IncrementPatrolPointId(__instance, KeyCode.E);
				}
				if (Input.GetKeyDown(KeyCode.N))
					__instance.PressedNewButton();
				if (Input.GetKeyDown(KeyCode.O))
					__instance.PressedLoad();
				if (Input.GetKeyDown(KeyCode.Q))
				{
					if (currentInterface == LevelEditorUtilities.LEInterfaces_Agents || currentInterface == LevelEditorUtilities.LEInterfaces_Floors || currentInterface == LevelEditorUtilities.LEInterfaces_Objects)
						LevelEditorUtilities.SetDirection(__instance, KeyCode.Q);
					else if (currentInterface == LevelEditorUtilities.LEInterfaces_PatrolPoints)
						LevelEditorUtilities.IncrementPatrolPointId(__instance, KeyCode.Q);
				}
				if (Input.GetKeyDown(KeyCode.S))
					__instance.PressedSave();
			}
			else if (shift && !fieldFocused)
			{
				if (Input.GetKeyDown(KeyCode.E))
					LevelEditorUtilities.ZoomInFully(__instance);
				if (Input.GetKeyDown(KeyCode.Q))
					LevelEditorUtilities.ZoomOutFully(__instance);
			}
			else if (!fieldFocused)
			{
				if (Input.GetKeyDown(KeyCode.UpArrow))
					LevelEditorUtilities.SetDirection(__instance, KeyCode.UpArrow);
				if (Input.GetKeyDown(KeyCode.DownArrow))
					LevelEditorUtilities.SetDirection(__instance, KeyCode.DownArrow);
				if (Input.GetKeyDown(KeyCode.LeftArrow))
					LevelEditorUtilities.SetDirection(__instance, KeyCode.LeftArrow);
				if (Input.GetKeyDown(KeyCode.RightArrow))
					LevelEditorUtilities.SetDirection(__instance, KeyCode.RightArrow);
				if (Input.GetKey(KeyCode.A))
					__instance.ScrollW();
				if (Input.GetKey(KeyCode.D))
					__instance.ScrollE();
				if (Input.GetKeyDown(KeyCode.E))
					__instance.ZoomIn();
				if (Input.GetKeyDown(KeyCode.Q))
					__instance.ZoomOut();
				if (Input.GetKey(KeyCode.S))
					__instance.ScrollS();
				if (Input.GetKey(KeyCode.W))
					__instance.ScrollN();

				if (__instance.currentInterface != "Level")
				{
					if (Input.GetKeyDown(KeyCode.Alpha1))
					{
						__instance.PressedWallsButton();

						if (ctrl)
						{
							__instance.PressedLoadWalls();
							__instance.EnterDrawMode();
						}
					}
					if (Input.GetKeyDown(KeyCode.Alpha2))
					{
						__instance.PressedFloorsButton();

						if (ctrl)
						{
							__instance.PressedLoadFloors();
							__instance.EnterDrawMode();
						}
					}
					if (Input.GetKeyDown(KeyCode.Alpha3))
					{
						__instance.PressedFloors2Button();

						if (ctrl)
						{
							__instance.PressedLoadFloors();
							__instance.EnterDrawMode();
						}
					}
					if (Input.GetKeyDown(KeyCode.Alpha4))
					{
						__instance.PressedFloors3Button();

						if (ctrl)
						{
							__instance.PressedLoadFloors();
							__instance.EnterDrawMode();
						}
					}
					if (Input.GetKeyDown(KeyCode.Alpha5))
					{
						__instance.PressedObjectsButton();

						if (ctrl)
						{
							__instance.PressedLoadObjects();
							__instance.EnterDrawMode();
						}
					}
					if (Input.GetKeyDown(KeyCode.Alpha6))
					{
						__instance.PressedAgentsButton();

						if (ctrl)
						{
							__instance.PressedLoadAgents();
							__instance.EnterDrawMode();
						}
					}
					if (Input.GetKeyDown(KeyCode.Alpha7))
					{
						__instance.PressedItemsButton();

						if (ctrl)
						{
							__instance.PressedLoadItems();
							__instance.EnterDrawMode();
						}
					}
					if (Input.GetKeyDown(KeyCode.Alpha8))
					{
						__instance.PressedLightsButton();

						if (ctrl)
						{
							__instance.PressedLoadLights();
							__instance.EnterDrawMode();
						}
					}
					if (Input.GetKeyDown(KeyCode.Alpha9))
						__instance.PressedPatrolPointsButton();
				}
			}

			if (Input.GetKeyDown(KeyCode.F5))
			{
				if (__instance.ChunkNameUsed(__instance.chunkName))
				{
					__instance.SaveChunkData(true, false);
					__instance.PressedYesButton();
				}
				else
				{
					__instance.PressedSave();
					__instance.PressedYesButton();
				}
			}
			else if (Input.GetKeyDown(KeyCode.F2))
			{
				__instance.PressedNewButton();
				__instance.PressedYesButton();
			}
			else if (Input.GetKeyDown(KeyCode.F9))
			{
				logger.LogDebug(
					"\tAttempting Quickload: \n" +
					"\t\tChunk Name: " + __instance.chunkName);

				//__instance.PressedLoadChunksFile();
				//__instance.LoadChunkFromFile(___chunkNameField.text, ___yesNoButtonHelper);

				string loadName = __instance.chunkName;
				__instance.PressedLoadChunksFile();
				LevelEditorUtilities.PressScrollingMenuButton(loadName);
				// May need to send Yes Button pressed here
			}
			else if (Input.GetKeyDown(KeyCode.F11))
				__instance.PressedPlayButton();

			#region Mouse tracking
			Vector3 vector = GC.cameraScript.actualCamera.ScreenCamera.ScreenToWorldPoint(Input.mousePosition);
			int num;
			int num2;

			if (__instance.currentLayer == LevelEditorUtilities.LEInterfaces_Walls || __instance.currentLayer == LevelEditorUtilities.LEInterfaces_Floors || __instance.currentLayer == LevelEditorUtilities.LEInterfaces_Floors2 || __instance.currentLayer == LevelEditorUtilities.LEInterfaces_Floors3)
			{
				num = (int)((vector.x + 0.32f) / 0.64f);
				num2 = (int)((vector.y + 0.32f) / 0.64f);
			}
			else
			{
				num = (int)((vector.x + 0.16f) / 0.32f);
				num2 = (int)((vector.y + 0.16f) / 0.32f);
			}

			if (num != __instance.previousMouseTileX || num2 != __instance.previousMouseTileY || (vector.x >= -0.64f && __instance.previousMousePosX < -0.64f) || (vector.y >= -0.64f && __instance.previousMousePosY < -0.64f))
			{
				__instance.previousMouseTileX = num;
				__instance.previousMouseTileY = num2;
				__instance.previousMousePosX = vector.x;
				__instance.previousMousePosY = vector.y;
				__instance.EnteredTile();
			}
			#endregion
			return false;
		}
	}
}