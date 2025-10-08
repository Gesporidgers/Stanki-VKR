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
	public int work = 0;
	delegate void IncreaseRotationSpeedDelegate();
	
	[SerializeField]
	EngineSound Sound;

	void Start()
	{

    }
	private void FixedUpdate()
	{
		
	}
	public void Switch()
	{
		work = Convert.ToInt32(work == 0);
		if (work == 1)
		{
			StartCoroutine(Sound.Start());
		}
		else
		{
			Sound.Stop();
		}
	}
}
