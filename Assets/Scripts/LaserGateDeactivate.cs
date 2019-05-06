using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGateDeactivate : MonoBehaviour {

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag== "ProjectileFire")
		{
			other.gameObject.SetActive(false);
		}
	}
}
