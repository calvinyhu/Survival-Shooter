using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float speed = 6f;

	Vector3 movement;
	Animator anim;
	Rigidbody playerRigidbody;
	int floorMask;
	float camRayLength = 100f;

	void Awake() // starts whethere or not script is enable (unlike Start()) Awake is always called before Start
	{
		floorMask = LayerMask.GetMask ("Floor");
		anim = GetComponent<Animator>();
		playerRigidbody = GetComponent<Rigidbody>();
	}

	void FixedUpdate() // for all physics updates, fires all physics updates, rigidbodies
	{
		// snaps to max speed, more responsive feel i.e. -1, 0, or 1. NOT GetAxis which is [-1, 1]
		// "Horizontal" maps to "A", "D", "left", "right" directional keys.
		// Edit > Project Settings > Input > Axes
		// Pressing A is a value of -1, D is a value of 1.
		float h = Input.GetAxisRaw("Horizontal");
		float v = Input.GetAxisRaw("Vertical");

		Move (h, v);
		Turning();
		Animating (h, v);
	}

	void Move(float h, float v)
	{
		movement.Set(h, 0f, v);

		movement = movement.normalized * speed * Time.deltaTime; // so that the diagonal vector doesnt move at 1.4

		playerRigidbody.MovePosition (transform.position + movement);
	}

	void Turning() // uses ray casting
	{
		// creating a Ray variable called camRay which sets the hit point of the Main Camera to the mouse position
		// ScreenPointToRay casts a point from the screen forwards into the scene. The point is the mouse position
		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);

		// gets information from raycast, we need raycast hit variable
		RaycastHit floorHit;

		// "out" gets information from Raycast and stores it into "floorHit"
		// if there is a hit from the mouse point to the area of the floorMask (quad), then execute turning
		if (Physics.Raycast (camRay, out floorHit, camRayLength, floorMask))
		{
			Vector3 playerToMouse = floorHit.point - transform.position;
			playerToMouse.y = 0f;

			// cannot set a players rotation based on a vector
			// quarternion - a way to store a rotation
			Quaternion newRotation = Quaternion.LookRotation (playerToMouse);
			playerRigidbody.MoveRotation (newRotation);
		}
	}

	void Animating (float h, float v)
	{
		bool walking = h != 0f || v != 0f; // did we press horizontal axis or vertical axis? if we did we are walking
		anim.SetBool ("IsWalking", walking);
	}
}




















