using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    public Vector2 Speed;
    public bool IsHorizontalScroll;
    public bool IsVerticalScroll;
    public SpriteRenderer Sprite;
    public Camera TargetCamera;

	GameObject thisGameObj;
	Transform camTransform;
	Material textureMaterial;

	void Start()
    {
		if (thisGameObj.GetComponent<Renderer>().material == null)
		{
			Debug.LogError("There is no texture attached. Please assign one in the inspector.");
		}
		else
		{
			// get the texture that will be wrapped
			textureMaterial = thisGameObj.GetComponent<Renderer>().material;
		}

		if (TargetCamera == null)
		{
			Debug.LogError("There is no camera attached. Please assign one in the inspector.");
		}
		else
		{
			//get the reference to the cameras transform
			camTransform = TargetCamera.transform;
		}
	}

    void Update()
    {
		if (IsHorizontalScroll)
		{
			//get the cam's pos & use it to determine offset
			Vector3 theOffset = (camTransform.localPosition * Speed);
			//set the offset in the texture's material
			textureMaterial.SetTextureOffset("_MainTex", theOffset);
		}


		if (IsVerticalScroll)
		{
			//get the cam's pos & use it to determine offset
			Vector3 theOffset = (camTransform.localPosition * Speed);
			//set the offset in the texture's material
			textureMaterial.SetTextureOffset("_MainTex", theOffset);
		}
	}
}
