using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLauncherSlug : MonoBehaviour
{
	[SerializeField] float slugHeat;
	[SerializeField] float coolingSpeed = 1f;
	[SerializeField] ParticleSystem slugFlareParticles;
	[SerializeField] AnimationCurve flareSizeCurve;
	[SerializeField] ParticleSystem slugFireParticles;
	[SerializeField] AnimationCurve fireEmissionCurve;
	float fireParticlesMaxEmissionOverTime;
	float fireParticlesMaxEmissionOverDistance;
	[SerializeField] ParticleSystem slugSmokeParticles;
	[SerializeField] AnimationCurve smokeEmissionCurve;
	float smokeParticlesMaxEmissionOverTime;
	Color hotEmissionColor;
	[SerializeField] MeshRenderer slugMeshRenderer;
	Material slugMaterial;
	Vector3 lastPosition;	
	void Start()
	{
		lastPosition = transform.position;
		slugHeat = 1f;

		slugMaterial = slugMeshRenderer.GetComponent<MeshRenderer>().material;
		hotEmissionColor = slugMaterial.GetColor("_EmissionColor");

		fireParticlesMaxEmissionOverTime = slugFireParticles.emission.rateOverTimeMultiplier;
		fireParticlesMaxEmissionOverDistance = slugFireParticles.emission.rateOverDistanceMultiplier;
		smokeParticlesMaxEmissionOverTime = slugSmokeParticles.emission.rateOverTimeMultiplier;
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
	}
	void FixedUpdate()
	{
		Debug.DrawLine(lastPosition, transform.position, Color.red, 3f);
		lastPosition = transform.position;
	}
}
