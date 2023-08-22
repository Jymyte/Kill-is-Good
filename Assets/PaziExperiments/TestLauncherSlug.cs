using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLauncherSlug : MonoBehaviour
{
	[SerializeField] bool visualizeProjectilePath;
	[SerializeField] float coolingSpeed = 1f;
	[SerializeField] float parallaxCorrectSpeed = 1f;
	[SerializeField] AnimationCurve parallaxCorrectCurve;
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
			// Parallax correction speed must increase if the projectile slows down significantly or tumbles down or rolls to prevent visually slowly hovering to its true position
			// But it must instead be reduced to zero if the projectile gets lodged in a surface to make sure no visual hovering appears at all
		}

		float easedCurrentParallax;
		//easedCurrentParallax = -(Mathf.Cos(Mathf.PI * currentProjectileParallax) - 1f) / 2f; //ease in out sine
		easedCurrentParallax = parallaxCorrectCurve.Evaluate(currentProjectileParallax);
		slugVisuals.position = transform.position + projectileParallaxOffset * easedCurrentParallax;
		Debug.DrawLine(transform.position, slugVisuals.position, Color.green, Time.deltaTime, false);
	}
	void FixedUpdate()
	{
		if(visualizeProjectilePath)
		{
			Debug.DrawLine(lastPosition, transform.position, Color.red, 3f);
			Debug.DrawRay(transform.position, transform.forward * .2f, new Color(1f, 0.2f, 0f, 1f));
		}
		

		lastPosition = transform.position;

		currentProjectileSpeed = slugRb.velocity.magnitude;
	}
	void OnDrawGizmos()
	{
		Gizmos.color = new Color(1f, .2f, 0f, 1f);
        Gizmos.DrawMesh(slugMesh, -1, transform.position, transform.rotation);
	}
}
