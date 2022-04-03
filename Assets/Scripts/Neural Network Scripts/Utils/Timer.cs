using System;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
	bool started;
	float timeStarted;
	float timerDuration;
	float timeElapsed;
	UnityAction action;

	private void Update()
	{
		if (started)
		tickTime();
	}

	private void tickTime()
	{
		if(timeElapsed > timerDuration)
		//if (timeStarted + (timerDuration * Time.timeScale) < Time.realtimeSinceStartup)
		{
			started = false;
			action.Invoke();
		}
		timeElapsed += Time.deltaTime;
	}

	public void Fire(float i_TimerDuration, UnityAction i_Action)
	{
		this.timerDuration = i_TimerDuration;
		this.timeElapsed = 0;
		this.action = i_Action;
		started = true;
	}

	public void ResetWithCurrentParameters()
	{
		started = true;
		this.timeStarted = Time.realtimeSinceStartup;
	}

	public void Stop()
	{
		started = false;
	}

	public bool isRunning()
	{
		return started;
	}
}

//public void setParameters(float timerDuration, Action action)
//{
//	this.timerDuration = timerDuration;
//	this.action = action;
//}

//public void fire()
//{
//	started = true;
//	this.timeStarted = Time.realtimeSinceStartup;
//}
