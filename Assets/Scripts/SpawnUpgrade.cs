using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnUpgrade : MonoBehaviour {

	public GameObject doubleJump;
	public GameObject fireboltPickup;
	public GameObject player;

	void Start()
	{
		player = GameObject.Find ("Telekinetic2(Clone)");
	}

	public void SpawnDoubleJump()
	{
		Instantiate (doubleJump, player.transform.position, Quaternion.identity);
	}

	public void SpawnFireboltPickup()
	{
		Instantiate (fireboltPickup, player.transform.position, Quaternion.identity);
	
	}

}
