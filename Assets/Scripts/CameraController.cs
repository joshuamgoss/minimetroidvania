using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{

	public Vector3 camOffsetPlacement; //placement of the camera relative to the player
	private Vector3 offset;
	private float currenty;
	private float targety;
	private float disty;
	private float yOffset;
	public GameObject player;
	private float yincrement;
	private float camYSpeed;
	private bool centerCameraLoop;
	public bool stabelizey;

	void Start()
	{
		player = GameObject.Find("Telekinetic2(Clone)");
		transform.position = player.transform.position+camOffsetPlacement;
		yincrement = .1f; 
		camYSpeed = 160;
	}

	void LateUpdate()
	{

		currenty = transform.position.y;
		targety = playercontroller.lastSurface + camOffsetPlacement.y;
		disty = targety - currenty;

		/*This makes it so that if stabelizey is true, the y axis of the camera only moves if a tolerance is exceeded, then it recenters the camera.  It was an experiment.  Maybe it would reduce nausea in VR?
		 * at any rate, the else piece makes it a more standard camera
		 */

		if (stabelizey == true)
		{
			if (Mathf.Abs(disty) > 15 * yincrement)
			{
				centerCameraLoop = true;
			}
			else if (Mathf.Abs(disty) < 2 * yincrement)
			{
				centerCameraLoop = false;
			}


			if (centerCameraLoop == true)
			{
				yOffset = (Mathf.Abs(disty) / disty) * yincrement * Time.deltaTime * camYSpeed;
			}
			else
			{
				yOffset = 0;
			}
			transform.position = new Vector3(player.transform.position.x + camOffsetPlacement.x, currenty + yOffset, player.transform.position.z + camOffsetPlacement.z);
		}
		else
		{
			transform.position=new Vector3(0f,0f,0f)+(player.transform.position+camOffsetPlacement);
		}




	}
}
//playercontroller.lastSurface + offset.y