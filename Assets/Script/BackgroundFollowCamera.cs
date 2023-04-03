using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundFollowCamera : MonoBehaviour
{
	public Transform Target;        //the target the camera follows
	private Vector2 velocity;       //velocity of the camera

	void Awake()
	{
	}

	void LateUpdate()
	{
		Vector3 temp = transform.position;

		temp.x = Target.position.x;
		temp.y = Target.position.y;
		transform.position = temp;
	}
}
