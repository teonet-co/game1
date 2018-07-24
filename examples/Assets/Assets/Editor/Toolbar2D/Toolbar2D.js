import UnityEditor;
import System.Collections.Generic;
import UnityEngine.UI;

public class Toolbar2D extends EditorWindow
{
	var selectedGameObjects : GameObject[];
	var style : GUIStyle = new GUIStyle();
	var scrollPos : Vector2;
	
	//Windows
	var selectedObjectsWindow : Rect = new Rect(0,0,100,150);
	var snappingWindow : Rect = new Rect(100,0,100,150);
	var createWindow : Rect = new Rect(200,0,250,150);
	var editorWindow : Rect = new Rect(450,0,150,150);
	var menusWindow : Rect = new Rect(600,0,150,150);
	var toolbarWindow : Rect = new Rect(750,0,150,150);
	
	//Snapping
	var snapX : float;
	var snapY : float;
	var snapXEnabled : boolean;
	var snapYEnabled : boolean;
	
	//Scenes
	var scenes : String[];
	
	@MenuItem ("Window/Toolbar 2D") 	
    static function InitToolbar () 														
    {
        EditorWindow.GetWindow(Toolbar2D);															
        EditorWindow.GetWindow(Toolbar2D).minSize = new Vector2(1000, 150); 
        EditorWindow.GetWindow(Toolbar2D).maxSize = new Vector2(2000, 150); 			
    }
 
    function Update ()															
    {	
    	GetScenes();
    	
    	if(UnityEditor.Selection.gameObjects != null){
    		selectedGameObjects = UnityEditor.Selection.gameObjects;
    		Snap();
    	}else{
    		selectedGameObjects = null;
    	}
    }
    
    function OnGUI ()
    {
    	BeginWindows();
    	selectedObjectsWindow = GUI.Window(0, selectedObjectsWindow, SelectedGameObjects, "Selected");
    	snappingWindow = GUI.Window(1, snappingWindow, Snapping, "Snapping");
    	createWindow = GUI.Window(2, createWindow, Create, "Create");
    	editorWindow = GUI.Window(3, editorWindow, WindowEditor, "Editor");
    	menusWindow = GUI.Window(4, menusWindow, WindowMenus, "Menus");
    	toolbarWindow = GUI.Window(5, toolbarWindow, Toolbar, "Toolbar 2D");
    	EndWindows();
    	
    	style.richText = true;
    }
    
    function Snap ()
    {
    	if(snapXEnabled){
    		for(var x : int = 0; x < selectedGameObjects.Length; x ++){
    			selectedGameObjects[x].transform.position.x = snapX * Mathf.Round((selectedGameObjects[x].transform.position.x / snapX));
    		}
    	}    	
    	if(snapYEnabled){
    		for(var y : int = 0; y < selectedGameObjects.Length; y ++){
    			selectedGameObjects[y].transform.position.y = snapY * Mathf.Round((selectedGameObjects[y].transform.position.y / snapY));
    		}
    	}
    }
    
    //Windows
    function SelectedGameObjects (id : int)
    {
    	if(GUILayout.Button(GUIContent("Center", "Sets selection's position to 0,0"), GUILayout.Width(90), GUILayout.Height(30))){
    		for(var x : int = 0; x < selectedGameObjects.Length; x ++){
    			selectedGameObjects[x].transform.position = Vector3(0,0,0);
    		}
    	}
    	
    	if(GUILayout.Button(GUIContent("Center Scene", "Sets selection's position to the scene cameras position"), GUILayout.Width(90), GUILayout.Height(30))){
    		for(var y : int = 0; y < selectedGameObjects.Length; y ++){
    			selectedGameObjects[y].transform.position.x = SceneView.lastActiveSceneView.camera.transform.position.x;
    			selectedGameObjects[y].transform.position.y = SceneView.lastActiveSceneView.camera.transform.position.y;
    		}
    	}
    	    	
    	GUI.DragWindow();
    	selectedObjectsWindow.x = Mathf.Clamp(selectedObjectsWindow.x, 0, Screen.width - selectedObjectsWindow.width); 
    	selectedObjectsWindow.y = Mathf.Clamp(selectedObjectsWindow.y, 0, Screen.height - selectedObjectsWindow.height - 22);
    }
    
    function Snapping (id : int)
    {
    	EditorGUILayout.BeginVertical();
    		EditorGUILayout.BeginHorizontal();
    			GUILayout.Label("X");
    			snapX = EditorGUILayout.FloatField(snapX);
    		EditorGUILayout.EndHorizontal();
    		EditorGUILayout.BeginHorizontal();
    			GUILayout.Label("Y");
    			snapY = EditorGUILayout.FloatField(snapY);
    		EditorGUILayout.EndHorizontal();
    		EditorGUILayout.BeginHorizontal();
    			GUILayout.Label(GUIContent("Snap X", "Enables snapping on the X axis"));
    			snapXEnabled = EditorGUILayout.Toggle(snapXEnabled);
    		EditorGUILayout.EndHorizontal();
    		EditorGUILayout.BeginHorizontal();
    			GUILayout.Label(GUIContent("Snap Y", "Enables snapping on the Y axis"));
    			snapYEnabled = EditorGUILayout.Toggle(snapYEnabled);
    		EditorGUILayout.EndHorizontal();
    	EditorGUILayout.EndVertical();
    	
    	GUI.DragWindow();
    	snappingWindow.x = Mathf.Clamp(snappingWindow.x, 0, Screen.width - snappingWindow.width); 
    	snappingWindow.y = Mathf.Clamp(snappingWindow.y, 0, Screen.height - snappingWindow.height - 22);
    }
    
    function Create (id : int)
    {
    	GUILayout.Label("Basic");
    	EditorGUILayout.BeginHorizontal();
    		if(GUILayout.Button(GUIContent("Empty", "Creates an empty GameObject"), GUILayout.Width(55), GUILayout.Height(20))){
    			EditorApplication.ExecuteMenuItem("GameObject/Create Empty");
    		}
    		if(GUILayout.Button(GUIContent("Sprite", "Creates a sprite"), GUILayout.Width(55), GUILayout.Height(20))){
    			EditorApplication.ExecuteMenuItem("GameObject/2D Object/Sprite");
    		}
    		if(GUILayout.Button(GUIContent("Camera", "Creates a camera"), GUILayout.Width(60), GUILayout.Height(20))){
    			EditorApplication.ExecuteMenuItem("GameObject/Camera");
    		}
    		if(GUILayout.Button(GUIContent("Particle", "Creates a particle system"), GUILayout.Width(55), GUILayout.Height(20))){
    			EditorApplication.ExecuteMenuItem("GameObject/Particle System");
    		}
    	EditorGUILayout.EndHorizontal();
    	
    	GUILayout.Label("Light");
    	EditorGUILayout.BeginHorizontal();
    		if(GUILayout.Button(GUIContent("Directional", "Creates a directional light"), GUILayout.Width(75), GUILayout.Height(20))){
    			EditorApplication.ExecuteMenuItem("GameObject/Light/Directional Light");
    		}
    		if(GUILayout.Button(GUIContent("Spot", "Creates a spotlight"), GUILayout.Width(50), GUILayout.Height(20))){
    			EditorApplication.ExecuteMenuItem("GameObject/Light/Spotlight");
    		}
    		if(GUILayout.Button(GUIContent("Point", "Creates a point light"), GUILayout.Width(50), GUILayout.Height(20))){
    			EditorApplication.ExecuteMenuItem("GameObject/Light/Point Light");
    		}
    		if(GUILayout.Button(GUIContent("Area", "Creates and area light"), GUILayout.Width(50), GUILayout.Height(20))){
    			EditorApplication.ExecuteMenuItem("GameObject/Light/Area Light");
    		}
    	EditorGUILayout.EndHorizontal();
    	
    	GUILayout.Label("UI");
    	EditorGUILayout.BeginHorizontal();
    		if(GUILayout.Button(GUIContent("Canvas", "Creates a canvas"), GUILayout.Width(55), GUILayout.Height(20))){
    			EditorApplication.ExecuteMenuItem("GameObject/UI/Canvas");
    		}
    		if(GUILayout.Button(GUIContent("Button", "Creates a button"), GUILayout.Width(52), GUILayout.Height(20))){
    			EditorApplication.ExecuteMenuItem("GameObject/UI/Button");
    		}
    		if(GUILayout.Button(GUIContent("Txt", "Creates a text"), GUILayout.Width(32), GUILayout.Height(20))){
    			EditorApplication.ExecuteMenuItem("GameObject/UI/Text");
    		}
    		if(GUILayout.Button(GUIContent("Panel", "Creates a panel"), GUILayout.Width(47), GUILayout.Height(20))){
    			EditorApplication.ExecuteMenuItem("GameObject/UI/Panel");
    		}
    		if(GUILayout.Button(GUIContent("Img", "Creates an image"), GUILayout.Width(35), GUILayout.Height(20))){
    			EditorApplication.ExecuteMenuItem("GameObject/UI/Image");
    		}
    	EditorGUILayout.EndHorizontal();
    	
    	GUI.DragWindow();
    	createWindow.x = Mathf.Clamp(createWindow.x, 0, Screen.width - createWindow.width); 
    	createWindow.y = Mathf.Clamp(createWindow.y, 0, Screen.height - createWindow.height - 22);
    }
    
    function WindowEditor (id : int)
    {	
    	EditorGUILayout.BeginHorizontal();
    		if(GUILayout.Button(GUIContent("Save", "Saves the current scene and assets"), GUILayout.Width(50), GUILayout.Height(25))){
    			AssetDatabase.SaveAssets();
    			EditorApplication.SaveScene();   
    			GetScenes();			
    		}
    		if(GUILayout.Button(GUIContent("New Scene", "Creates a new scene"), GUILayout.Width(87), GUILayout.Height(25))){
    			EditorApplication.NewScene();	
    			GetScenes();
    		}
    	EditorGUILayout.EndHorizontal();
    	   	
    	GUILayout.Label("<b> Change Scene</b>", style);    	
    	GUILayout.Space(3);	
    	
    	EditorGUILayout.BeginHorizontal();
    	scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Width(140), GUILayout.Height(85));
    		if(scenes[0] != null){
	    		for(var x : int = 0; x < scenes.Length; x ++){
	    			var buttonName : String = scenes[x];
	    				buttonName = buttonName.Replace("Assets/Scenes/", "");
	    				buttonName = buttonName.Replace(".unity", "");
	    			if(GUILayout.Button(GUIContent(buttonName, "Opens the scene '" + buttonName + "'"), GUILayout.Width(110), GUILayout.Height(20))){
	    				AssetDatabase.SaveAssets();
	    				EditorApplication.SaveScene();   			
	    				EditorApplication.OpenScene(scenes[x]);
	    			}
	    		}
	    	}
    	EditorGUILayout.EndScrollView();  	
    	EditorGUILayout.EndHorizontal();
    	
    	GUI.DragWindow();
    	editorWindow.x = Mathf.Clamp(editorWindow.x, 0, Screen.width - editorWindow.width); 
    	editorWindow.y = Mathf.Clamp(editorWindow.y, 0, Screen.height - editorWindow.height - 22);
    }
    
    function WindowMenus (id : int)
    {
    	EditorGUILayout.BeginVertical();
    		if(GUILayout.Button(GUIContent("Physics 2D", "Navigates to the Physics 2D menu"))){
    			EditorApplication.ExecuteMenuItem("Edit/Project Settings/Physics 2D");
    		}
    		if(GUILayout.Button(GUIContent("Input", "Navigates to the Input menu"))){
    			EditorApplication.ExecuteMenuItem("Edit/Project Settings/Input");
    		}
    		if(GUILayout.Button(GUIContent("Tags & Layers", "Navigates to the Tags and Layers menu"))){
    			EditorApplication.ExecuteMenuItem("Edit/Project Settings/Tags and Layers");
    		}
    		if(GUILayout.Button(GUIContent("Player", "Navigates to the Player menu"))){
    			EditorApplication.ExecuteMenuItem("Edit/Project Settings/Player");
    		}
    		if(GUILayout.Button(GUIContent("Quality", "Navigates to the Quality menu"))){
    			EditorApplication.ExecuteMenuItem("Edit/Project Settings/Quality");
    		}
    	EditorGUILayout.EndVertical();
    	
    	GUI.DragWindow();
    	menusWindow.x = Mathf.Clamp(menusWindow.x, 0, Screen.width - menusWindow.width); 
    	menusWindow.y = Mathf.Clamp(menusWindow.y, 0, Screen.height - menusWindow.height - 22);
    }
    
    function Toolbar (id : int)
    {
    	if(GUILayout.Button(GUIContent("Reset Sections", "Resets the sections to their original position"), GUILayout.Height(30))){
    		selectedObjectsWindow = Rect(0,0,100,150);
	 		snappingWindow = Rect(100,0,100,150);
	 		createWindow = Rect(200,0,250,150);
	 		editorWindow = Rect(450,0,150,150);
	 		menusWindow = Rect(600,0,150,150);
	 		toolbarWindow = Rect(750,0,150,150);
    	}
    	if(GUILayout.Button(GUIContent("Help", "Brings up the help menu"), GUILayout.Height(30))){
    		EditorWindow.GetWindow(Toolbar2DHelp);
    	}
    	
    	GUI.DragWindow();
    	toolbarWindow.x = Mathf.Clamp(toolbarWindow.x, 0, Screen.width - toolbarWindow.width); 
    	toolbarWindow.y = Mathf.Clamp(toolbarWindow.y, 0, Screen.height - toolbarWindow.height - 22);
    }
    
    function GetScenes ()
    {
    	scenes = new String[EditorBuildSettings.scenes.Length];
    	
    	for(var x : int = 0; x < EditorBuildSettings.scenes.Length; x ++){
    		scenes[x] = EditorBuildSettings.scenes[x].path;
    	}
    }
}

public class Toolbar2DHelp extends EditorWindow
{
	var style : GUIStyle = new GUIStyle();;
	var scrollPos : Vector2;
	
	@MenuItem ("Window/Toolbar 2D Help") 	
    static function InitHelp () 														
    {
        EditorWindow.GetWindow(Toolbar2DHelp);														
        EditorWindow.GetWindow(Toolbar2DHelp).minSize = new Vector2(300, 400);  			
    }
    
    function OnGUI ()
    {	    	
    	scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
    		GUILayout.Space(5);
    		GUILayout.Label("<b><size=15> Toolbar 2D Help</size></b>", style);
    		GUILayout.Space(5);
    		GUILayout.Label("<b> About</b>", style);
    		GUILayout.Label("Toolbar2D is a utility that can assist anyone in \ncreating 2D games. It will save you from having \nto navigate through multiple tabs to comnplete \na simple action, and make creating a 2D game \neasier.");   	
    	
    		GUILayout.Label("<b> Sections</b>", style);
    		GUILayout.Label("Selected Objects\n- Center the selected objects at 0,0 with the 'Center' button.\n- Center the selected objects at the scene camera position with the 'Center Scene' button.\n\nSnapping\n- Turn snapping on and off on the X and/or Y axis.\n- Choose snapping incriments on the X and/or Y axis.\n\nCreate\n- Create common objects used when making a 2D game.\n\nEditor\n- You can save the scene and assets with the 'Save' button.\n- You can create a new scene with the 'New Scene' button.\n- The 'Change Scene' area displays all of your scenes in the file path: 'Assets/Scenes'.\n\nMenus\n- You can easily navigate to different menus from here with a click of a button.\n\nToolbar 2D\n- Reset sections positions with the 'Reset Sections' button.\n- Bring up the help menu with the 'Help' button.");
    		GUILayout.Space(10);
    		GUILayout.Label(" Contact me at: <b>buckleydaniel101@gmail.com</b>", style);
    	EditorGUILayout.EndScrollView();
    	style.richText = true;
    }
}