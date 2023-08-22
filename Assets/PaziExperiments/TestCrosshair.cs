using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Mathematics;

public class TestCrosshair : MonoBehaviour
{
	[SerializeField] Image compensatorImage;
	[SerializeField] float compensatorYPosition;
	[SerializeField] int decimals = 3;
	[SerializeField] RectTransform canvasRect;
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
	public void CrosshairFromWorldPosition(Vector3 worldPosition)
	{
		Vector2 viewportPoint=Camera.main.WorldToViewportPoint(worldPosition);
		float verticalPoint = (viewportPoint.y*canvasRect.sizeDelta.y)-(canvasRect.sizeDelta.y*0.5f);
		//verticalPoint = verticalPoint*decimals;
		verticalPoint = (Mathf.Round(verticalPoint*decimals))/decimals;
		//verticalPoint = verticalPoint/decimals;
		
		Vector2 WorldObject_ScreenPosition=new Vector2(
		(0f),
		((verticalPoint)));
		/* Vector2 WorldObject_ScreenPosition=new Vector2(
		((viewportPoint.x*canvasRect.sizeDelta.x)-(canvasRect.sizeDelta.x*0.5f)),
		((viewportPoint.y*canvasRect.sizeDelta.y)-(canvasRect.sizeDelta.y*0.5f))); */

		//now you can set the position of the ui element
		compensatorImage.rectTransform.anchoredPosition=WorldObject_ScreenPosition;
	}
}
