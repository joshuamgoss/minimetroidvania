using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PracticeTargetController : MonoBehaviour {

    private int damageTakenAmmount;
    private bool isAlive;
    private int velocityX;
    private int velocityZ;
    private Rigidbody rb;
    private float initTime;
    private Renderer rend;


    public int speedMax;
    public int health=1;
    public int attack;
    public float frequency;
    public Material[] materials;



	void Start () {
        isAlive = true;
        rb = GetComponent<Rigidbody>();
        initTime = Time.time - frequency;
        rend = GetComponent<Renderer>();
        rend.enabled=true;
        rend.material = materials[3];
	}
	

	void Update ()
    {
		if (Time.time > initTime + frequency)
        {
            Movement();
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ProjectileFire")
        {
            damageTakenAmmount = 8;
            health = health - damageTakenAmmount;
            if (health <= 0)
            {
                isAlive = false;
                gameObject.SetActive(false);
            }
            else if (health < 8)
            {
                rend.material = materials[2];
            }
            else if (health < 16)
            {
                rend.material = materials[1];
            }
            else
            {
                rend.material = materials[0];
            }
            Destroy(other.gameObject);
        }
        else
            return;
    }

    private void Movement()
    {
        velocityX = Random.Range(-speedMax, speedMax);
        velocityZ = Random.Range(-speedMax, speedMax);
        rb.velocity = new Vector3(velocityX, 0f, velocityZ);
        initTime = Time.time;
    }

}
