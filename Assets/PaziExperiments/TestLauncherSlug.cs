using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLauncherSlug : MonoBehaviour
{
	[SerializeField] bool visualizeProjectilePath;
	[SerializeField] float coolingSpeed = 1f;
	[SerializeField] float impactPowerThreshold;
	[SerializeField] ParticleSystem impactParticles;
	[SerializeField] float parallaxCorrectSpeed = 1f;
	[SerializeField] AnimationCurve parallaxCorrectCurve;
	[SerializeField] AnimationCurve parallaxCorrectCurveIfObstructed;
	[SerializeField] Transform slugVisuals;
	[SerializeField] ParticleSystem slugFlareParticles;
	[SerializeField] AnimationCurve flareSizeCurve;
	[SerializeField] ParticleSystem slugFireParticles;
	[SerializeField] AnimationCurve fireEmissionCurve;
	[SerializeField] ParticleSystem slugSmokeParticles;
	[SerializeField] AnimationCurve smokeEmissionCurve;
	[SerializeField] MeshRenderer slugMeshRenderer;

	[Header("Internal")]
	[SerializeField] float slugHeat;
	[SerializeField] float currentProjectileSpeed;
	float fireParticlesMaxEmissionOverTime;
	float fireParticlesMaxEmissionOverDistance;
	float smokeParticlesMaxEmissionOverTime;
	Color hotEmissionColor;
	Material slugMaterial;
	Vector3 lastPosition; //For storing position in last FixedUpdate, for debug visuals
	Mesh slugMesh;
	Vector3 projectileParallaxOffset;
	public float currentProjectileParallax = 0f;
	Rigidbody slugRb;	
	void Start()
	{
		slugRb = GetComponent<Rigidbody>();
		slugMesh = GetComponentInChildren<MeshFilter>().mesh; //Used for drawing a gizmo of the projectile for clarity

		lastPosition = transform.position; //Used to debug the projectile path 
		slugHeat = 1f; //Heat is a normalized range

		//Material emission glow control
		slugMaterial = slugMeshRenderer.GetComponent<MeshRenderer>().material; 
		hotEmissionColor = slugMaterial.GetColor("_EmissionColor");

		//Take emission values of particles as max
		fireParticlesMaxEmissionOverTime = slugFireParticles.emission.rateOverTimeMultiplier;
		fireParticlesMaxEmissionOverDistance = slugFireParticles.emission.rateOverDistanceMultiplier;
		smokeParticlesMaxEmissionOverTime = slugSmokeParticles.emission.rateOverTimeMultiplier;

		Destroy(gameObject, 20); //Pool party later
	}

	public void InitializeParallaxProjectile(Vector3 offset) //Called by the gun script when instantiating projectile if firing with parallax correction
	{
		currentProjectileParallax = 1f; //Current parallax should only start at 1 if firing with correction
		projectileParallaxOffset = offset - transform.position;
		projectileParallaxOffset = Vector3.ProjectOnPlane(projectileParallaxOffset, transform.forward);
		slugVisuals.position = transform.position + projectileParallaxOffset;

	}
	void OnCollisionEnter(Collision collision)
	{
		//Debug.Log($"{collision.impulse.magnitude}");
		//if(collision.impulse.magnitude > impactPowerThreshold)
		//{
			//Debug.DrawRay(collision.contacts[0].point, collision.contacts[0].normal, Color.white, 1.5f); //Visualize hit normal
			//Debug.DrawRay(transform.position, slugRb.velocity.normalized, Color.yellow, 1.5f); //Visualize new projectile travel direction
			//Debug.DrawRay(transform.position, Vector3.Slerp(collision.contacts[0].normal, slugRb.velocity.normalized, .5f), Color.magenta, 1.5f); //Visualize particles direction
			/* impactParticles.transform.position = collision.contacts[0].point;
			impactParticles.transform.rotation = Quaternion.LookRotation(Vector3.Slerp(collision.contacts[0].normal, slugRb.velocity.normalized, .5f), Vector3.up);
			impactParticles.Play(); */
			ParticleSystem impactSparks = Instantiate<ParticleSystem>(
				impactParticles, collision.contacts[0].point, 
				Quaternion.LookRotation(Vector3.Slerp(
					collision.contacts[0].normal, 
					slugRb.velocity.normalized, 
					.5f), 
				Vector3.up));
			//Debug.Break();
		//}

		for (int i = 0; i < collision.contacts.Length; i++)
		{
			//Debug.DrawRay(collision.contacts[i].point, collision.contacts[i].normal, Color.yellow, 1f);
			
		}
		/* foreach (ContactPoint contact in collision.contacts)
		{
			//Vector3 impulseForce = contact.impulse;
		}
		if (collision.relativeVelocity.magnitude > 2)
		{
			//audioSource.Play();

		} */
	}

	void Update()
	{
		if(slugHeat > 0f)
		{
			slugHeat = Mathf.MoveTowards(slugHeat, 0f, Time.deltaTime * coolingSpeed);

			float flareScale = flareSizeCurve.Evaluate(slugHeat);
			slugFlareParticles.transform.localScale = new Vector3(flareScale, flareScale, flareScale); //Shrink the flare

			var fireEmission = slugFireParticles.emission;
			fireEmission.rateOverTimeMultiplier = Mathf.Lerp(0f, fireParticlesMaxEmissionOverTime, fireEmissionCurve.Evaluate(slugHeat));
			fireEmission.rateOverDistanceMultiplier = Mathf.Lerp(0f, fireParticlesMaxEmissionOverDistance, fireEmissionCurve.Evaluate(slugHeat));
			//fireEmission.rateOverDistanceMultiplier = fireEmissionCurve.Evaluate(slugHeat);

			var smokeEmission = slugSmokeParticles.emission;
			smokeEmission.rateOverTimeMultiplier = Mathf.Lerp(0f, smokeParticlesMaxEmissionOverTime, smokeEmissionCurve.Evaluate(slugHeat));
			//smokeEmission.rateOverTimeMultiplier = smokeEmissionCurve.Evaluate(slugHeat);

			slugMaterial.SetColor("_EmissionColor", Color.Lerp(Color.black, hotEmissionColor, slugHeat));
		}

		if(currentProjectileParallax > 0f)
		{
			currentProjectileParallax = Mathf.MoveTowards(currentProjectileParallax, 0f, Time.deltaTime * parallaxCorrectSpeed);
			// Parallax correction may need to increase if the projectile slows down significantly or tumbles down or rolls to prevent visually slowly hovering to its true position
			// But it should instead be reduced to zero if the projectile gets abruptly lodged in a surface to make sure no visual hovering appears at all
			// This depends on the desired behaviour of the projectile and how bad the hovering problem turns out to be
		}

		float easedCurrentParallax;
		//easedCurrentParallax = -(Mathf.Cos(Mathf.PI * currentProjectileParallax) - 1f) / 2f; //Ease in out sine
		easedCurrentParallax = parallaxCorrectCurve.Evaluate(currentProjectileParallax); //Ease using curve field
		slugVisuals.position = transform.position + projectileParallaxOffset * easedCurrentParallax;
		Debug.DrawLine(transform.position, slugVisuals.position, Color.green, Time.deltaTime, false);
	}
	void FixedUpdate()
	{
		if(visualizeProjectilePath)
		{
			Debug.DrawLine(lastPosition, transform.position, Color.red, 3f);
			//Debug.DrawRay(transform.position, transform.forward * .2f, new Color(1f, 0.2f, 0f, 1f)); //Forward pointing ray
		}
		

		lastPosition = transform.position;

		currentProjectileSpeed = slugRb.velocity.magnitude;
	}
	/* void OnDrawGizmos()
	{
		Gizmos.color = new Color(1f, .2f, 0f, 1f);
		Gizmos.DrawMesh(slugMesh, -1, transform.position, transform.rotation);
	} */
}
