using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceStationRotate : MonoBehaviour {
	private Transform tform;
	public float rotationspeed;

	void Start () {
		tform = GetComponent<Transform>();
		
	}
	
	// Update is called once per frame
	void Update () {
		tform.Rotate(0,Time.deltaTime*rotationspeed,0);
		
	}
}
