using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireShot : MonoBehaviour {

	public float speed;
	private Rigidbody rb;
	private Vector3 startingloc;
	public float range;



	void Start ()
	{
		rb = GetComponent<Rigidbody>();
		rb.velocity = transform.up * speed;
		startingloc = transform.position;
	}

	private void Update()
	{
		if (Vector3.Distance(startingloc, transform.position) > range)
		{
			Destroy(gameObject);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
        if (other.tag == "FireDoor")
        {
            other.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
        else
            return;
	}

}
