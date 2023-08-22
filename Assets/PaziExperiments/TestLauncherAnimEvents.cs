using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLauncherAnimEvents : MonoBehaviour
{
	[SerializeField] TestLauncherShooty launcherScript;

	// Update is called once per frame
	public void ReadyWeapon()
	{
		launcherScript.ReadyWeapon();
	}
}
