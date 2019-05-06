using UnityEngine;
using System.Collections;

/*To get the character working:
 * Capsule collider: center (0,1.1,0) radius 0.29, height 2.2
 * Sphere collider at feet with .25 radius set to trigger
 * character tag 'player'
 * rigidbody with x and z rotations frozen
 * surfaces tagged 'platform'
 * 
 * drag self to 'Animator' in inspector pain for this script
 * drag DoubleJumpIndicator off the ORG-thigh.L rig piece into Doublejumpindicator in the inpsector for this script
 * drag FireboltIndicator off of ORG-shoulder.R into FireboltIndicator slot in the inspector for this script
 * drag Laser Bolt prefab into Laser Blast slot in the inspector for this script
 * 
 * for this script in the inspector, set speedxy, speedjump, strafeeffect, Fire Rate and numberofjumps.
 * add "Aim" and "Strafe" to the input manager
 */

/*The following values should be used in the animator:
 * bool "jumping" to detect when the character is jumping
 * bool "strafing" when the character is strafing
 * bool "running" when the character is running but not strafing
 * int "strafedirection" sends 0-forward 90-right 180-back 270-left
 * 
 * (update when things are added)
 */


public class playercontroller : MonoBehaviour {

	public GameObject gameController;
	public PlayerState playerState;
	private Rigidbody rb;
	private Transform tform;
	private AudioSource allAudio;
	private float movex;
	private Vector3 moveX;
	private float movey;
	private Vector3 moveY;
	private Vector3 horizontalMovement;
	private float movez;
	private Vector3 moveZ;
	private Vector3 movement;
	private float runspeed;
	private float strafespeed;
	private bool strafing;
	private float strafedirection;
	private float moveDirection;
	private bool movexy;
	private bool onGround;
	private bool running;
	public bool fireboltEnabled=false;
	private float nextFire=0f;


	public int jumpsLeft;
	public float fireRate=0.2f;
	public float lookDirection;
	public Animator animator;
	public float speedxy=7f;
	public float speedjump=6f;
	public float strafeEffect=0.7f;
	public int numberJumpsPermitted=1;
	public GameObject doublejumpindicator;
	public GameObject fireboltIndicator;
	public GameObject laserBlast;
    public float raycastRange;
    public Transform lockedOnObject;

	public static float lastSurface;


	void Start ()
	{
		rb = GetComponent<Rigidbody>();
		tform = GetComponent<Transform>();
		animator.GetComponent<Animator>();
		gameController = GameObject.Find("GameController");
		playerState = gameController.GetComponent<PlayerState>();
		onGround = true;
		fireRate = playerState.FireRate;
		fireboltEnabled = playerState.FireboltEnabled;
		speedxy = playerState.Speedxy;
		speedjump = playerState.Speedjump;
		strafeEffect = playerState.StrafeEffect;
		numberJumpsPermitted = playerState.NumberJumpsPermitted;

		doublejumpindicator.SetActive(false);
		fireboltIndicator.SetActive(false);

		if (fireboltEnabled == true)
		{
			fireboltIndicator.SetActive(true);
		}
		if (numberJumpsPermitted >= 2)
		{
			doublejumpindicator.SetActive(true);
		}
	}

	void Update ()
	{
		movez = Input.GetAxis("Vertical");    //these will be used by LookDirection() and MoveController().  They have to come before those are called
		movex = Input.GetAxis("Horizontal");  //these will be used by LookDirection() and MoveController().  They have to come before those are called



		if (Input.GetButton("Strafe") == false)  //Locks the rotation if the player holds the strafe button
		{
            if ((Input.GetAxis("Horizontal")!=0) || (Input.GetAxis("Vertical")!=0)) {
				LookDirection ();
			}
		}

		if (Input.GetButton("Aim") == false)  //keeps character from moving if the player holds the look button
		{
			MoveController();
		}
        else
        {
            rb.velocity = rb.velocity - new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            animator.SetBool("run", false);
        }
		WeaponsSystem();
	
	}

	private void WeaponsSystem()
	{
		if ((Input.GetKeyDown("q")==true&&(fireboltEnabled == true) && (Time.time > nextFire)))
		{
			nextFire = Time.time + fireRate;
			Instantiate(laserBlast, fireboltIndicator.transform.position, tform.rotation*Quaternion.Euler(90,0f,0f));
		}

	}

	private void MoveController()
	{
		moveZ = new Vector3(0f, 0f, movez);
		moveX = new Vector3(movex, 0f, 0f);
		strafespeed = speedxy * strafeEffect;

		if (Input.GetButton("Strafe")==true)
		{
			horizontalMovement = Vector3.Normalize(moveZ + moveX) * strafespeed; //this line takes the horizontal value, adds it to the vertical value, and makes the vector magnitude 1, then gives it running speed
			strafing = (Mathf.Abs(movex) + Mathf.Abs(movez) != 0); //this checks to see if there is movement so that strafing animations don't play when the strafe key is held but the character is still
			if (movez >= 0)
			{
				moveDirection = Mathf.Atan(movex / movez) * 360 / (2 * Mathf.PI);
			}
			else if (movez < 0)
			{
				moveDirection = 180 + (Mathf.Atan(movex / movez) * 360 / (2 * Mathf.PI));
			}
			animator.SetBool("run", false);
			animator.SetBool("strafing", strafing);
			strafedirection = (moveDirection - transform.rotation.eulerAngles.y + 720) % 360;
			if (onGround == false) //this disables stafing animations in the air, whith conditions setting the strafe animation direction based on movement direction
			{
				animator.SetInteger("strafedirection", 1000);
				animator.SetBool("strafing", false);
			}
			else if ((strafedirection > 337.5) || (strafedirection < 22.5))
			{
				animator.SetInteger("strafedirection", 0);
			}
			else if (strafedirection <= 157.5)
			{
				animator.SetInteger("strafedirection", 90);
			}
			else if (strafedirection <= 202.5)
			{
				animator.SetInteger("strafedirection", 180);
			}
			else if (strafedirection <=337.5)
			{
				animator.SetInteger("strafedirection", 270);
			}
			else
			{
				animator.SetInteger("strafedirection", 1000);
			}


		}
		else
		{
			horizontalMovement = Vector3.Normalize(moveZ + moveX) * speedxy; //this line takes the horizontal value, adds it to the vertical value, and makes the vector magnitude 1, then gives it strafing speed
			running = (Mathf.Abs(movex) + Mathf.Abs(movez) != 0);
			animator.SetBool("run", running);
			animator.SetBool("strafing", false);
			animator.SetInteger("strafedirection", 1000);
		}

		movey = rb.velocity.y; //this grabs the vertical velocity off of the physics component to preserve vertical momentum if the jump isn't pushed

		if (Input.GetKeyDown ("space")) 
		{
			if (jumpsLeft > 0) 
			{
				movey = Input.GetAxis("Jump") * speedjump;
				jumpsLeft -= 1;

				if (onGround == false)
				{
					animator.SetBool("secondjump", true);
				}
			}

		}

		moveY = new Vector3(0f, movey, 0f);
		movement = horizontalMovement + moveY; //this adds the horizontal and vertical motion together
		rb.velocity = movement;


	}

	private void LookDirection()
	{		
		if (movement.z >= 0)
		{
			lookDirection = Mathf.Atan(movex / movez) * 360 / (2 * Mathf.PI);
		}
		else if (movement.z < 0)
		{
			lookDirection = 180+(Mathf.Atan(movex / movez) * 360 / (2 * Mathf.PI));
		}
		rb.rotation = Quaternion.Euler(0f, lookDirection, 0f);

	}

	private void OnTriggerStay(Collider other)
	{
		if (other.tag == "platform")
		{
			onGround = true;
			animator.SetBool("jumping", false);
			jumpsLeft = numberJumpsPermitted;
			lastSurface = tform.position.y;
			animator.SetBool("secondjump", false);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag== "DoubleJump")
		{
			other.gameObject.SetActive(false);
			numberJumpsPermitted = 2;
			playerState.NumberJumpsPermitted = 2;
			doublejumpindicator.SetActive(true);
		} 
		else if (other.tag == "Firebolt")
		{
			fireboltEnabled = true;
			other.gameObject.SetActive(false);
			fireboltIndicator.SetActive(true);
			playerState.FireboltEnabled = true;
			
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "platform")
		{
			onGround = false;
			animator.SetBool("jumping", true);
			jumpsLeft -= 1;

		}
	}

    private void TargetLock()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, transform.forward, out hit);
        if (hit.collider.tag == "Enemy")
        {
            lockedOnObject = hit.transform;
        }
    }
}
