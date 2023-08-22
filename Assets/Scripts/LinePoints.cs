using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinePoints : MonoBehaviour
{
	LineRenderer lineRenderer;
	[SerializeField] Transform attachedTransformA;
	[SerializeField] Transform attachedTransformB;
	[SerializeField] Vector3[] points;
	void Start()
	{
		lineRenderer = GetComponent<LineRenderer>();
	}
	void Update()
	{
		lineRenderer.positionCount = points.Length;
		lineRenderer.SetPosition(0, attachedTransformA.TransformPoint(points[0]));
		lineRenderer.SetPosition(1, attachedTransformB.TransformPoint(points[1]));
	}
	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.white;
		Gizmos.DrawLine(attachedTransformA.TransformPoint(points[0]), attachedTransformB.TransformPoint(points[1]));
		Gizmos.color = Color.gray;
		Gizmos.DrawWireSphere(attachedTransformA.TransformPoint(points[0]), .05f);
		Gizmos.DrawWireSphere(attachedTransformB.TransformPoint(points[1]), .05f);
	}
}
