using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
	public Transform target; // camera follows this target
	public float smoothing = 5f; // camera has some lag before it follows

	Vector3 offset; // distance between camera and player

	void Start ()
	{
		offset = transform.position - target.position; // stores distance between camera's transform and target's transform
	}

	void FixedUpdate ()
	{
		Vector3 targetCamPos = target.position + offset; // position of camera after player moves to a different position
		transform.position = Vector3.Lerp (transform.position, targetCamPos, smoothing * Time.deltaTime);
	}
}
