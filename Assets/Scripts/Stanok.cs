using NUnit.Framework;
using NUnit.Framework.Constraints;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;


[Serializable]
public class EngineSound
{
	public AudioSource StartEngine; 
	public AudioSource StopEngine; 
	public AudioSource IdleEngine;
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
	public List<float> MotorForces;
	private int forceIndex = 0;
	
	[SerializeField]
	public EngineSound Sound;

	public Canvas StanokUI;
	public List<Button> Buttons;
	public TMPro.TMP_Text Text;


    public void Switch()
	{
		work = Convert.ToInt32(work == 0);
		if (work == 1)
		{
			UpdateMotor();
			StanokUI.gameObject.SetActive(true);
			StartCoroutine(Sound.Start());
            UpdateButtons();
		}
		else
		{
			StanokUI.gameObject.SetActive(false);
			JointMotor m = gameObject.GetComponent<HingeJoint>().motor;
			m.force = 0;
			gameObject.GetComponent<HingeJoint>().motor = m;
			StopAllCoroutines();
			Sound.Stop();
			
		}
	}

	public void IncSpeed()
    {
		forceIndex++;
		UpdateMotor();
		UpdateButtons();
	}

	public void DecSpeed()
    {
		forceIndex--;
		UpdateMotor();
		UpdateButtons();
	}

	private void UpdateButtons()
	{
		Buttons[1].interactable = forceIndex != 0; // кнопка минус
		Buttons[0].interactable = forceIndex + 1 != MotorForces.Count; // кнопка плюс
		this.Text.text = (MotorForces[forceIndex] / 10).ToString();
	}

	private void UpdateMotor()
	{
		JointMotor m = gameObject.GetComponent<HingeJoint>().motor;
		m.force = MotorForces[forceIndex];
		gameObject.GetComponent<HingeJoint>().motor = m;
	}
}
