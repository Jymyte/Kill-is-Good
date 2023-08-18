using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScaler : MonoBehaviour
{
	public void TogglePause()
	{
		if (Time.timeScale > 0f)
		{
			Time.timeScale = 0f;
		}
		else
		{
			Time.timeScale = 1f;
		}
	}
	public void ToggleSlowDown()
	{
		if (Time.timeScale < 1f || Mathf.Approximately(Time.timeScale, 0.2f))
		{
			Time.timeScale = 1f;
		}
		else
		{
			Time.timeScale = 0.2f;
		}
	}

	public void SlidingTimeScale(float sliderValue)
	{
		Time.timeScale = sliderValue;
	}
}
