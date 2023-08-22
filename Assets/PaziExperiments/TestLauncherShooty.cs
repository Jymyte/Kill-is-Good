using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class TestLauncherShooty : MonoBehaviour
{
	[Header("Modify these")]
	[SerializeField] bool visualizeShootDirection;
	[SerializeField] bool shootFromViewpoint;
	[SerializeField] float shootVelocity = 1f;
	[SerializeField] Animator gunAnimator;
	[SerializeField] Rigidbody gunProjectile;
	[SerializeField] Transform gunParticles;
	[SerializeField] Transform gunMeshTransform;
	[SerializeField] Transform gunShootPoint;
	[SerializeField] Transform playerViewpoint;
	[SerializeField] TestCrosshair crosshairs;
	[SerializeField] float maximumAngleCompensation = 20f;
	[SerializeField] AnimationCurve lookShootAngleCompensationCurve;

	[Header("Don't modify these")]
	[SerializeField] float lookShootAngleCompensation;
	[SerializeField] float normalizedCompensation;
	[SerializeField] float lookShootAngleCompensationExp;
	[SerializeField] Vector3 lookShootCompensatedDirection;
	[SerializeField] Vector3 crosshairWorldPosition;
	bool crosshairChanged;
	bool readyToFire = true;
	//Quaternion gunMeshStartRotation;
	
	//float handedness;

	/* void Start()
	{
		//gunMeshStartRotation = gunMeshTransform.rotation;
	} */

	void Update()
	{
		
		if(Input.GetMouseButton(0) && readyToFire)
		//if(Input.GetMouseButton(0) && gunAnimator.GetCurrentAnimatorStateInfo(0).IsName("GunIdleBlendTree"))
		{
			Fire();
			readyToFire = false;
		}

		if(Input.mouseScrollDelta.y != 0f)
		{
			lookShootAngleCompensation += Input.mouseScrollDelta.y;
			lookShootAngleCompensation = Mathf.Clamp(lookShootAngleCompensation, 0f, maximumAngleCompensation);

			//float lookShootAngleCompensationExp = Mathf.Pow(lookShootAngleCompensation/5f, 2f) + (Mathf.Sign(lookShootAngleCompensation) * 0.2f); 
			/* float */ normalizedCompensation = math.remap(0f, maximumAngleCompensation, 0f, 1f, lookShootAngleCompensation);
			lookShootAngleCompensationExp = lookShootAngleCompensationCurve.Evaluate(normalizedCompensation)*maximumAngleCompensation;

			//lookShootAngleCompensationExp = lookShootAngleCompensation; //yeet

			crosshairChanged = true;
			//crosshairs.UpdateCrosshairCompensator(lookShootAngleCompensationExp);
		}
		
	}

	public void ReadyWeapon() //Called from animation event script
	{
		readyToFire = true;
	}

	void LateUpdate()
	{
		lookShootCompensatedDirection = Quaternion.AngleAxis(-lookShootAngleCompensationExp, transform.right) * playerViewpoint.forward; 

		if(crosshairChanged)
		{
			crosshairWorldPosition = transform.position + lookShootCompensatedDirection * 50f;
			crosshairs.CrosshairFromWorldPosition(crosshairWorldPosition);

			//gunMeshTransform.rotation =  transform.rotation * Quaternion.AngleAxis(-lookShootAngleCompensationExp, transform.right);
			
			crosshairChanged = false;
		}
	}

	void Fire()
	{
		//lookShootCompensatedDirection = Quaternion.AngleAxis(-lookShootAngleCompensationExp, transform.right) * lookShootDirection.forward;
		if(visualizeShootDirection)
		{
			Debug.DrawRay(gunShootPoint.position, lookShootCompensatedDirection * 200f, Color.yellow, 5f, true);
		}

		gunAnimator.SetTrigger("Shoot");
		Instantiate(gunParticles, gunShootPoint.position, gunShootPoint.rotation, gameObject.transform);

		if(shootFromViewpoint) 
		{
			Rigidbody newProjectile = Instantiate<Rigidbody>(gunProjectile, playerViewpoint.position, Quaternion.LookRotation(lookShootCompensatedDirection, Vector3.up));
			newProjectile.velocity = lookShootCompensatedDirection * shootVelocity;
			TestLauncherSlug projectileScript = newProjectile.GetComponent<TestLauncherSlug>(); 
			projectileScript.InitializeParallaxProjectile(gunShootPoint.position);
		}
		else
		{
			Rigidbody newProjectile = Instantiate<Rigidbody>(gunProjectile, gunShootPoint.position, Quaternion.LookRotation(lookShootCompensatedDirection, Vector3.up));
			newProjectile.velocity = lookShootCompensatedDirection * shootVelocity;
			//TestLauncherSlug projectileScript = newProjectile.GetComponent<TestLauncherSlug>(); 
		}
		
		

	}

	/* void OnDrawGizmos()
	{
		Gizmos.color = Color.black;
		Gizmos.DrawSphere(crosshairWorldPosition, 0.5f);
	} */
}
