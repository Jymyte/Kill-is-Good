using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLauncherShooty : MonoBehaviour
{
	[SerializeField] float shootVelocity = 1f;
	[SerializeField] Animator gunAnimator;
	[SerializeField] Rigidbody gunProjectile;
	[SerializeField] Transform gunParticles;
	[SerializeField] Transform gunShootPoint;

	/* void Start()
	{
		
	} */

	void Update()
	{
		if(Input.GetMouseButtonDown(0))
		{
			Fire();
		}
		
	}

	void Fire()
	{
		gunAnimator.SetTrigger("Shoot");
		Rigidbody newProjectile = Instantiate<Rigidbody>(gunProjectile, gunShootPoint.position, gunShootPoint.rotation);
		newProjectile.velocity = gunShootPoint.forward * shootVelocity;
	}
}
