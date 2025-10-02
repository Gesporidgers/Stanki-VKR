using System;
using System.Collections;
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
	
	
	[SerializeField]
	EngineSound Sound;
	[SerializeField]
	public strDir Direction;
	void Start()
	{
		
	}
	private void FixedUpdate()
	{
		transform.Rotate(Convert.ToInt32(Direction.X) * 10f * work, Convert.ToInt32(Direction.Y) * 10f * work, Convert.ToInt32(Direction.Z) * 10f * work);
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
