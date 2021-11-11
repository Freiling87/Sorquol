
#	C	Documentation
##		C	Keyboard Layout Diagrams 
###			√	Chunk Editor
http://www.keyboard-layout-editor.com/#/gists/2f3df5c48d93b5cbb24ebddca302ffd6
###			C	Level Editor
New

#	C	All Editors
##		CT	Hotkeys
###			CT	F11 - Return to Editor
- Tried adding a check for GC.loadCompleteReally
  - Tried adding !GC.loadLevel.restartingGame, noticed it's used in leveleditor check
    - If this one doesn't work, just ice the feature since it's not really important
- levelEditor.ReturnToLevelEditor();
  - Patched this into PlayerControl.Update
   	- In Editor: Reloads editor
    - In Testing: Works
    - Called by NextLevel, so it's probably better to do the latter since it will handle weird cases
- See also MenuGUI Class in case above doesn't work
###			C	Enter or Space - Yes on YesNo menu
- Changed to GetKeyDown to rate-limit
Need to test this with *all* menus, because you're not guaranteed that they're all made the same 
- Load Chunk
  - Esc works, not Enter
###			C	Escape - No on YesNo menu
- Changed to GetKeyDown to rate-limit
- Works, but seems to either trigger per-frame, or go directly to the meta-menu when you press escape from the editor. Find a way to halt the input once it's done it once.
- Need to test this with *all* menus, because you're not guaranteed that they're all made the same 
- See also levelEditor.CloseNotification(), CloseHelpScreen()...
- On Load Chunk, it actually closed the Chunk name selector window before the yesno window. How do I direct it?
###			C	Letters - Scroll Menu to section starting with letter
- ScrollingMenu.OpenScrollingMenu: 
  - __instance.scrollBarDetails.value = 1f;
    - Possibly where initial scroll location is set?
- Detect whether ScrollingMenu is active/open
  - ButtonHelper.scrollingMenu != null
  - Detect input
    - Get all ScrollingButtons from ScrollingMenu, count to letter
      - GC.buttonHelpersList
      - GC.menuButtonHelpersList
      - ScrollingMenu.numButtonsOnScreen
      - Set ScrollBar to that y-axis
        - ScrollBar.Set
###			√	Alt + 1/2/3/4 - Switch to Editor
Complete
###			√	Alt + Ctrl + 1/2/3/4 - Quickswitch to Editor
Complete

#	C	Character Editor
###		N	Access from Chunk/Campaign Editor selector dropdown
Next release
##		C	Extend Sort button to Added traits - Name
- ScrollingMenu.PushedButton @ 0006
  - Pretty much has exactly what you need.
- DW
- Remember, there's a button that does this... just tie this into that.
##		C	Extend Sort button to Added traits - Value
- ScrollingMenu.PushedButton @ 0006
  - Pretty much has exactly what you need.
- Remember, there's a button that does this... just tie this into that.
##		C	Change conflicts to red text on mouseover
- If you mouseover a trait with conflicts, change text on added traits to red temporarily.

#	C	Chunk Editor
##		CT	Hotkeys
###			T	00 SetOrientation/SetDirection not updating in fields for Draw Mode only
- Distributed the changes from the LeftArrow approach. Test that it was done right.
###			C	F9 - Quickload
- Maybe try this through calling menus and sending a ButtonHelper as Pressed.
  - Proposed:
	// Get current file name
	// Open SCrollingmenu
	// TRY send pressedbutton
	// If no match, some kind of UI feedback to indicate it
	// Maybe those popup dialogues are easier than you think?
    - Attempted
      - Omitting PressedYesNoButton() after PressedScrollingMenuButton(), because I am not sure yet if it even asks for confirmation, or if so, if it always asks or only does so when there are no changes to the current chunk's saved version.
      - Logging messages showed, but no effect whatsoever:
			[Debug  :CCU_P_LevelEditor]     Attempting Quickload:
			Chunk Name: 00TestChunk
		
// MenuGUI.OpenYesNoScreen, the non-yesno equivalent
- Probably also need a general static method you can call to send a button you determine by name, since you'll likely need to reuse it somewhere.
- Confirmed ButtonHelper is not a Singleton class, however LevelEditor declares yesNoButtonHelper
    - ButtonHelper.PressedScrollingMenuButton
- This is still occurring after unpredictable intervals:
	[Debug  :CCU_P_LevelEditor]     Attempting Quickload:
        Chunk Name: 00TestChunk
	[Error  : Unity Log] NullReferenceException: Object reference not set to an instance of an object
	Stack trace:
	LevelEditor.LoadChunkFromFile (System.String chunkName, ButtonHelper myButtonHelper) (at <cc65d589faac4fcd9b0b87048bb034d5>:0)
	CCU.Patches.Interface.P_LevelEditor.FixedUpdate_Prefix (LevelEditor __instance, UnityEngine.GameObject ___helpScreen, UnityEngine.GameObject ___initialSelection, UnityEngine.GameObject ___workshopSubmission, UnityEngine.GameObject ___longDescription, ButtonHelper ___yesNoButtonHelper, UnityEngine.UI.InputField ___chunkNameField) (at <ca47c8100fc149198a1a46d28d85f694>:0)
	LevelEditor.FixedUpdate () (at <cc65d589faac4fcd9b0b87048bb034d5>:0)
- Log it.
###			H	A-Z - skip to letter on scrolling menu
On ice, pending collaboration with someone who uderstands UI methods well
###			H	A + Ctrl + Shift - Select all in Layer only
What I mean here is that the normal Ctrl + A should select in all layers. Is that possible?
###			H	C + Ctrl - Copy, All Layers
Hold
###			H	C + Ctrl + Shift - Copy, One Layer
Hold
###			H	V + Ctrl - Paste All Layers
- Pending completion of Copy
###			H	V + Ctrl + Shift - Paste One Layer
Hold
###			H	Y + Ctrl - Redo
New
###			H	Z + Ctrl - Undo
New
###			H	Alt - Security Cam: Highlight Visible Tiles
Pending anyone indicating they actually could use this feature
###			H	Alt + Ctrl - Show Spawn Chances
- Pending Pilot NumberBox display
- Filter to layer too?
###			H	Alt + Shift - Filter + Display Patrol Sequence IDs on all Points in field Patrol ID 
New
###			H	Ctrl + Shift - Filter & Display Owner IDs
New
###			H	Ctrl + Shift - Filter & Display Patrol IDs (group, not sequence) on all Points
New
###			H	F12 - Exit Playing Chunk
On ice, pending user request
###			H	Mouse3 - Drag Viewport
New, and hell no
###			H	NumKeys, NumPad + Alt - Menu Trails
ALT trail for overhead menus
This one is likely beyond my ability right now since we'd need to underline text in menus or make popup shortcut letter boxes. 
###			H	Tab - Tab through fields
- Putting this on ice - not sure how useful of a feature it is yet
- Pending Input Rate Limit
- Technically works. Only moved between the three Spawn% fields in the horizontal group
	[Info   :  CCU_Core] Tab: Method Call
	[Debug  :CCU_LevelEditorUtilities] Active Field: SpawnChance3Agent
	[Debug  :CCU_LevelEditorUtilities] ActiveInputField: SpawnChance3Agent (UnityEngine.UI.InputField)
	[Info   :  CCU_Core] Tab: Method Call
	[Debug  :CCU_LevelEditorUtilities] Active Field: SpawnChance2Agent
	[Debug  :CCU_LevelEditorUtilities] ActiveInputField: SpawnChance2Agent (UnityEngine.UI.InputField)
	[Info   :  CCU_Core] Tab: Method Call
	[Debug  :CCU_LevelEditorUtilities] Active Field: SpawnChanceAgent
	[Debug  :CCU_LevelEditorUtilities] ActiveInputField: SpawnChanceAgent (UnityEngine.UI.InputField)
	[Info   :  CCU_Core] Tab: Method Call
	[Debug  :CCU_LevelEditorUtilities] Active Field: SpawnChance2Agent
	[Debug  :CCU_LevelEditorUtilities] ActiveInputField: SpawnChance2Agent (UnityEngine.UI.InputField)
	[Info   :  CCU_Core] Tab: Method Call
	[Debug  :CCU_LevelEditorUtilities] Active Field: SpawnChance3Agent
	[Debug  :CCU_LevelEditorUtilities] ActiveInputField: SpawnChance3Agent (UnityEngine.UI.InputField)
- Review the output from LevelEditor.Start_Postfix, as it traverses a field list by name.
###			√	A + Ctrl - De/Select All
Complete
###			√	E, Q - Zoom In/Out
Complete
###			√	E, Q + Ctrl - Increment Patrol Point
Complete
###			√	E, Q + Ctrl - Rotate Direction Field Value
Complete
###			√	E, Q + Shift - Max Zoom In/Out
Complete
###			√	N + Ctrl - New
Complete
###			√	O + Ctrl - Open
Complete
###			√	S + Ctrl - Save
Complete
###			√	Arrow Keys - Set Direction Field Value, or Toggle if matching
Complete
###			√	F2 - QuickNew
Complete
###			√	F5 - Quicksave
Complete
###			√	F9 - Abort function if no matching filename to field
This is pretty much automatic already, since it will just fail. But reactivate if you ever want to put up a warning message or some kind of UI indicator.
###			√	F11 - Play Chunk
Complete
###			√	NumKeys - Select Layer
Complete
###			√	NumKeys + Ctrl - Select Layer & Open Draw Type Selector
Complete
###			√	Tab + Shift- Reverse-Tab through fields
Complete
##		H	Orient Object Sprites in Edit Mode
I.e., show rotated sprite for any objects
##		H	Rotate Chunks in Play Mode
This sounds hard
##		H	Red-Tint Out-Of-District Objects
I.e., Show stuff that won't show up, unless you can disable that disabling behavior

#	C	Level Editor
##		C	Hotkeys
###			C	Arrow Keys - Set Chunk Direction, Draw or Select Mode
Need separate version
###			H	Arrow Keys - Clear Chunk Direction
Pending resolution of Original
###			H	Ctrl + A - De-select All Chunks if All Selected
Pending resolution of Select All, but seems to work here
###			C	Ctrl + A - Select All Chunks
- Only selects one:
  - Select All:
		[Info   :  CCU_Core] ToggleSelectAll: Method Call
		[Debug  :CCU_LevelEditorUtilities]      Tile list count: 100
		[Debug  :CCU_LevelEditorUtilities]      Selected count: 0
  - Deselect all (actually worked):
		[Info   :  CCU_Core] ToggleSelectAll: Method Call
		[Debug  :CCU_LevelEditorUtilities]      Tile list count: 100
		[Debug  :CCU_LevelEditorUtilities]      Selected count: 1
		[Debug  :CCU_LevelEditorUtilities]              Index 0: TestChunkExit
		[Error  : Unity Log] ArgumentOutOfRangeException: Index was out of range. Must be non-negative and less than the size of the collection.
		Parameter name: index
		Stack trace:
		System.ThrowHelper.ThrowArgumentOutOfRangeException (System.ExceptionArgument argument, System.ExceptionResource resource) (at <44afb4564e9347cf99a1865351ea8f4a>:0)
		System.ThrowHelper.ThrowArgumentOutOfRangeException () (at <44afb4564e9347cf99a1865351ea8f4a>:0)
		LevelEditor.UpdateInterface (System.Boolean setDefaults) (at <cc65d589faac4fcd9b0b87048bb034d5>:0)
		LevelEditor.ClearSelections (System.Boolean setDefaults) (at <cc65d589faac4fcd9b0b87048bb034d5>:0)
		CCU.Content.LevelEditorUtilities.ToggleSelectAll (LevelEditor levelEditor, System.Boolean limitToLayer) (at <60860e74db334df19fb9b913ab023427>:0)
		CCU.Patches.Interface.P_LevelEditor.FixedUpdate_Prefix (LevelEditor __instance, UnityEngine.GameObject ___helpScreen, UnityEngine.GameObject ___initialSelection, UnityEngine.GameObject ___workshopSubmission, UnityEngine.GameObject ___longDescription, ButtonHelper ___yesNoButtonHelper, UnityEngine.UI.InputField ___chunkNameField) (at <60860e74db334df19fb9b913ab023427>:0)
		LevelEditor.FixedUpdate () (at <cc65d589faac4fcd9b0b87048bb034d5>:0)
###			C	Up/Down - Flip Chunk over X axis
New
###			C	Ctrl + E, Q - Toggle Rotation
New
###			C	Left/Right - Flip over Y axis
New
###			H	Ctrl + Y - Redo
Pending the skills to do this
###			H	Ctrl + Z - Redo
Pending the skills to do this
###			C	F9 - Quickload
Opens Load selector menu
###			√	Ctrl + S - Save
Complete
###			√	Ctrl + O - Open
Complete
###			√	F5 - Quicksave
Complete

#	N	Player Utilities
This might need to be its own mod
##		H	Mouse3 Bind to command followers
- Target
  - Ground - All Stand Guard
  - Agent - All Attack
  - Self - All Follow
- Could also be an item or SA
##		H	Mutators to omit Vanilla content when custom is available
- If designer has added customs to be Roamers, or Hide in Bushes, etc., have some mutators to exclude Vanilla types from those spawning behaviors
##		H	Save Chunk Pack configuration between loads
- I.e., only deactivate chunk packs when the player says so!
  - This is useful but doesn't belong in CCU, it belongs in a QOL mod
##		H	Show Chunk info on Mouseover in Map mode
- When in gameplay map view, mouseover a chunk to see its name and author in the unused space in the margins.
  - Gives credit to author
  - Helps identify gamebreaking chunks, allowing you to not use the chunk pack or notify their author.
    - This is useful but doesn't belong in CCU, it belongs in a QOL mod