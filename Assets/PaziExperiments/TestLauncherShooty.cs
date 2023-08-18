using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class TestLauncherShooty : MonoBehaviour
{
	[SerializeField] float shootVelocity = 1f;
	[SerializeField] Animator gunAnimator;
	[SerializeField] Rigidbody gunProjectile;
	[SerializeField] Transform gunParticles;
	[SerializeField] Transform gunShootPoint;
	[SerializeField] Transform lookShootDirection;
	[SerializeField] TestCrosshair crosshairs;
	[SerializeField] float maximumAngleCompensation = 20f;
	[SerializeField] float lookShootAngleCompensation;
	[SerializeField] float normalizedCompensation;
	[SerializeField] float lookShootAngleCompensationExp;
	[SerializeField] AnimationCurve lookShootAngleCompensationCurve;
	
	//float handedness;

	/* void Start()
	{
		
	} */

	void Update()
	{
		if(Input.GetMouseButtonDown(0) && gunAnimator.GetCurrentAnimatorStateInfo(0).IsName("GunIdleBlendTree"))
		{
			Fire();
		}

		if(Input.mouseScrollDelta.y != 0f)
		{
			lookShootAngleCompensation += Input.mouseScrollDelta.y;
			lookShootAngleCompensation = Mathf.Clamp(lookShootAngleCompensation, 0f, maximumAngleCompensation);

			//float lookShootAngleCompensationExp = Mathf.Pow(lookShootAngleCompensation/5f, 2f) + (Mathf.Sign(lookShootAngleCompensation) * 0.2f); 
			/* float */ normalizedCompensation = math.remap(0f, maximumAngleCompensation, 0f, 1f, lookShootAngleCompensation);
			lookShootAngleCompensationExp = lookShootAngleCompensationCurve.Evaluate(normalizedCompensation)*maximumAngleCompensation;

			crosshairs.UpdateCrosshairCompensator(lookShootAngleCompensationExp);

		}
		
	}

	void Fire()
	{
		Debug.DrawRay(gunShootPoint.position, Quaternion.AngleAxis(-lookShootAngleCompensation, transform.right) * lookShootDirection.forward * 20f, Color.yellow, 5f, true);

		gunAnimator.SetTrigger("Shoot");
		Rigidbody newProjectile = Instantiate<Rigidbody>(gunProjectile, gunShootPoint.position, Quaternion.AngleAxis(-lookShootAngleCompensation, transform.right));
		newProjectile.velocity = Quaternion.AngleAxis(-lookShootAngleCompensation, transform.right) * lookShootDirection.forward * shootVelocity;
		Instantiate(gunParticles, gunShootPoint.position, gunShootPoint.rotation, gameObject.transform);

	}
}
