using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;


[Serializable]
public class EngineSound
{
	public AudioSource StartEngine; public AudioSource StopEngine; public AudioSource IdleEngine;
	public IEnumerator Start()
	{
		StartEngine.Play();
		yield return new WaitUntil(()=>!StartEngine.isPlaying);
		IdleEngine.Play();
	}
	public void Stop()
	{
		IdleEngine.Stop();
		StopEngine.Play();
	}
}

public class Stanok : MonoBehaviour
{
	[HideInInspector]
	public int work = 0;
	public int MotorForce;
	delegate void IncreaseRotationSpeedDelegate();
	
	[SerializeField]
	EngineSound Sound;

	public void Switch()
	{
		work = Convert.ToInt32(work == 0);
		if (work == 1)
		{
			JointMotor m = gameObject.GetComponent<HingeJoint>().motor;
			m.force = MotorForce;
			gameObject.GetComponent<HingeJoint>().motor = m;
			StartCoroutine(Sound.Start());
		}
		else
		{
			JointMotor m = gameObject.GetComponent<HingeJoint>().motor;
			m.force = 0;
			gameObject.GetComponent<HingeJoint>().motor = m;
			Sound.Stop();
		}
	}
}
