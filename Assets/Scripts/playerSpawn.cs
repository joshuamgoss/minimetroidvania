using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerSpawn : MonoBehaviour {

	public GameObject player;
	public Transform spawnSite;

	void Awake () {
		spawnSite = GameObject.Find("SpawnSite").transform;
		Instantiate(player, spawnSite.position, Quaternion.Euler(0,0,0));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
