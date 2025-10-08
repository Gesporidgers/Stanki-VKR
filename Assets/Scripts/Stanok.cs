using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public class strDir
{
	public bool X;
	public bool Y;
	public bool Z;
}
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
	[SerializeField]
	public strDir Direction;
	[SerializeField]
	List<float> RotationSpeeds = new List<float>() { 10f };


	private float CurrentSpeed;
	private int CurrentSpeedId;
	void Start()
	{
		CurrentSpeed = RotationSpeeds[0];
		CurrentSpeedId = 0;

    }
	private void FixedUpdate()
	{
		transform.Rotate(
			Convert.ToInt32(Direction.X) * CurrentSpeed * work, 
			Convert.ToInt32(Direction.Y) * CurrentSpeed * work, 
			Convert.ToInt32(Direction.Z) * CurrentSpeed * work
		);
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

	public void IncreaseRotationSpeed()
	{
		CurrentSpeedId++;
		if(CurrentSpeedId >= RotationSpeeds.Count) CurrentSpeedId = RotationSpeeds.Count - 1;
		CurrentSpeed = RotationSpeeds[CurrentSpeedId];
    }
    public void DecreaseRotationSpeed()
	{
        CurrentSpeedId--;
        if (CurrentSpeedId < 0) CurrentSpeedId = 0;
        CurrentSpeed = RotationSpeeds[CurrentSpeedId];
    }
}
