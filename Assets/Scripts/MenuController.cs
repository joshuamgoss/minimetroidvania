using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

	Scene scene;
	Scene oldScene;
	private bool menuState;
	private GameObject rootMenu;


	void Start () {
		rootMenu = GameObject.Find ("InGameMenu");
		rootMenu.SetActive (false);
		menuState = false;
		oldScene = SceneManager.GetActiveScene ();
	}


	void Update () 
	{
		SceneUpdate ();

		if (scene.name != "MainMenu") 
		{
			if (Input.GetKeyUp("m")==true) 
			{
				menuState = !menuState;
				rootMenu.SetActive (menuState);
					
				//if (menuState == false) {
				//	rootMenu.SetActive (true);
				//	menuState = true;
				//} else if (menuState == true) {
				//	rootMenu.SetActive (false);
				//	menuState = false;
			}
		}

	}
		

	private void SceneUpdate()
	{
		scene = SceneManager.GetActiveScene ();
		if (oldScene.name != scene.name) {
			rootMenu.SetActive (false);
			menuState = false;
		}
		oldScene = scene;
	}

}
