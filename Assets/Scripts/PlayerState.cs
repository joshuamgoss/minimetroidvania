using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerState : MonoBehaviour {

	Scene scene;

	public static float fireRate;
	public static bool fireboltEnabled;
	public static float speedxy;
	public static float speedjump;
	public static float strafeEffect;
	public static int numberJumpsPermitted;

	public float FireRate { get; set; }
	public bool FireboltEnabled { get; set; }
	public float Speedxy { get; set; }
	public float Speedjump { get; set; }
	public float StrafeEffect { get; set; }
	public int NumberJumpsPermitted { get; set; }

	void Start () {
		DontDestroyOnLoad(this);
		FireRate = 0.2f;
		Speedxy = 7;
		FireboltEnabled = false;
		Speedjump = 6;
		StrafeEffect = 0.7f;
		NumberJumpsPermitted = 1;
	}
	
	void Update () {
		scene = SceneManager.GetActiveScene();
		if (scene.name == "Initialization Scene")
		{
			SceneManager.LoadScene("MainMenu");
		}
	}
}
