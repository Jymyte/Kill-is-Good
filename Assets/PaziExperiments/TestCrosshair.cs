using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Mathematics;

public class TestCrosshair : MonoBehaviour
{
	[SerializeField] Image compensatorImage;
	[SerializeField] float compensatorYPosition;
	void Start()
	{
		compensatorYPosition = compensatorImage.rectTransform.position.y;
	}
	public void UpdateCrosshairCompensator(float compensationDegrees)
	{
		compensatorYPosition = compensationDegrees;
		
		float halfVerticalFov = Camera.main.fieldOfView / 2f;
		float halfScreenHeight = Screen.height / 2f;

		//from "0 to 40 degrees" to "0 to screen top edge from center"
		compensatorYPosition = math.remap(0f, halfVerticalFov, 0f, halfScreenHeight, halfVerticalFov + compensatorYPosition);

		compensatorImage.rectTransform.position = new Vector3
		(
			compensatorImage.rectTransform.position.x,
			compensatorYPosition,
			compensatorImage.rectTransform.position.z
		);
		
	}
}
