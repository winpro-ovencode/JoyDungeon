using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
	public float ScrollSpeed = 0.2F;    // speed to scroll the texture
	public bool IsHorizontalScroll;     // determines if the parallax should be Horizontal
	public bool IsVerticalScroll;       // determines if the parallax should be Vertical

	public Camera TheCam;               // reference to the camera the texture follows

	private Transform CamTransform;     // ref to the camera's transform
	private Material TextureMaterial;   // the material that is set to repeat itself


	void Awake()
	{
	}

	void Start()
	{
		if (GetComponent<Renderer>().material == null)
		{
			Debug.LogError("There is no texture attached. Please assign one in the inspector.");
		}
		else
		{
			// get the texture that will be wrapped
			TextureMaterial = GetComponent<Renderer>().material;
		}

		if (TheCam == null)
		{
			Debug.LogError("There is no camera attached. Please assign one in the inspector.");
		}
		else
		{
			//get the reference to the cameras transform
			CamTransform = TheCam.transform;
		}

	}

	void LateUpdate()
	{
		if (IsHorizontalScroll)
		{
			//get the cam's pos & use it to determine offset
			Vector3 theOffset = (CamTransform.localPosition * ScrollSpeed);
			//set the offset in the texture's material
			TextureMaterial.SetTextureOffset("_MainTex", theOffset);
		}


		if (IsVerticalScroll)
		{
			//get the cam's pos & use it to determine offset
			Vector3 theOffset = (CamTransform.localPosition * ScrollSpeed);
			//set the offset in the texture's material
			TextureMaterial.SetTextureOffset("_MainTex", theOffset);
		}
	}
}
