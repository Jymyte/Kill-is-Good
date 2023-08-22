using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullsEye : MonoBehaviour
{
	[SerializeField] float[] targetRadii;
	[SerializeField] Transform[] bullseyeParticles;
	[SerializeField] MeshRenderer bullseyeMeshRenderer;
	[SerializeField] Collider bullseyeCollider;
	bool targetReady = true;
	/* void Start()
	{
		
	} */

	// Update is called once per frame
	void OnCollisionEnter(Collision collision)
	{
		if(targetReady && collision.gameObject.CompareTag("Projectile"))
		{
			foreach (ContactPoint contact in collision.contacts)
			{
				Debug.DrawRay(contact.point, contact.normal, Color.white);

				for (int i = 0; i < targetRadii.Length; i++)
				{
					float distanceToTarget = (contact.point - transform.position).magnitude;

					if(distanceToTarget < targetRadii[i])
					{
						//Debug.Log($"Hit point within target radius of {targetRadii[i]} at {distanceToTarget}, radius index: {i}.");

						Instantiate(bullseyeParticles[i], contact.point, Quaternion.LookRotation(contact.normal));

						CooldownOnTarget();
						Debug.DrawLine(contact.point, transform.position, Color.cyan, 5f);
						break;
					}
					else
					{
						//Debug.Log($"Hit but missed target radius of {targetRadii[i]} at {distanceToTarget}");
						Debug.DrawLine(contact.point, transform.position, Color.black, 5f);
					}
				}
			}
		}
	}


	void CooldownOnTarget()
	{
		bullseyeMeshRenderer.enabled = false;
		targetReady = false;
		Invoke("CooldownOffTarget", 1f);
	}
	void CooldownOffTarget()
	{
		bullseyeMeshRenderer.enabled = true;
		targetReady = true;

	}
}
